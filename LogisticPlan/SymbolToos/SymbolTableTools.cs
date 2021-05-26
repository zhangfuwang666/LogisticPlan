using System.Collections.Generic;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using DotNetARX;

namespace LogisticPlan
{
    public static class SymbolTableTools
    {
        public static void loadSymbleTable()
        {
            LoadLineType();

            LoadDimStyle();
        }

        public static void LoadLineType()//加载线性文件
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            using (doc.LockDocument())
            {
                var db = doc.Database;
                var ed = doc.Editor;
                using (var trans = db.TransactionManager.StartTransaction())
                {
                    var lt = (LinetypeTable)db.LinetypeTableId.GetObject(OpenMode.ForRead);
                    try
                    {
                        if (!lt.Has("CENTER"))
                        {
                            db.LoadLineTypeFile("CENTER", "acadiso.lin");
                        }
                        if (!lt.Has("CENTER2"))
                        {
                            db.LoadLineTypeFile("CENTER2", "acadiso.lin");
                        }
                        if (!lt.Has("CENTERX2"))
                        {
                            db.LoadLineTypeFile("CENTERX2", "acadiso.lin");
                        }
                        if (!lt.Has("Continuous"))
                        {
                            db.LoadLineTypeFile("Continuous", "acadiso.lin");
                        }

                        if (!lt.Has("DOT"))
                        {
                            db.LoadLineTypeFile("DOT", "acadiso.lin");
                        }
                        if (!lt.Has("DOT2"))
                        {
                            db.LoadLineTypeFile("DOT2", "acadiso.lin");
                        }
                        if (!lt.Has("DOTX2"))
                        {
                            db.LoadLineTypeFile("DOTX2", "acadiso.lin");
                        }

                        if (!lt.Has("HIDDEN"))
                        {
                            db.LoadLineTypeFile("HIDDEN", "acadiso.lin");
                        }
                        if (!lt.Has("HIDDEN2"))
                        {
                            db.LoadLineTypeFile("HIDDEN2", "acadiso.lin");
                        }
                        if (!lt.Has("HIDDENX2"))
                        {
                            db.LoadLineTypeFile("HIDDENX2", "acadiso.lin");
                        }

                        if (!lt.Has("PHANTOM"))
                        {
                            db.LoadLineTypeFile("PHANTOM", "acadiso.lin");
                        }
                        if (!lt.Has("PHANTOM2"))
                        {
                            db.LoadLineTypeFile("PHANTOM2", "acadiso.lin");
                        }
                        if (!lt.Has("PHANTOMX2"))
                        {
                            db.LoadLineTypeFile("PHANTOMX2", "acadiso.lin");
                        }
                    }
                    catch (System.Exception)
                    {
                    }
                    trans.Commit();
                }
            }
        }

