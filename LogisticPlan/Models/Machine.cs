using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticPlan.Models
{
    public class Machine
    {
        int m_materialLength;
        int m_materialWidth;
        int m_materialHeight;
        int m_materialWeight;
        int m_materialType;
        public Machine(int materialLength, int materialWidth, int materialHeight, int materialWeight)
        {
            m_materialLength = materialLength;
            m_materialWidth = materialWidth;
            m_materialHeight = materialHeight;
            m_materialWeight = materialWeight;
        }

        public string code { get; set; }
        public string name { get; set; }
        public string version { get; set; }
        public string specification { get; set; }
        public int materialLength
        {
            get
            {
                return m_materialLength;
            }
            set
            {
                m_materialLength = GetMaterialLenth(value);
            }
        }
        public int materialWidth
        {
            get
            {
                return m_materialWidth;
            }
            set
            {
                m_materialWidth = GetMaterialWidth(value);
            }
        }
        public int materialHeight
        {
            get
            {
                return m_materialHeight;
            }
            set
            {
                m_materialHeight = GetMaterialHeight(value);
            }
        }
        public int materialWeight
        {
            get
            {
                return m_materialWeight;

            }
            set
            {
                m_materialWeight = GetMaterialWeight(value);
            }
        }
        public string materialType { get; set; }
        public string sourse { get; set; }
        public double power { get; set; }
        public bool crsEnable
        {
            get
            {
                return GetCrsEnable();
            }
        }

        string GetSpecifiaction()
        {

            return string.Empty;
        }
        int GetMaterialLenth(int length)
        {

            if (length > 300 && length <= 400)
            {
                return 400;
            }
            else if (length > 400 && length <= 500)
            {
                return 500;
            }
            else if (length > 500 && length <= 600)
            {
                return 600;
            }
            else if (length > 600 && length <= 700)
            {
                return 700;
            }
            else if (length > 800 && length <= 800)
            {
                return 800;
            }
            else if (length > 950 && length <= 1050)
            {
                return 1000;
            }
            else if (length > 1050 && length <= 1150)
            {
                return 1100;
            }
            else if (length > 1150 && length <= 1250)
            {
                return 1200;
            }
            else
            {
                return 0;
            }
        }
        int GetMaterialWeight(int weight)
        {
            if (weight > 0 && weight <= 30)
            {
                return 30;
            }
            else if (weight > 30 && weight <= 50)
            {
                return 50;
            }
            else if (weight > 300 && weight <= 800)
            {
                return 800;
            }
            else if (weight > 800 && weight <= 900)
            {
                return 900;
            }
            else if (weight > 900 && weight <= 1000)
            {
                return 1000;
            }
            else if (weight > 1000 && weight <= 1100)
            {
                return 1100;
            }
            else if (weight > 1100 && weight <= 1200)
            {
                return 1200;
            }
            else if (weight > 1200 && weight <= 1300)
            {
                return 1300;
            }
            else if (weight > 1300 && weight <= 1400)
            {
                return 1400;
            }
            else if (weight > 1400 && weight <= 1500)
            {
                return 1500;
            }
            else
            {
                return 0;
            }
        }
        int GetMaterialHeight(int height)
        {
            if (height >= 120 && height <= 150)
            {
                return 150;
            }
            else if (height > 150 && height <= 200)
            {
                return 200;
            }
            else if (height > 200 && height <= 250)
            {
                return 250;
            }
            else if (height > 250 && height <= 300)
            {
                return 300;
            }
            else if (height > 300 && height <= 350)
            {
                return 350;
            }
            else if (height > 350 && height <= 400)
            {
                return 400;
            }
            else if (height > 400 && height <= 450)
            {
                return 450;
            }
            else if (height > 450 && height <= 500)
            {
                return 500;
            }
            else
            {
                return 0;
            }

        }
        int GetMaterialWidth(int width)
        {

            if (width > 300 && width <= 400)
            {
                return 400;
            }
            else if (width > 400 && width <= 500)
            {
                return 500;
            }
            else if (width > 500 && width <= 600)
            {
                return 600;
            }
            else if (width > 600 && width <= 700)
            {
                return 700;
            }
            else if (width > 800 && width <= 800)
            {
                return 800;
            }
            else if (width > 950 && width <= 1050)
            {
                return 1000;
            }
            else if (width > 1050 && width <= 1150)
            {
                return 1100;
            }
            else if (width > 1150 && width <= 1250)
            {
                return 1200;
            }
            else
            {
                return 0;
            }
        }
        bool GetCrsEnable()
        {
            if (materialLength!=0&&materialWidth!=0)
            {
                return true;
            }
            else
            {
               return false;
            }
           
        }
        
    }
}
