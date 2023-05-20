using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
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
                Container[0, i].SetWallPermanent('u'); 
                Container[i, 0].SetWallPermanent('l');
                Container[i, size - 1].SetWallPermanent('r');
                Container[size - 1, i].SetWallPermanent('d');
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
                return new List<(int, int)> { (1, 0), (0, 1) };
            else if (row == 0 && column != 0 && column != Size-1) // up-mid
                return new List<(int, int)> { (row, column - 1), (row, column + 1), (row + 1, column) };
            else if (row == 0 && column == Size - 1) // up-right
                return new List<(int, int)> { (row, column - 1), (row + 1, column) };
            else if (row != 0 && row != Size - 1 && column == 0) // left-mid
                return new List<(int, int)> { (row - 1, column), (row + 1, column), (row, column + 1) };
            else if (row != 0 && row != Size - 1 && column == Size - 1) // right-mid
                return new List<(int, int)> { (row, column - 1), (row + 1, column), (row - 1, column) };
            else if (row == Size - 1 && column == 0) // bottom-left
                return new List<(int, int)> { (row - 1, column), (row, column + 1) };
            else if (row == Size - 1 && column != 0 && column != Size - 1) // bottom-mid
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
    private (int, int, char) ChooseRandomStartingNode()
        {
            string[] chooseFrom = { "up", "right", "down", "left" };
            switch ( chooseFrom[rand.Next(chooseFrom.Length)] )
            {
                case "up":
                    return (0, rand.Next(Size), 'u'); ;
                case "right":
                    return (rand.Next(Size), Size - 1, 'r');
                case "down":
                    return (Size - 1, rand.Next(Size), 'd');
                case "left":
                    return (rand.Next(Size), 0, 'l');
                default: // seems useless but compiler demands it
                    return (0, 0, '0');
            }
        }
        private void RemoveWallBetweenCells((int, int) cellOne, (int, int) cellTwo)
        {
            if (cellOne.Item1 == cellTwo.Item1) // we are on the same row
            {
                if (cellOne.Item2 < cellTwo.Item2 ) // wall to remove is on the right
                {
                    this.Container[cellOne.Item1, cellOne.Item2].DeleteWall('r');
                }
                else
                {
                    this.Container[cellOne.Item1, cellOne.Item2].DeleteWall('l');
                }
            }
            else // we are in the same column
            {
                if (cellOne.Item1 < cellTwo.Item1)
                {
                    this.Container[cellOne.Item1, cellOne.Item2].DeleteWall('d');
                }

                else
                {
                    this.Container[cellOne.Item1, cellOne.Item2].DeleteWall('u');
                }
            }
        }
    private (int, int, char) ChooseExit( (int, int, char) EntranceNode)
        {
            switch (EntranceNode.Item3)
            {
                case 'u':
                    return (Size - 1, rand.Next(Size), 'd');
                case 'd':
                    return (0, rand.Next(Size), 'u');
                case 'r':
                    return (rand.Next(Size), 0, 'l');
                case 'l':
                    return (rand.Next(Size), Size - 1, 'r');
                default: 
                    return (0, 0, '0');
            }
        }

    public void StartDFS() // iterative version of DFS search 
        {
            // Choosing the start and the exit point, deleting permanent edges
            (int, int, char) startingNode = ChooseRandomStartingNode();
            (int, int, char) exitNode = ChooseExit(startingNode);
            Container[startingNode.Item1, startingNode.Item2].MarkVisited();
            Container[startingNode.Item1, startingNode.Item2].RemovePermanent(startingNode.Item3);
            Container[exitNode.Item1, exitNode.Item2].RemovePermanent(exitNode.Item3);

            Stack<(int, int)> toProcess = new Stack<(int, int)>();
            toProcess.Push((startingNode.Item1, startingNode.Item2));
            while (toProcess.Count > 0)
            {
                (int, int) currentCell = toProcess.Pop();
                List<(int, int)> unvisitedNeighbours = GetUnvisitedNeighbours(GetNeighbours(currentCell.Item1, currentCell.Item2));
                if (unvisitedNeighbours.Count != 0)
                {
                    toProcess.Push(currentCell);
                    int randomIndex = rand.Next(unvisitedNeighbours.Count);
                    (int, int) neighbour = unvisitedNeighbours[randomIndex];
                    RemoveWallBetweenCells(currentCell, neighbour);
                    Container[neighbour.Item1, neighbour.Item2].MarkVisited();
                    toProcess.Push((neighbour.Item1, neighbour.Item2));
                }
            }

        }

    }

}
