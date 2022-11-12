namespace CL.MarsRover.Domain
{
    public class GridPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public GridPoint(int x, int y)
        {
            if (x < 0)
                throw new ArgumentException("x must be grather than zero");
            if (y < 0)
                throw new ArgumentException("y must be grather than zero");
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"x:{X}, y:{Y}";
        }

        public override bool Equals(object? obj)
        {
            try
            {
                if (obj == null)
                    return this == null;
                else
                {
                    GridPoint objIn = (GridPoint)obj;
                    return X == objIn.X && Y == objIn.Y;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}