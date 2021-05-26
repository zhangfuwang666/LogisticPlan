using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogisticPlan.Models;
namespace LogisticPlan
{
    public partial class LogisticPlanUI : UserControl
    {
        private static Machine machine = new Machine(0,0,0,0);
        public LogisticPlanUI()
        {
            InitializeComponent();
            InitContrls();
        }
        void InitContrls()
        {
            btnRollerStraight.Enabled = machine.crsEnable;
        }


        private void btnRollerStraight_Click(object sender, EventArgs e)
        {
            MessageBox.Show(machine.materialLength.ToString()+"=="+ machine.materialWidth.ToString());
            

        }

        private void txbConveyorMaterialLength_KeyUp(object sender, KeyEventArgs e)
        {
            machine.materialLength = txbConveyorMaterialLength.Text.Length!=0? Convert.ToInt32(txbConveyorMaterialLength.Text):0;
            txbConveyorMaterialLength.RectColor = machine.materialLength == 0 ? Color.Red : Color.Gray;
            btnRollerStraight.Enabled = machine.crsEnable;
        }

        private void txbConveyorMaterialWidth_KeyUp(object sender, KeyEventArgs e)
        {
            machine.materialWidth = (txbConveyorMaterialWidth.Text.Length != 0 ? Convert.ToInt32(txbConveyorMaterialWidth.Text) : 0);
            txbConveyorMaterialWidth.RectColor = machine.materialWidth == 0 ? Color.Red : Color.Gray;
            btnRollerStraight.Enabled = machine.crsEnable;
        }

        private void txbConveyorMaterialHeight_KeyUp(object sender, KeyEventArgs e)
        {
            machine.materialHeight = (txbConveyorMaterialHeight.Text.Length != 0 ? Convert.ToInt32(txbConveyorMaterialHeight.Text) : 0);
            txbConveyorMaterialHeight.RectColor = machine.materialHeight == 0 ? Color.Red : Color.Gray;
        }

        private void txbConveyorMaterialWeight_KeyUp(object sender, KeyEventArgs e)
        {
            machine.materialWeight = (txbConveyorMaterialWeight.Text.Length != 0 ? Convert.ToInt32(txbConveyorMaterialWeight.Text) : 0);
            txbConveyorMaterialWeight.RectColor = machine.materialHeight == 0 ? Color.Red : Color.Gray;
        }


    }
}