        public static void LoadLayer(string layerName)//加载图层
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (doc.LockDocument())
            {
                var db = doc.Database;
                var ed = doc.Editor;
                using (var trans = db.TransactionManager.StartTransaction())
                {
                    switch (layerName)
                    {
                        case "!000#Drawing_Layer": AddLayer(db, "!000#Drawing_Layer", 7, "Continuous", "图框图层"); break;
                        case "!001#Contour_Layer": AddLayer(db, "!001#Contour_Layer", 7, "Continuous", "外轮廓图层"); break;
                        case "!002#CenterLine_Layer": AddLayer(db, "!002#CenterLine_Layer", 1, "CENTER", "中心线图层"); break;
                        case "!003#DotLine_Layer": AddLayer(db, "!003#DotLine_Layer", 1, "Dot", "不可见线图层"); break;
                        case "!004#Text_Layer": AddLayer(db, "!004#Text_Layer", 4, "Continuous", "文字图层"); break;
                        case "!005#Dimention_Layer": AddLayer(db, "!005#Dimention_Layer", 4, "Continuous", "尺寸线图层"); break;
                        case "!006#DimentionPower_Layer": AddLayer(db, "!006#DimentionPower_Layer", 4, "Continuous", "动力分段尺寸线图层"); break;
                        case "!007#DimentionBordSide_Layer": AddLayer(db, "!007#DimentionBordSide_Layer", 7, "Continuous", "侧边分段尺寸线图层"); break;
                        case "!008#FirstFloor_Layer": AddLayer(db, "!008#FirstFloor_Layer", 9, "Continuous", "第一层输送图层"); break;
                        case "!009#SecondFloor_Layer": AddLayer(db, "!009#SecondFloor_Layer", 3, "Continuous", "第二层输送图层"); break;
                        case "!010#ThirdFloor_Layer": AddLayer(db, "!010#ThirdFloor_Layer", 4, "Continuous", "第三层输送图层"); break;
                        case "!011#FourthFloor_Layer": AddLayer(db, "!011#FourthFloor_Layer", 5, "Continuous", "第四层输送图层"); break;
                        case "!012#ConveyorLeg_Layer": AddLayer(db, "!012#ConveyorLeg_Layer", 4, "Continuous", "输送支腿图层"); break;
                        case "!013#EquipmentNumber": AddLayer(db, "!013#EquipmentNumber", 7, "Continuous", "设备编号图层"); break;
                        case "!014#GoodsShelf_Layer": AddLayer(db, "!014#GoodsShelf_Layer", 163, "Continuous", "多穿货架图层"); break;
                        case "!015#3DGoodsShelf_LiZhu_Layer": AddLayer(db, "!015#3DGoodsShelf_LiZhu_Layer", 150, "Continuous", "三维多穿货架立柱图层"); break;
                        case "!015#3DGoodsShelf_LiZhuDiJiao_Layer": AddLayer(db, "!015#3DGoodsShelf_LiZhuDiJiao_Layer", 23, "Continuous", "三维多穿货架立柱地脚图层"); break;
                        case "!015#3DGoodsShelf_DaoGui_Layer": AddLayer(db, "!015#3DGoodsShelf_DaoGui_Layer", 110, "Continuous", "三维多穿货架导轨图层"); break;
                        case "!015#3DGoodsShelf_WaiCeLang_Layer": AddLayer(db, "!015#3DGoodsShelf_WaiCeLang_Layer", 124, "Continuous", "三维多穿货架外侧梁图层"); break;
                        case "!015#3DGoodsShelf_WeiXiuCeng_Layer": AddLayer(db, "!015#3DGoodsShelf_WeiXiuCeng_Layer", 115, "Continuous", "三维多穿货架维修层图层"); break;
                        case "!015#3DGoodsShelf_LiZhuLaGan_Layer": AddLayer(db, "!015#3DGoodsShelf_LiZhuLaGan_Layer", 30, "Continuous", "三维多穿货架立柱拉杆图层"); break;
                        case "!015#3DGoodsShelf_HuoGeHengCheng_Layer": AddLayer(db, "!015#3DGoodsShelf_HuoGeHengCheng_Layer", 8, "Continuous", "三维多穿货架货格横撑图层"); break;
                        case "!015#3DGoodsShelf_DingLaLongMenLiang_Layer": AddLayer(db, "!015#3DGoodsShelf_DingLaLongMenLiang_Layer", 163, "Continuous", "三维多穿货架顶拉龙门梁图层"); break;
                        case "!015#3DGoodsShelf_DingLaXieLa_Layer": AddLayer(db, "!015#3DGoodsShelf_DingLaXieLa_Layer", 163, "Continuous", "三维多穿货架顶拉斜拉图层"); break;
                        case "!015#3DGoodsShelf_BeiLaZuo_Layer": AddLayer(db, "!015#3DGoodsShelf_BeiLaZuo_Layer", 150, "Continuous", "三维多穿货架背拉座图层"); break;
                        case "!015#3DGoodsShelf_BeiLaDiJiao_Layer": AddLayer(db, "!015#3DGoodsShelf_BeiLaDiJiao_Layer", 150, "Continuous", "三维多穿货架背拉地脚图层"); break;
                        case "!015#3DGoodsShelf_BeiLaLiang_Layer": AddLayer(db, "!015#3DGoodsShelf_BeiLaLiang_Layer", 150, "Continuous", "三维多穿货架背拉梁图层"); break;
                        case "!015#3DGoodsShelf_HuoGeDingHengLiang_Layer": AddLayer(db, "!015#3DGoodsShelf_HuoGeDingHengLiang_Layer", 3, "Continuous", "三维多穿货架顶横梁图层"); break;
                        case "!016#Arrows_Layer": AddLayer(db, "!016#Arrows_Layer", 3, "Continuous", "箭头层"); break;

                        default:
                            break;
                    }
                    trans.Commit();
                }
            }
        }

        public static ObjectId AddLayer(Database db, string layerName, short LayerColorIndex, string LineTypeName, string Description)//封装添加图层方法
        {
            LayerTable layerTable = (LayerTable)db.LayerTableId.GetObject(OpenMode.ForRead);
            if (!layerTable.Has(layerName))
            {
                LayerTableRecord layer = new LayerTableRecord();
                layer.Name = layerName;
                ColorMethod method = ColorMethod.ByLayer;
                layer.Color = Color.FromColorIndex(method, LayerColorIndex);
                var lt = (LinetypeTable)db.LinetypeTableId.GetObject(OpenMode.ForRead);
                LoadLineType();
                var id = lt[LineTypeName];
                layer.LinetypeObjectId = id;
                layer.Description = Description;
                layerTable.UpgradeOpen();
                layerTable.Add(layer);
                db.TransactionManager.AddNewlyCreatedDBObject(layer, true);
                layer.DowngradeOpen();
            }
            return layerTable[layerName];
        }

        public static void LoadDimStyle()//加载标注样式
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            using (doc.LockDocument())
            {
                var db = doc.Database;
                var ed = doc.Editor;
                using (var trans = db.TransactionManager.StartTransaction())
                {
                    AddDimStyle(db, "动力分段1");
                    AddDimStyle(db, "动力分段2");
                    AddDimStyle(db, "动力分段3");
                    AddDimStyle(db, "动力分段4");
                    AddDimStyle(db, "侧边分段1");
                    AddDimStyle(db, "侧边分段2");
                    AddDimStyle(db, "侧边分段3");
                    AddDimStyle(db, "侧边分段4");
                    trans.Commit();
                }
            }
        }

        public static ObjectId AddDimStyle(Database db, string DimName)//添加标注样式
        {
            DimStyleTable table = (DimStyleTable)db.DimStyleTableId.GetObject(OpenMode.ForRead);
            if (!table.Has(DimName))
            {
                DimStyleTableRecord record = new DimStyleTableRecord();
                record.Name = DimName;
                record.Dimasz = 30;
                record.Dimtxt = 50;
                record.Dimtad = 1;
                record.Dimdec = 0;
                record.Dimtad = 2;
                //文字
                record.Dimtih = false;
                record.Dimtoh = false;
                record.Dimtxtdirection = false;
                table.UpgradeOpen();
                table.Add(record);
                db.TransactionManager.AddNewlyCreatedDBObject(record, true);
                table.DowngradeOpen();
            }
            return table[DimName];
        }

        public static ObjectId AddMlederStyle(string MlStyleName)//添加标注样式
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database Db = Application.DocumentManager.MdiActiveDocument.Database;
            ObjectId ArrowId = Db.GetArrowObjectId(DimArrowBlock.Dot);
            using (DocumentLock docLock = doc.LockDocument())
            {
                using (var trans = Db.TransactionManager.StartTransaction())
                {
                    //获取引线字典
                    DBDictionary MleaderStyles = (DBDictionary)trans.GetObject(Db.MLeaderStyleDictionaryId, OpenMode.ForWrite);
                    //如果不存在则创建引线样式
                    if (!MleaderStyles.Contains(MlStyleName))
                    {
                        //生成信息卡片
                        Polyline Rectangle = new Polyline();
                        Rectangle.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
                        Rectangle.AddVertexAt(0, new Point2d(600, 0), 0, 0, 0);
                        Rectangle.AddVertexAt(0, new Point2d(600, 400), 0, 0, 0);
                        Rectangle.AddVertexAt(0, new Point2d(0, 400), 0, 0, 0);
                        Rectangle.Closed = true;
                        Line line = new Line(new Point3d(0, 200, 0), new Point3d(600, 200, 0));

                        AttributeDefinition attribute = new AttributeDefinition(new Point3d(300, 0, 0), "设备编号", "设备编号", "请输入设备编号", ObjectId.Null);
                        attribute.LockPositionInBlock = true;
                        attribute.Position = new Point3d(10, 0, 0);
                        attribute.Height = 100;
                        attribute.Invisible = false;

                        AttributeDefinition attribute2 = new AttributeDefinition(new Point3d(300, 200, 0), "设备名称", "设备名称", "请输入设备名称", ObjectId.Null);
                        attribute2.LockPositionInBlock = true;
                        attribute2.Position = new Point3d(10, 200, 0);
                        attribute2.Height = 100;
                        attribute2.Invisible = false;

                        var blockId = Db.AddBlockTableRecord("InfoCard", Rectangle, line, attribute, attribute2);

                        MLeaderStyle MlStyle = new MLeaderStyle();
                        MlStyle.ArrowSymbolId = ArrowId;//设置箭头样式
                        MlStyle.ArrowSize = 50;
                        MlStyle.FirstSegmentAngleConstraint = AngleConstraint.Degrees045;
                        MlStyle.ContentType = ContentType.BlockContent;
                        MlStyle.BlockId = blockId;

                        MlStyle.BlockColor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByLayer, 256);
                        MleaderStyles.DowngradeOpen();
                        //发送到Db
                        var MlstyleId = MlStyle.PostMLeaderStyleToDb(Db, MlStyleName);
                        trans.AddNewlyCreatedDBObject(MlStyle, true);
                        trans.Commit();
                        return MlstyleId;
                    }
                    //存在则返回引线样式ID
                    else
                    {
                        return MleaderStyles.GetAt(MlStyleName);
                    }
                }
            }
        }

        /// <summary>
        /// 设置图层（不存在该图层则会新建）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="LayerName">图层名称</param>
        public static void SetLayer(this Entity entity, string LayerName)
        {
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            using (var trans = db.TransactionManager.StartTransaction())
            {
                LayerTable layerTable = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForRead);
                if (!layerTable.Has(LayerName))
                {
                    LoadLayer(LayerName);
                }
                entity.Layer = LayerName;
                trans.Commit();
            }
        }

        public static void AddItems(this List<Entity> ts, params Entity[] entities)
        {
            foreach (var item in entities)
            {
                ts.Add(item);
            }
        }
    }
}
