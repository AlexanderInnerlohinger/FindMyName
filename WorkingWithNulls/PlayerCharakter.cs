using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithNulls
{
    class PlayerCharakter
    {
        #region Fields

        private readonly SpecialDefence m_specialDefence;

        #endregion

        #region Properties

        public string Name { get; set; }

        public int Health { get; set; } = 100;

        #endregion

        #region Constructor

        public PlayerCharakter(SpecialDefence specialDefence)
        {
            m_specialDefence = specialDefence;
        }

        #endregion

        #region Methods

        public void Hit(int damage)
        {
            //int damageReduction = 0;

            //if (m_specialDefence != null)
            //{
            //    damageReduction = m_specialDefence.CalculateDamageReduction(damage);
            //}

            //int totalDamageTaken = damage - damageReduction;

            int totalDamageTaken = damage - m_specialDefence.CalculateDamageReduction(damage);

            Health -= totalDamageTaken;

            Console.WriteLine($"{Name}'s Health has been reduced by {totalDamageTaken} to {Health}.");
        }

        #endregion
    }
}
