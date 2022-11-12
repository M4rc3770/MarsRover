using CL.MarsRover.Domain;
using CL.MarsRover.Enum;

namespace MarsRoverTests
{
    public class Tests
    {
        private Rover? _rover;
        private Grid _grid;

        [SetUp]
        public void Setup()
        {
            /*
             * 9 . . . # . . . . . . 
             * 8 . . . . . . . . . . 
             * 7 . . . . . . . # . . 
             * 6 . . . . . . . . . . 
             * 5 . . . . . . . . . . 
             * 4 . . . . . . . . . . 
             * 3 . . . . . . . . . . 
             * 2 . . . . . . . . . . 
             * 1 . # . . . . . . . . 
             * 0 . . . . . . . . . . 
             *   0 1 2 3 4 5 6 7 8 9
             *   
             *  '.' void poisition
             *  '#' position  occupied by an obstacle
             */
            _grid = new(9, 9, new List<(int, int)>
            {
                (1, 1),
                (3, 9),
                (7, 7),
            });
        }

        [Test, Category("Happy")]
        [TestCase(ECardinalPoint.North)]
        [TestCase(ECardinalPoint.South)]
        [TestCase(ECardinalPoint.East)]
        [TestCase(ECardinalPoint.West)]
        public void TestTurnRight_OK(ECardinalPoint direction)
        {
            int x = 0, y = 0;
            var myRover = InitializeRover(0, 0, direction);

            myRover.ExecuteCommand(new char[] { 'R' });

            ECardinalPoint expectedDirection;
            switch (direction)
            {
                case ECardinalPoint.North:
                    expectedDirection = ECardinalPoint.East;
                    break;
                case ECardinalPoint.East:
                    expectedDirection = ECardinalPoint.South;
                    break;
                case ECardinalPoint.South:
                    expectedDirection = ECardinalPoint.West;
                    break;
                case ECardinalPoint.West:
                    expectedDirection = ECardinalPoint.North;
                    break;
                default:
                    expectedDirection = ECardinalPoint.North;
                    Assert.Fail();
                    break;
            }

            Assert.Multiple(() =>
            {
                Assert.That(myRover.CurrentPosition.Facing, Is.EqualTo(expectedDirection));
                Assert.That(myRover.CurrentPosition.GridPoint.X, Is.EqualTo(x));
                Assert.That(myRover.CurrentPosition.GridPoint.Y, Is.EqualTo(y));
                Assert.That(myRover.CommandReport.ObstacledDetected, Is.EqualTo(false));
            });
        }

        [Test, Category("Happy")]
        [TestCase(ECardinalPoint.North)]
        [TestCase(ECardinalPoint.South)]
        [TestCase(ECardinalPoint.East)]
        [TestCase(ECardinalPoint.West)]
        public void TestTurnLeft_OK(ECardinalPoint direction)
        {
            int x = 0, y = 0;
            var myRover = InitializeRover(x, y, direction);

            myRover.ExecuteCommand(new char[] { 'L' });

            ECardinalPoint expectedDirection;
            switch (direction)
            {
                case ECardinalPoint.North:
                    expectedDirection = ECardinalPoint.West;
                    break;
                case ECardinalPoint.West:
                    expectedDirection = ECardinalPoint.South;
                    break;
                case ECardinalPoint.South:
                    expectedDirection = ECardinalPoint.East;
                    break;
                case ECardinalPoint.East:
                    expectedDirection = ECardinalPoint.North;
                    break;
                default:
                    expectedDirection = ECardinalPoint.North;
                    Assert.Fail();
                    break;
            }

            Assert.Multiple(() =>
            {
                Assert.That(myRover.CurrentPosition.Facing, Is.EqualTo(expectedDirection));
                Assert.That(myRover.CurrentPosition.GridPoint.X, Is.EqualTo(x));
                Assert.That(myRover.CurrentPosition.GridPoint.Y, Is.EqualTo(y));
                Assert.That(myRover.CommandReport.ObstacledDetected, Is.EqualTo(false));
            });
        }

