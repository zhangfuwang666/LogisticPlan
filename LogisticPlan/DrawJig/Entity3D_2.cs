using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
using DotNetARX;
using System;
using System.Collections.Generic;
using System.Linq;
using Polyline = Autodesk.AutoCAD.DatabaseServices.Polyline;

namespace LogisticPlan
{
    public static class Entity3D_2
    {
        public static double AngleJig = 0;

        public static Entity CeBian3D(int Width, double Length, int colorIndex)
        {
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            using (var trans = db.TransactionManager.StartTransaction())
            {
                Polyline CeBian = new Polyline();
                //不带盖板
                CeBian.AddVertexAt(CeBian.NumberOfVertices, new Point2d(-35, -55), 0, 0, 0);
                CeBian.AddVertexAt(CeBian.NumberOfVertices, new Point2d(0, -55), 0, 0, 0);
                CeBian.AddVertexAt(CeBian.NumberOfVertices, new Point2d(0, 55), 0, 0, 0);
                CeBian.AddVertexAt(CeBian.NumberOfVertices, new Point2d(-35, 55), 0, 0, 0);
                CeBian.Closed = true;

                var Regions = RegionTools.CreateRegion(CeBian);
                Solid3d CeBian3D = new Solid3d();
                CeBian3D.Extrude(Regions[0], Length, 0);
                CeBian3D.ColorIndex = colorIndex;

                Matrix3d mtRotation = Matrix3d.Rotation(-Math.PI / 2, Vector3d.XAxis, Point3d.Origin);
                Matrix3d mtMove = Matrix3d.Displacement(Vector3d.ZAxis * -55 + Vector3d.XAxis * -(Width - 70) / 2);
                //归原地
                CeBian3D.TransformBy(mtRotation);
                CeBian3D.ColorIndex = colorIndex;
                Regions[0].Dispose();
                db.AddBlockTableRecord("输送侧边-L" + Length, CeBian3D);
                var brfId = db.InsertBlockReference("0", "输送侧边-L" + Length, Point3d.Origin, new Scale3d(), 0);
                var brf = trans.GetObject(brfId, OpenMode.ForWrite) as BlockReference;
                var brfCopy = brf.Clone() as Entity;
                brfCopy.SetLayer("!008#FirstFloor_Layer");
                brfCopy.ColorIndex = colorIndex;
                brfCopy.TransformBy(mtMove);
                brf.Erase();
                trans.Commit();
                return brfCopy;
            }
        }
      
    }
}
