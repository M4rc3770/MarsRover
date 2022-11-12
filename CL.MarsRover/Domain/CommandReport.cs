namespace CL.MarsRover.Domain
{
    public class CommandReport
    {
        public bool ObstacledDetected { get; set; } = false;
        public GridPoint? ObstacleGridPoint { get; set; }

        public override string ToString()
        {
            return ObstacledDetected ? $"Command Aborted. Detected obstacle at {ObstacleGridPoint}." : "";
        }
    }
}