using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticPlan
{
    public static class EntityJigTools
    {
        public static void Crs(int width, int length, int pith,int height, string blockName)
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database db = acDoc.Database;
            PromptPointResult pPtRes;
            PromptPointOptions pPtOpts = new PromptPointOptions("");

            pPtOpts.Message = "\n选择输送的起点: ";
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            Point3d ptStart = pPtRes.Value;

            if (pPtRes.Status == PromptStatus.OK)
            {
          
                DrawJig_Crs rectJig = new DrawJig_Crs(pPtRes.Value, pith, length, width,  height, blockName);
                PromptResult PR = acDoc.Editor.Drag(rectJig);
                if (PR.Status == PromptStatus.OK)
                {
                    using (var trans = db.TransactionManager.StartTransaction())
                    {
                        var bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                        var space = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                        foreach (var ent in rectJig.entities)
                        {
                            space.AppendEntity(ent);
                            trans.AddNewlyCreatedDBObject(ent, true);
                        }


                        trans.Commit();
                    }

                }
            }


        }
        public static void qiut(int width, int length, int pith,int height, string blockName)
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database db = acDoc.Database;
            PromptPointResult pPtRes;
            PromptPointOptions pPtOpts = new PromptPointOptions("");

            pPtOpts.Message = "\n选择输送的起点: ";
            pPtRes = acDoc.Editor.GetPoint(pPtOpts);
            Point3d ptStart = pPtRes.Value;

            if (pPtRes.Status == PromptStatus.OK)
            {
                DrawJig_Crs rectJig = new DrawJig_Crs(pPtRes.Value, pith, length, width, height, blockName);
                PromptResult PR = acDoc.Editor.Drag(rectJig);
                if (PR.Status == PromptStatus.OK)
                {
                    using (var trans = db.TransactionManager.StartTransaction())
                    {
                        var bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                        var space = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                        foreach (var ent in rectJig.entities)
                        {
                            space.AppendEntity(ent);
                            trans.AddNewlyCreatedDBObject(ent, true);
                        }


                        trans.Commit();
                    }

                }
            }


        }
    

    }
}
