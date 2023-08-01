using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Armor
    {
        private string m_armorName;
        private ArmorTypeEnum m_armorType;
        private double m_armorAmount;

        public enum ArmorTypeEnum
        {
            None,
            Light,
            Medium,
            Heavy
        }
        
        public string ArmorName
        {
            get
            {
                return m_armorName;
            }
            set
            {
                m_armorName = value;
            }
        }

        public ArmorTypeEnum ArmorType
        {
            get
            {
                return m_armorType;
            }
            set
            {
                m_armorType = value;
            }
        }

        public double ArmorAmount
        {
            get
            {
                return m_armorAmount;
            }
            set
            {
                m_armorAmount = value;
            }
        }

        public Armor(string armorName, ArmorTypeEnum armorType, int armorAmount)
        {
            m_armorName = armorName;
            m_armorType = armorType;
            m_armorAmount = armorAmount;
        }
    }
}
