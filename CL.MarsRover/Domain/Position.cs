using CL.MarsRover.Enum;

namespace CL.MarsRover.Domain
{
    public class Position
    {
        public GridPoint GridPoint { get; set; }
        public ECardinalPoint Facing { get; set; }

        public Position(int x, int y, ECardinalPoint facing)
        {
            GridPoint = new GridPoint(x, y);
            Facing = facing;
        }

        public Position(int x, int y, char facing)
        {
            GridPoint = new GridPoint(x, y);

            Facing = char.ToUpper(facing) switch
            {
                'N' => ECardinalPoint.North,
                'S' => ECardinalPoint.South,
                'E' => ECardinalPoint.East,
                'W' => ECardinalPoint.West,
                _ => throw new Exception("Direction not recognized, admitted values: N,S,E,W. The values are case-insensitive."),
            };
        }

        public override string ToString() => $"{GridPoint}, facing:{Facing}";
    }
}