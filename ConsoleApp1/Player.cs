namespace ConsoleApp1
{
    internal class Player : Entity
    {
        private double m_experience;

        public double Experience
        {
            get
            {
                return m_experience;
            }
            set
            {
                m_experience = value;
            }
        }
    }
}