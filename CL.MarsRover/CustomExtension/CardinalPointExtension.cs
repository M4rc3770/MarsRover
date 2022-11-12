using CL.MarsRover.Domain;
using CL.MarsRover.Enum;

namespace CL.MarsRover.Extension
{
    public static class CardinalPointExtension
    {
        /// <summary>
        /// rotates the cardinal point by n steps
        /// </summary>
        /// <param name="cardinalPoint"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private static ECardinalPoint Rotate(ECardinalPoint cardinalPoint, int n)
        {
            int x = (int)cardinalPoint + n;
            return (ECardinalPoint)SurplusReducer.Reduce(x, 4);
        }

        /// <summary>
        /// gets the first item on the left - counterclockwise
        /// </summary>
        /// <param name="cardinalPoint"></param>
        /// <returns></returns>
        public static ECardinalPoint Lefter(this ECardinalPoint cardinalPoint)
        {
            return Rotate(cardinalPoint, -1);
        }
        /// <summary>
        /// gets the first item on the right - clockwise
        /// </summary>
        /// <param name="cardinalPoint"></param>
        /// <returns></returns>
        public static ECardinalPoint Righter(this ECardinalPoint cardinalPoint)
        {
            return Rotate(cardinalPoint, +1);
        }

        /// <summary>
        /// gets the opposite cardinal point
        /// </summary>
        /// <param name="cardinalPoint"></param>
        /// <returns></returns>
        public static ECardinalPoint Opposite(this ECardinalPoint cardinalPoint)
        {
            return Rotate(cardinalPoint, +2);
        }
    }
}