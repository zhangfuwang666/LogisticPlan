using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using DotNetARX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticPlan
{
   public static  class DatabaseTools
    {
        /// <summary>
        /// 优化版插入块参照
        /// </summary>
        /// <param name="database">当前数据库</param>
        /// <param name="layerName">图层</param>
        /// <param name="BlcokName">块名称</param>
        /// <param name="Position">插入位置</param>
        /// <param name="scale">缩放</param>
        /// <param name="rotateAngle">旋转角度</param>
        /// <returns></returns>
        public static ObjectId InsertBlockReference(this Database database, string layerName, string BlcokName, Point3d Position, Scale3d scale, double rotateAngle)
        {
            ObjectId BlockRefId;//储存要插入的块参照Id
            BlockTable bt = (BlockTable)database.BlockTableId.GetObject(OpenMode.ForRead);
            using (var trans = database.TransactionManager.StartTransaction())
            {
                BlockTableRecord space = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                BlockReference br = new BlockReference(Position, bt[BlcokName]);
                br.ScaleFactors = scale;
                br.SetLayer(layerName);
                br.Rotation = rotateAngle;
                BlockRefId = space.AppendEntity(br);
                trans.AddNewlyCreatedDBObject(br, true);
                space.DowngradeOpen();
                trans.Commit();
            }

            return BlockRefId;
        }
        /// <summary>
        /// 块参照添加属性
        /// </summary>
        /// <param name="BlockRecId"></param>
        /// <param name="AttPosition"></param>
        /// <param name="AttRotation"></param>
        public static void BlockRecordAddAttude(ObjectId BlockRecId, Point3d AttPosition, double AttRotation)
        {
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            using (var trans = db.TransactionManager.StartTransaction())
            {
                BlockTable blockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord space = trans.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                AttributeDefinition attribute = new AttributeDefinition(new Point3d(300, 0, 0), " ", Program.BianHao, "请输入设备编号", ObjectId.Null);
                attribute.SetLayer("!013#EquipmentNumber");
                attribute.LockPositionInBlock = true;
                attribute.Position = Point3d.Origin;
                attribute.Height = 30;
                attribute.HorizontalMode = TextHorizontalMode.TextCenter;
                attribute.VerticalMode = TextVerticalMode.TextVerticalMid;
                attribute.AlignmentPoint = AttPosition;
                attribute.Rotation = AttRotation;
                var TextStyle = (TextStyleTable)trans.GetObject(db.TextStyleTableId, OpenMode.ForRead);
                try
                {
                    attribute.TextStyleId = TextStyle["Standard"];
                }
                catch (System.Exception)
                {
                }
                attribute.Invisible = false;

                BlockTableRecord record = trans.GetObject(BlockRecId, OpenMode.ForWrite) as BlockTableRecord;
                if (!record.HasAttributeDefinitions)//没有属性
                {
                    BlockRecId.AddAttsToBlock(attribute);
                }
                else//有属性，判断是否有该属性
                {
                    foreach (ObjectId Id in record)
                    {
                        //检查是否是属性定义
                        AttributeDefinition attDef = Id.GetObject(OpenMode.ForRead) as AttributeDefinition;
                        if (attDef != null)
                        {
                            if (!attDef.Tag.Contains(Program.BianHao))
                            {
                                BlockRecId.AddAttsToBlock(attribute);
                                break;
                            }
                        }
                    }
                }
                trans.Commit();
            }
        }
    }
}
