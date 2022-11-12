using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MarsRoverTests")]
namespace CL.MarsRover.Domain
{
    internal static class SurplusReducer
    {
        /// <summary>
        /// The input value 'x' is reduced to an integer value 'y' of the interval [0, max-1] as if the values of the interval itself were repeated indefinitely.
        /// Ex. con max=4  
        /// x=-5 => y=3
        /// x=-4 => y=0
        /// x=-3 => y=1
        /// x=-2 => y=2
        /// x=-1 => y=3
        /// x= 0 => y=0
        /// x= 1 => y=1
        /// x= 2 => y=2 
        /// x= 3 => y=3 
        /// x= 4 => y=0
        /// x= 5 => y=1
        /// x= 6 => y=2
        /// x= 7 => y=3
        /// </summary>
        /// <param name="x"></param>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public static int Reduce(int x, int dimension)
        {
            int y = x % dimension;

            if (x < 0)
                y += dimension;

            return y;
        }
    }
}