namespace UlamSpiral
{
    public partial class Form1 : Form
    {
        // Only for squares and odd numbers
        private int m_Dimensions = 41;

        private enum Extensions
        {
            right,
            up,
            left,
            down
        }

        public Form1()
        {
            InitializeComponent();

            int[,] allFigures = new int[m_Dimensions, m_Dimensions];

            PopulateArray(allFigures);
            DisplayArray(allFigures);

            this.Name = $"{m_Dimensions} x {m_Dimensions}";
        }


        private void PopulateArray(int[,] allFigures)
        {
            // Initialize needed varialbes
            Extensions direction = Extensions.right;
            int movesLeft = 1;
            int directionChanges = 0;
            int turnIncrementer = 1;

            // Find starting position
            int middleIndex = m_Dimensions / 2;

            if (m_Dimensions % 2 != 0)
            {
                middleIndex = m_Dimensions / 2;
            }

            int xIndex = middleIndex;
            int yIndex = middleIndex;

            allFigures[xIndex, yIndex] = 1;

            // Start loop for populating
            for (int i = 2; i <= Math.Pow(m_Dimensions, 2); i++)
            {
                switch (direction)
                {
                    case Extensions.right:

                        xIndex++;
                        break;

                    case Extensions.up:

                        yIndex--;
                        break;

                    case Extensions.left:

                        xIndex--;
                        break;

                    case Extensions.down:

                        yIndex++;
                        break;
                }

                allFigures[xIndex, yIndex] = i;
                movesLeft--;

                if (movesLeft == 0)
                {
                    direction = direction.Next();
                    directionChanges++;
                    movesLeft = ResetMoves(ref turnIncrementer, directionChanges);
                }

            }
        }

        private int ResetMoves(ref int turnIncrementer, int directionChanges)
        {
            if (directionChanges % 2 != 0)
            {
                return turnIncrementer;
            }

            turnIncrementer++;
            return turnIncrementer;
        }

        private void DisplayArray(int[,] allFigures)
        {
            int buttonSize = 35;
            int countColumns = 0;
            int countRows = 0;

            foreach (int entry in allFigures)
            {
                Button button = new Button();
                button.Text = entry.ToString();
                button.Size = new Size(buttonSize, buttonSize);
                button.Font = new Font(FontFamily.GenericMonospace, 6);
                button.Location = new Point(button.Size.Height * countColumns, button.Size.Height * countRows);
                this.Controls.Add(button);

                if (CheckIfPrimeNumber(entry))
                {
                    button.BackColor = Color.Aqua;
                }

                countColumns++;

                if (countColumns == m_Dimensions)
                {
                    countRows++;
                    countColumns = 0;
                }
            }

        }

        private bool CheckIfPrimeNumber(int n)
        {
            if (n == 2 || n == 3)
                return true;

            if (n <= 1 || n % 2 == 0 || n % 3 == 0)
                return false;

            for (int i = 5; i * i <= n; i += 6)
            {
                if (n % i == 0 || n % (i + 2) == 0)
                    return false;
            }

            return true;
        }
    }
}