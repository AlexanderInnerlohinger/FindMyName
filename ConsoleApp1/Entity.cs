using System;
using System.Dynamic;

namespace ConsoleApp1
{
    public class Entity
    {
        #region Fields
        
        private string m_Name;
        private double m_health;
        private double m_level;

        private string m_movementType;
        private double m_speed;

        private Armor m_armor;
        private Weapon m_weapon;

        public enum m_owner
        {
            player,
            wilderness
        }

        #endregion

        #region Properties

        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public double Health
        {
            get
            {
                return m_health;
            }
            set
            {
                m_health = value;
            }
        }

        public string MovementType
        {
            get
            {
                return m_movementType;
            }
            set
            {
                m_movementType = value;
            }
        }

        public double Speed
        {
            get
            {
                return m_speed;
            }
            set
            {
                m_speed = value;
            }
        }

        public Armor Armor
        {
            get
            {
                return m_armor;
            }
            set
            {
                m_armor = value;
            }
        }

        public Weapon Weapon
        {
            get
            {
                return m_weapon;
            }
            set
            {
                m_weapon = value;
            }
        }

        #endregion

        #region Constructors

        public Entity()
        {
        }

        #endregion
    }
}