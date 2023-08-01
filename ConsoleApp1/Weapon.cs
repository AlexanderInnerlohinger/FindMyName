using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Weapon
    {
        private string m_weaponName;
        private WeaponTypeEnum m_weaponType;
        private double m_damageAmount;

        public enum WeaponTypeEnum
        {
            Unarmed,
            Blunt,
            Sharp,
            Magic
        }

        public string WeaponName
        {
            get
            {
                return m_weaponName;
            }
            set
            {
                m_weaponName = value;
            }
        }

        public WeaponTypeEnum WeaponType
        {
            get
            {
                return m_weaponType;
            }
            set
            {
                m_weaponType = value;
            }
        }

        public double DamageAmount
        {
            get
            {
                return m_damageAmount;
            }
            set
            {
                m_damageAmount = value;
            }
        }

        public Weapon(string weaponName, WeaponTypeEnum weaponType, int damageAmount)
        {
            m_weaponName = weaponName;
            m_weaponType = weaponType;
            m_damageAmount = damageAmount;
        }

    }
}