        [Test, Category("Happy")]
        [TestCase(1, 1, ECardinalPoint.North, 1, 2)]
        [TestCase(1, 1, ECardinalPoint.South, 1, 0)]
        [TestCase(1, 1, ECardinalPoint.East, 2, 1)]
        [TestCase(1, 1, ECardinalPoint.West, 0, 1)]
        public void MoveForward_OK(int initialX, int initialY, ECardinalPoint initialFacing, int finalX, int finalY)
        {
            var myRover = InitializeRover(initialX, initialY, initialFacing);

            myRover.ExecuteCommand(new char[] { 'F' });

            Assert.Multiple(() =>
            {
                Assert.That(myRover.CurrentPosition.Facing, Is.EqualTo(initialFacing));
                Assert.That(myRover.CurrentPosition.GridPoint.X, Is.EqualTo(finalX));
                Assert.That(myRover.CurrentPosition.GridPoint.Y, Is.EqualTo(finalY));
                Assert.That(myRover.CommandReport.ObstacledDetected, Is.EqualTo(false));
            });
        }

        [Test, Category("Happy")]
        [TestCase(1, 1, ECardinalPoint.North, 1, 0)]
        [TestCase(1, 1, ECardinalPoint.South, 1, 2)]
        [TestCase(1, 1, ECardinalPoint.East, 0, 1)]
        [TestCase(1, 1, ECardinalPoint.West, 2, 1)]

        public void MoveBack_OK(int initialX, int initialY, ECardinalPoint initialFacing, int finalX, int finalY)
        {
            var myRover = InitializeRover(initialX, initialY, initialFacing);

            myRover.ExecuteCommand(new char[] { 'B' });

            Assert.Multiple(() =>
            {
                Assert.That(myRover.CurrentPosition.Facing, Is.EqualTo(initialFacing));
                Assert.That(myRover.CurrentPosition.GridPoint.X, Is.EqualTo(finalX));
                Assert.That(myRover.CurrentPosition.GridPoint.Y, Is.EqualTo(finalY));
                Assert.That(myRover.CommandReport.ObstacledDetected, Is.EqualTo(false));
            });
        }

        [Test, Category("Happy")]
        [TestCase(7, 6, ECardinalPoint.North)]
        public void DetectObstacle_OK(int initialX, int initialY, ECardinalPoint initialFacing)
        {
            Rover myRover = InitializeRover(initialX, initialY, initialFacing);
            Position initialPosition = new(initialX, initialY, initialFacing);

            myRover.ExecuteCommand(new char[] { 'F' });

            Assert.Multiple(() =>
            {
                Assert.That(myRover.CommandReport.ObstacledDetected, Is.EqualTo(true));
                Assert.That(myRover.CommandReport.ObstacleGridPoint, Is.EqualTo(new GridPoint(7, 7)));

            });
        }


        /*
         * 9 . . . # . . . . . . 
         * 8 . . . . . . . . . . 
         * 7 . . . . . . . # . . 
         * 6 . . . . . . . . . . 
         * 5 . . . . . . . . . . 
         * 4 . . . . . . . . . . 
         * 3 . . . . . . . . . . 
         * 2 ┌ ┐ . . . . . . . .  
         * 1 | # . . . . . . . . 
         * 0 ^ . . . . . . . . . 
         *   0 1 2 3 4 5 6 7 8 9
         *   
         *  '.' void poisition
         *  '#' position  occupied by an obstacle
         */
        [Test, Category("Happy")]
        [TestCase(0, 0, ECardinalPoint.North, "FFRFRFFLFFF")]
        public void ExecuteCommand_DetectObstacle_at_1_1(int initialX, int initialY, ECardinalPoint initialFacing, string command)
        {
            Rover myRover = InitializeRover(initialX, initialY, initialFacing);
            myRover.ExecuteCommand(command);

            Assert.Multiple(() =>
            {
                Assert.That(myRover.CurrentPosition.Facing, Is.EqualTo(ECardinalPoint.South));
                Assert.That(myRover.CurrentPosition.GridPoint.X, Is.EqualTo(1));
                Assert.That(myRover.CurrentPosition.GridPoint.Y, Is.EqualTo(2));
                Assert.That(myRover.CommandReport.ObstacledDetected, Is.EqualTo(true));
                Assert.That(myRover.CommandReport.ObstacleGridPoint, Is.EqualTo(new GridPoint(1, 1)));

            });
        }

