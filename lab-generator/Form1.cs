using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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
                    g.DrawRectangle(blackPen, column * Constants.SSL, row * Constants.SSL, Constants.SSL, Constants.SSL);

            // Deleting walls by drawing white lines at corresponding wall
            for (int row = 0; row < Program.Maze.Size; row++)
                for (int column = 0; column < Program.Maze.Size; column++)
                {
                    Node current = Program.Maze.Container[row, column];
                    if (current.Up == 0)
                        g.DrawLine(whitePen, new Point(column * Constants.SSL + 1, row * Constants.SSL), new Point(column * Constants.SSL + Constants.SSL - 1, row * Constants.SSL));
                    if (current.Right == 0)
                        g.DrawLine(whitePen, new Point(column * Constants.SSL + Constants.SSL, row * Constants.SSL + 1), new Point(column * Constants.SSL + Constants.SSL, row * Constants.SSL + Constants.SSL - 1));
                    if (current.Left == 0)
                        g.DrawLine(whitePen, new Point(column * Constants.SSL, row * Constants.SSL + 1), new Point(column * Constants.SSL, row * Constants.SSL + Constants.SSL - 1));
                    if (current.Down == 0)
                        g.DrawLine(whitePen, new Point(column * Constants.SSL + 1, row * Constants.SSL + Constants.SSL), new Point(column * Constants.SSL + Constants.SSL - 1, row * Constants.SSL + Constants.SSL));
                }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.Maze = new Maze(Program.Complexity);
            Program.Maze.StartDFS();
            panel1.Refresh();

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Complexity
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Program.Complexity = (int)numericUpDown1.Value;
        }
    }

}
