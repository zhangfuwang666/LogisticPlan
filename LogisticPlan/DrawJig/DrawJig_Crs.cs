using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.GraphicsInterface;
using DotNetARX;

namespace LogisticPlan
{
    public class DrawJig_Crs : DrawJig
    {
        BlockReference brf;
        /// <summary>
        /// 块基点
        /// </summary>
        Point3d m_basePoint;
        int m_pith;
        int m_width;
        int m_lenght;
        int m_blockName;
        int m_height;

        Point3d m_zanCunPoint;
        int num;
        int m_dis;
        public int step;
        public List<Entity> entities = new List<Entity>();

        public DrawJig_Crs(Point3d point, int pith, int lenght, int width,int height, string blockName)
        {
            m_pith = pith;
            m_lenght = lenght;
            m_width = width;
            m_dis = lenght;
            m_height = height;

            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            using (var loc = doc.LockDocument())
            {
                using (var trans = db.TransactionManager.StartTransaction())
                {
                    //侧边
                    List<Entity> entities = new List<Entity>();
                    Entity CeBian3d = Entity3D_2.CeBian3D(m_width, m_lenght, 2);

                    Matrix3d mtMir = Matrix3d.Mirroring(new Plane(new Point3d(0, 0, 0), Vector3d.XAxis));
                    var CeBian3dCopy = CeBian3d.GetTransformedCopy(mtMir);
                    entities.Add(CeBian3d);
                    entities.Add(CeBian3dCopy);

                    //辊筒
                    Solid3d GunTong = new Solid3d();
                    GunTong.CreateFrustum(m_width - 70, 25, 25, 25);
                    GunTong.TransformBy(Matrix3d.Rotation(Math.PI / 2, Vector3d.YAxis, Point3d.Origin));
                    GunTong.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByBlock, 0);
                    db.AddBlockTableRecord("输送直辊筒-L" + m_width, GunTong);
                    var brfId_gunTong = db.InsertBlockReference("0", "输送直辊筒-L" + m_width, Point3d.Origin, new Scale3d(), 0);
                    var brf_gunTong = trans.GetObject(brfId_gunTong, OpenMode.ForWrite) as BlockReference;
                    var brf_gunTongCopy = brf_gunTong.Clone() as Entity;
                    brf_gunTongCopy.SetLayer("!008#FirstFloor_Layer");
                    brf_gunTongCopy.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByBlock, 0);
                    brf_gunTong.Erase();

                    brf_gunTongCopy.TransformBy(Matrix3d.Displacement(Vector3d.YAxis * 60) * Matrix3d.Displacement(Vector3d.ZAxis * -25));

                    int num = m_lenght / pith;

                    for (int i = 0; i < num; i++)
                    {
                        var GunTongCopy = brf_gunTongCopy.GetTransformedCopy(Matrix3d.Displacement(Vector3d.YAxis * pith * i));
                        entities.Add(GunTongCopy);
                    }
                    var id = db.AddBlockTableRecord(blockName, entities);
                    brf = new BlockReference(new Point3d(point.X,point.Y,point.Z==0?height:point.Z),id);

                    trans.Commit();
                }
            }
            m_basePoint = point;


        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Matrix3d mt = ed.CurrentUserCoordinateSystem;
            JigPromptPointOptions prom = new JigPromptPointOptions();
            prom.UserInputControls = UserInputControls.Accept3dCoordinates
                | UserInputControls.NoNegativeResponseAccepted
                | UserInputControls.NullResponseAccepted;
            prom.BasePoint = m_basePoint.TransformBy(mt);
            prom.UseBasePoint = true;
            prom.Message = "\n选择终点";
            var rusult = prompts.AcquirePoint(prom);
            if (rusult.Status == PromptStatus.Cancel)
            {
                return SamplerStatus.Cancel;
            }
            else
            {
                if (rusult.Value.X == m_zanCunPoint.X && rusult.Value.Y == m_zanCunPoint.Y && rusult.Value.Z == m_zanCunPoint.Z)
                {
                    return SamplerStatus.NoChange;
                }

                if (rusult.Value.GetVectorTo(m_basePoint).Length == 0)
                {
                    m_zanCunPoint = m_basePoint;
                }
                else
                {
                    m_zanCunPoint = rusult.Value;
                    num = (int)Math.Round(rusult.Value.GetVectorTo(m_basePoint).Length / m_dis, 0);
                    brf.Rotation = rusult.Value.GetVectorTo(m_basePoint).AngleOnPlane(new Plane(Point3d.Origin, Vector3d.ZAxis)) + Math.PI / 2;
                }

            }
            return SamplerStatus.OK;

        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            entities.Clear();
            for (int i = 0; i < num; i++)
            {
                Entity brfCopy = brf.GetTransformedCopy(Matrix3d.Displacement(-m_zanCunPoint.GetVectorTo(m_basePoint).GetNormal() * m_dis * i));
                entities.Add(brfCopy);

            }
            foreach (var ent in entities)
            {
                draw.Geometry.Draw(ent);
            }
            return true;
        }
    }
}

