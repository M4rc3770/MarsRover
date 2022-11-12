namespace CL.MarsRover.Domain
{
    public class Grid
    {
        public int X_Max { get; private set; }
        public int Y_Max { get; private set; }
        public IEnumerable<(int, int)>? Obstacles { get; private set; }

        public Grid()
        {
            X_Max = 99;
            Y_Max = 99;
        }

        public Grid(int x, int y, IEnumerable<(int, int)>? obstacles)
        {
            if (x <= 0)
                throw new ArgumentException("x must be grather than zero");
            if (y <= 0)
                throw new ArgumentException("y must be grather than zero");

            X_Max = x;
            Y_Max = y;
            Obstacles = obstacles;
        }

        public Grid(int x, int y, int nObstacles) : this(x, y, null)
        {
            if (nObstacles <= 0)
                throw new ArgumentException("nObstacles must be grather or equal to zero");

            Obstacles = new List<(int, int)>(nObstacles);

            for (int i = 0; i < nObstacles; i++)
            {
                Random rand = new();
                int a = rand.Next(0, X_Max);
                int b = rand.Next(0, Y_Max);
                Obstacles.ToList().Add((a, b));
            }
        }
    }
}
