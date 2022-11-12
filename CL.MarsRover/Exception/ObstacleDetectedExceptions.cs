using CL.MarsRover.Domain;

namespace CL.MarsRover.CustomException
{
    public class ObstacleDetectedExceptions : Exception
    {
        private const string _mex = "Detected obstacle at: ";
        public GridPoint? Gridpoint { get; private set; }

        public ObstacleDetectedExceptions(GridPoint gridpoint) : base($"{_mex} {gridpoint}")
        { Gridpoint = gridpoint; }

        public ObstacleDetectedExceptions(GridPoint gridpoint, Exception? innerException) : base($"{_mex} {gridpoint}", innerException)
        { }

    }
}
