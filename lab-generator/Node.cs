namespace lab_generator
{
    static class Constants
    {
        public const int SSL = 20;
    }
    internal class Node
    {
        // 0 - no edge, 1 removable edge, 2 permanent edge
        public int Up { get; private set; }
        public int Right { get; private set; }
        public int Down { get; private set; }
        public int Left { get; private set; }
        public bool Visited { get; private set; }

        public void RemovePermanent(char wallPosition)
        {
            switch (wallPosition)
            {
                case 'l':
                    this.Left = 0;
                    break;
                case 'r':
                    this.Right = 0;
                    break;
                case 'u':
                    this.Up = 0;
                    break;
                case 'd':
                    this.Down = 0;
                    break;
                default:
                    break;
            }
        }
        public void SetWallPermanent(char wallPosition)
        {
            switch (wallPosition)
            {
                case 'l':
                    this.Left = 2;
                    break;
                case 'r':
                    this.Right = 2;
                    break;
                case 'u':
                    this.Up = 2;
                    break;
                case 'd':
                    this.Down = 2;
                    break;
                default:
                    break;
            }
        }
        public void DeleteWall(char wallPosition)
        {
            switch (wallPosition)
            {
                case 'l':
                    if (this.Left != 2)
                        this.Left = 0;
                    break;
                case 'r':
                    if (this.Right != 2)
                        this.Right = 0;
                    break;
                case 'u':
                    if (this.Up != 2)
                        this.Up = 0;
                    break;
                case 'd':
                    if (this.Down != 2)
                        this.Down = 0;
                    break;
                default:
                    break;
            }
        }
        public void MarkVisited()
        {
            this.Visited = true;
        }
        public Node() { Up = 1; Right = 1; Down = 1; Left = 1; Visited = false; }
    }
}
