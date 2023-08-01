using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Serilog;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            #region Adding Logger

            //const string customTemplate =
            //    "Will be logged -> {Timestamp:yyyy-MMM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}";

            //ILogger loggerAdding = new LoggerConfiguration()
            //    .WriteTo.Console()
            //    .WriteTo.File("logfile.txt", outputTemplate: customTemplate, fileSizeLimitBytes: null)
            //    .CreateLogger();

            //Log.Logger = loggerAdding;

            #endregion

            #region Rolling Logger

            //ILogger loggerRolling = new LoggerConfiguration()
            //    .WriteTo.Console()
            //    .WriteTo.RollingFile("rollinglogfile.txt", retainedFileCountLimit: 2)
            //    .CreateLogger();

            //Log.Logger = loggerRolling;

            #endregion

            #region Structured Logger 1

            ILogger logger = new LoggerConfiguration()
                .Destructure.ByTransforming<Color>(x => new { x.Red, x.Green })
                .WriteTo.Console()
                .CreateLogger();

            #endregion

            Log.Logger = logger;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("User added");

            string name = textBox1.Text;
            int age = int.Parse(textBox2.Text);
            DateTime dateAdded = DateTime.Now;

            var faveColors = new Color()
            {
                Red = 122,
                Green = 24,
                Blue = 19
            };

            var visited = new Dictionary<string, int>()
            {
                { "England", 5 },
                { "India", 2 },
                { "France", 1 }
            };

            Log.Information("Added user {UserName}, Age {UserAge}. Added on {Created} - {ID}.\nHis favorite colors are {@Colors} and he visited the following countries: {VisitedCountries}", name, age, dateAdded, Guid.NewGuid(), faveColors, visited);
        }

        class Color
        {
            public int Red { get; set; }
            public int Green { get; set; }
            public int Blue { get; set; }

            public override string ToString()
            {
                return string.Format("R: {0} G: {1} B: {2}", Red, Green, Blue);
            }
        }
    }
}
