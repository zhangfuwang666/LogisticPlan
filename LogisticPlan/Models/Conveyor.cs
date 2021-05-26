using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticPlan.Models
{
    public class Conveyor 
    {
        private int m_pith;
        private int m_machineHeight;
        private int m_machineLenght;
        private int m_machineWidth;
        private int m_machineAngle;
        private int m_dock;
        private int m_transmission;
        private int m_rollerMaterial;
        private int m_speed;

       public Conveyor(int machineHeight, int machineLenght, int machineWidth, int machineAngle, int pith)
        {
            m_machineHeight = machineHeight;
            m_machineLenght = machineLenght;
            m_machineWidth = machineWidth;
            m_machineAngle = machineAngle;
            m_pith = pith;
        }

        public int machineHeight
        {
            get
            {
                return m_machineHeight;
            }
            set
            { 

            }
        }
        public int machineLenght { get; set; }
        public int machineWidth { get; set; }
        public int machineAngle { get; set; }
        public int pith
        {
            get { return m_pith; }
            set
            {

                GetPith(m_pith);
            }
        }
        public string dock { get; set; }
        public string transmission { get; set; }
        public string rollerMaterial { get; set; }
        public int speed { get; set; }
        int GetPith(int pith)
        {
            return pith;
        }
        void GetRollerDiamter()
        {

        }
    }
}
