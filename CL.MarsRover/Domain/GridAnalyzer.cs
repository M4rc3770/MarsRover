using CL.MarsRover.CustomException;
using CL.MarsRover.Domain;
using CL.MarsRover.Enum;
using CL.MarsRover.Extension;

namespace CL.MarsRover
{
    internal class GridAnalyzer
    {
        private readonly Grid _map;
  
        internal GridAnalyzer(Grid Map)
        {
            _map = Map;
        }

        /// <summary>
        /// check if the position is free, if not it raises an ObstacleDetectedExceptions
        /// </summary>
        /// <param name="positionToCheck">position to check</param>
        /// <exception cref="ObstacleDetectedExceptions"></exception>
        internal void CanMove(Position positionToCheck)
        {
           if (_map.Obstacles != null && _map.Obstacles.Contains((positionToCheck.GridPoint.X, positionToCheck.GridPoint.Y)))
                throw new ObstacleDetectedExceptions(positionToCheck.GridPoint);
        }

        /// <summary>
        /// determines the subsequent position
        /// </summary>
        /// <param name="currentPosition">Current position</param>
        /// <param name="isBackwards">indicates whether it is moving backwards</param>
        /// <returns></returns>
        internal Position NextPosition(Position currentPosition, bool isBackwards)
        {
            Position NextPosition = new(currentPosition.GridPoint.X, currentPosition.GridPoint.Y, currentPosition.Facing);
            
            switch (isBackwards ? currentPosition.Facing.Opposite() : currentPosition.Facing)
            {
                case ECardinalPoint.North:
                    NextPosition.GridPoint.Y++;
                    break;
                case ECardinalPoint.South:
                    NextPosition.GridPoint.Y--;
                    break;
                case ECardinalPoint.East:
                    NextPosition.GridPoint.X++;
                    break;
                case ECardinalPoint.West:
                    NextPosition.GridPoint.X--;
                    break;
            }

            NextPosition.GridPoint.X = SurplusReducer.Reduce(NextPosition.GridPoint.X, _map.X_Max+1);
            NextPosition.GridPoint.Y = SurplusReducer.Reduce(NextPosition.GridPoint.Y, _map.Y_Max+1);

            return NextPosition;
        }
    }
}
