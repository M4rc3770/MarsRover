using CL.MarsRover.CustomException;
using CL.MarsRover.Enum;
using CL.MarsRover.Extension;


namespace CL.MarsRover.Domain
{
    public class Rover
    {
        /// <summary>
        /// The current position of the rover
        /// </summary>
        public Position CurrentPosition { get; private set; }

        public CommandReport CommandReport { get; private set; }
        private readonly GridAnalyzer _gridAnalyzer;

        public Rover(Position myPosition, Grid grid)
        {
            CurrentPosition = myPosition;
            _gridAnalyzer = new GridAnalyzer(grid);
            CommandReport = new CommandReport();
        }
        public Rover(int x, int y, ECardinalPoint direction, Grid grid) : this(new Position(x, y, direction), grid)
        { }
        public Rover(int x, int y, char direction, Grid grid) : this(new Position(x, y, direction), grid)
        { }

        /// <summary>
        /// Move the rover forward
        /// </summary>
        private void MoveForward()
        {
            Position nextPosition = _gridAnalyzer.NextPosition(CurrentPosition, false);
            Move(nextPosition);
        }

        /// <summary>
        /// Move the rover bacward
        /// </summary>
        private void MoveBackward()
        {
            Position nextPosition = _gridAnalyzer.NextPosition(CurrentPosition, true);
            Move(nextPosition);
        }

        /// <summary>
        /// turn the rover right
        /// </summary>
        private void TurnRight()
        { CurrentPosition.Facing = CurrentPosition.Facing.Righter(); }

        /// <summary>
        /// turn the rover left
        /// </summary>
        private void TurnLeft()
        { CurrentPosition.Facing = CurrentPosition.Facing.Lefter(); }

        /// <summary>
        /// execute commands
        /// </summary>
        /// <param name="commands">array of commands, admitted values: F,B,L,R</param>
        /// <exception cref="Exception">Command not recognized</exception>
        public void ExecuteCommand(IEnumerable<char> commands)
        {
            //validare comando
            //throw new Exception("Command not recognized, admitted values: F,B,L,R. The command values are case insensitive.");
            try
            {
                foreach (char c in commands)
                {

                    switch (char.ToUpper(c))
                    {
                        case 'F':
                            MoveForward();
                            break;
                        case 'B':
                            MoveBackward();
                            break;
                        case 'L':
                            TurnLeft();
                            break;
                        case 'R':
                            TurnRight();
                            break;
                    }
                }
            }
            catch (ObstacleDetectedExceptions exception)
            {
                CommandReport = new CommandReport()
                {
                    ObstacledDetected = true,
                    ObstacleGridPoint = exception.Gridpoint
                };
            }
        }

        /// <summary>
        /// Move the rover to the target position
        /// </summary>
        /// <param name="targetPosition">The target positin</param>
        /// <exception cref="ObstacleDetectedExceptions">The target position is occupied by an obstacle</exception>
        private void Move(Position targetPosition)
        {
            _gridAnalyzer.CanMove(targetPosition);
            CurrentPosition = targetPosition;
        }
    }
}