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

[assembly: CommandClass(typeof(LogisticPlan.Program))]

namespace LogisticPlan
{
    public class Program:IExtensionApplication
    {
        public static String BianHao = "NUMBER";
        private static PaletteSet palette1 = new PaletteSet("智能规划平台 beta_2.0", new Guid());
        private LogisticPlanUI logisticPlanUI = new LogisticPlanUI();
        [CommandMethod("CommandLoadPlug")]
        public void CommandLoadPlug()
        {
            //palette1.Size = new System.Drawing.Size(350, 900);
            palette1.MinimumSize = new System.Drawing.Size(250, 600);
            palette1.Location = new System.Drawing.Point(250, 50);
            palette1.DockEnabled = DockSides.Right;
            palette1.Dock = DockSides.Right;
            palette1.Opacity = 60;

            palette1.Visible = true;
        }
        [CommandMethod("CommandCRS")]
        public void CommandCRS()
        {
            //EntityJigTools.Crs();
        }

        public void Initialize()
        {
            palette1.Add("app1", logisticPlanUI);

            palette1.TitleBarLocation = PaletteSetTitleBarLocation.Right;
            palette1.Style = PaletteSetStyles.NameEditable;
            palette1.Visible = true;
        }

        

        public void Terminate()
        {
           
        }

      

    }
}
