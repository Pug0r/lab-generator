using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_generator
{
    internal class Maze
    {
        private readonly Random rand = new Random();
        public readonly int Size;
        public Node[,] Container { get; }

        public Maze(int size) 
        {
            this.Size = size;
            this.Container = new Node[size, size];
            // Initializing array with standard Nodes
            for (int row = 0; row < size; row++)
                for (int col = 0; col < size; col++)
                    Container[row, col] = new Node();
            // Setting peripheral edges to permanent
            for (int i = 0; i < size; i++) 
            {
                Container[0, i].Up = 2; 
                Container[i, 0].Left = 2; 
                Container[i, size - 1].Right = 2; 
                Container[size - 1, i].Down = 2; 
            }

        }
    public void ShowMaze()
        {
            for (int row = 0; row < this.Size; row++)
                for (int column = 0; column < this.Size; column++)
                {
                    Console.WriteLine(Container[row, column].Up);
                    Console.WriteLine(Container[row, column].Right);
                    Console.WriteLine(Container[row, column].Down);
                    Console.WriteLine(Container[row, column].Left);
                }
        }
        public List<(int, int)> GetNeighbours(int row, int column)
        {
            // There are 9 separate cases in location of cell
            if (row == 0 && column == 0) // up-left 
                return new List<(int, int)> { (row + 1, column), (row, column + 1) };
            else if (row == 0 && column != 0) // up-mid
                return new List<(int, int)> { (row, column - 1), (row, column + 1), (row + 1, column) };
            else if (row == 0 && column == Size - 1) // up-right
                return new List<(int, int)> { (row - 1, column), (row, column + 1) };
            else if (row != 0 && column == 0) // left-mid
                return new List<(int, int)> { (row - 1, column), (row + 1, column), (row, column + 1) };
            else if (row != 0 && column == Size - 1) // right-mid
                return new List<(int, int)> { (row, column - 1), (row + 1, column), (row - 1, column) };
            else if (row == Size - 1 && column == 0) // bottom-left
                return new List<(int, int)> { (row - 1, column), (row, column + 1) };
            else if (row == Size - 1 && column != 0) // bottom-mid
                return new List<(int, int)> { (row, column - 1), (row - 1, column), (row, column + 1) };
            else if (row == Size - 1 && column == Size - 1) // bottom-right
                return new List<(int, int)> { (row, column - 1), (row - 1, column) };
            else // anything in the middle
                return new List<(int, int)> { (row - 1, column), (row, column - 1), (row, column + 1), (row + 1, column) };
        }

        public List<(int, int)> GetUnvisitedNeighbours(List<(int, int)> listOfNeighbours)
        {
            List <(int, int)> unvisitedOnes = new List<(int, int)> ();
            foreach ((int, int) tuple in listOfNeighbours)
                if ( !Container[tuple.Item1, tuple.Item2].Visited )
                    unvisitedOnes.Add(tuple);
            return unvisitedOnes;
        }
    private (int, int) ChooseRandomStartingNode()
        {
            string[] chooseFrom = { "up", "right", "bottom", "left" };
            switch ( chooseFrom[rand.Next(chooseFrom.Length)] )
            {
                case "up":
                    return (0, rand.Next(Size));
                case "right":
                    return (rand.Next(Size), Size - 1);
                case "bottom":
                    return (Size - 1, rand.Next(Size));
                case "left":
                    return (rand.Next(Size), 0);
                default: // seems useless but compiler demands it
                    return (0, 0);
            }
        }
    public void StartDFS()
        {
            
        }

    }

}
