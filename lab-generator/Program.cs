using System;
using System.Drawing;
using System.Windows.Forms;

namespace lab_generator
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static int Complexity = Constants.StartingComplexity;
        public static Maze Maze = new Maze(Complexity);
        public static Graphics canvas;

        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