        /*
         * 9 . . . # . . . . . . 
         * 8 . . . . . . . . . . 
         * 7 . . . . . . . # . . 
         * 6 . . . . . . . . . . 
         * 5 . . . . . . . . . . 
         * 4 . . . ┌ ┐ . . . . . 
         * 3 . . . | | . . . . . 
         * 2 . . . ^ └ - - > . . 
         * 1 . # . . . . . . . . 
         * 0  . . . . . . . . . 
         *   0 1 2 3 4 5 6 7 8 9
         *   
         *  '.' void poisition
         *  '#' position  occupied by an obstacle
         *  
         */
        [Test, Category("Happy")]
        [TestCase(3, 2, ECardinalPoint.North, "FFRFRFFLFFF")]
        public void ExecuteCommand_Ok(int initialX, int initialY, ECardinalPoint initialFacing, string command)
        {
            Rover myRover = InitializeRover(initialX, initialY, initialFacing);
            myRover.ExecuteCommand(command);

            Assert.Multiple(() =>
            {
                Assert.That(myRover.CurrentPosition.Facing, Is.EqualTo(ECardinalPoint.East));
                Assert.That(myRover.CurrentPosition.GridPoint.X, Is.EqualTo(7));
                Assert.That(myRover.CurrentPosition.GridPoint.Y, Is.EqualTo(2));
                Assert.That(myRover.CommandReport.ObstacledDetected, Is.EqualTo(false));
            });
        }

        [Test, Category("Happy")]
        [TestCase(5, 9, ECardinalPoint.North, 5, 0)]
        [TestCase(5, 0, ECardinalPoint.South, 5, 9)]
        [TestCase(9, 5, ECardinalPoint.East, 0, 5)]
        [TestCase(0, 5, ECardinalPoint.West, 9, 5)]
        public void Wrapper(int initialX, int initialY, ECardinalPoint initialFacing, int finalX, int finalY)
        {
            Rover myRover = InitializeRover(initialX, initialY, initialFacing);
            myRover.ExecuteCommand(new char[] { 'F' });

            Assert.Multiple(() =>
            {
                Assert.That(myRover.CurrentPosition.Facing, Is.EqualTo(initialFacing));
                Assert.That(myRover.CurrentPosition.GridPoint.X, Is.EqualTo(finalX));
                Assert.That(myRover.CurrentPosition.GridPoint.Y, Is.EqualTo(finalY));
                Assert.That(myRover.CommandReport.ObstacledDetected, Is.EqualTo(false));
            });
        }


        [Test, Category("Sad")]
        [TestCase("AAA")]
        public void WrongCommnd(string command)
        {
            Rover myRover = InitializeRover(0, 0, ECardinalPoint.North);
            try
            {
                myRover.ExecuteCommand(command);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Command not recognized, admitted values: F,B,L,R. The command values are case insensitive."));
            }
        }

        [Test, Category("Sad")]
        [TestCase(1, 1, ECardinalPoint.North, "FFRFRFFLFFF")]
        public void ExecuteCommand_KO(int initialX, int initialY, ECardinalPoint initialFacing, string command)
        {
            Rover myRover = InitializeRover(initialX, initialY, initialFacing);
            myRover.ExecuteCommand(command);

            Assert.Multiple(() =>
            {
                Assert.That(myRover.CurrentPosition.Facing, Is.EqualTo(ECardinalPoint.East));
                Assert.That(myRover.CurrentPosition.GridPoint.X, Is.EqualTo(5));
                Assert.That(myRover.CurrentPosition.GridPoint.Y, Is.EqualTo(1));
                Assert.That(myRover.CommandReport.ObstacledDetected, Is.EqualTo(false));
            });
        }

        [Test, Category("Sad")]
        [TestCase("Z")]
        public void WrongInitialCardinalPosition(string command)
        {
            try
            {
                Rover myRover = new Rover(0, 0, 'Z', _grid);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Direction not recognized, admitted values: N,S,E,W. The values are case-insensitive."));
            }
        }

        #region privateMethods

        private Rover InitializeRover(int x, int y, ECardinalPoint direction)
        {
            _rover = new Rover(x, y, direction, _grid);
            return _rover;
        }


        #endregion
    }
}