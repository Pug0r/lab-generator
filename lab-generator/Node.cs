namespace lab_generator
{
    static class Constants
    {
        public const int SSL = 20; // Square side length
        public const int StartingComplexity = 15;
    }
    internal class Node
    {
        public bool UpWall { get; private set; }
        public bool RightWall { get; private set; }
        public bool DownWall { get; private set; }
        public bool LeftWall { get; private set; }
        public bool IsVisited { get; private set; }

        public void DeleteWall(char wallPosition)
        {
            switch (wallPosition)
            {
                case 'l':
                    this.LeftWall = false;
                    break;
                case 'r':
                    this.RightWall = false;
                    break;
                case 'u':
                    this.UpWall = false;
                    break;
                case 'd':
                    this.DownWall = false;
                    break;
                default:
                    break;
            }
        }
        public void MarkVisited()
        {
            this.IsVisited = true;
        }
        public Node() { UpWall = true; RightWall = true; DownWall = true; LeftWall = true; IsVisited = false; }
    }
}
