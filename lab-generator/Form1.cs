using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace lab_generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
 
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // CAREFUL! row = y, column = x -> 1 hour of debugging:>
            Graphics g = e.Graphics;
            Pen blackPen = new Pen(Color.Black, 1);
            Pen whitePen = new Pen(SystemColors.Control, 1);

            // Creating full grid
            for (int row = 0; row < Program.Maze.Size; row++)
                for (int column = 0; column < Program.Maze.Size; column++)
                    g.DrawRectangle(blackPen, column * 40, row * 40, 40, 40);

            // Deleting walls
            for (int row = 0; row < Program.Maze.Size; row++)
                for (int column = 0; column < Program.Maze.Size; column++)
                {
                    Node current = Program.Maze.Container[row, column];
                    if (current.Up == 0)
                        g.DrawLine(whitePen, new Point(column * 40 + 1, row * 40), new Point(column * 40 + 39, row * 40));
                    if (current.Right == 0)
                        g.DrawLine(whitePen, new Point(column * 40 + 40, row * 40 + 1), new Point(column * 40 + 40, row * 40 + 39));
                    if (current.Left == 0)
                        g.DrawLine(whitePen, new Point(column * 40, row * 40), new Point(column * 40, row * 40 + 39));
                    if (current.Down == 0)
                        g.DrawLine(whitePen, new Point(column * 40 + 1, row * 40 + 40), new Point(column * 40 + 39, row * 40 + 40));
                }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.Maze = new Maze(Program.Complexity);
            Program.Maze.StartDFS();
            panel1.Refresh();

        }
            // Added for displaying console as well as WF app
            private void Form1_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        // Drawing Speed
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Program.DrawingSpeed = (int) numericUpDown2.Value;
        }

        // Complexity
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Program.Complexity = (int)numericUpDown2.Value;
        }
    }
}
