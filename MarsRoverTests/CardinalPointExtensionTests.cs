using CL.MarsRover.Enum;
using CL.MarsRover.Extension;

namespace MarsRoverTests
{
    internal class CardinalPointExtensionTests
    {

        [Test, Category("Happy")]
        [TestCase(ECardinalPoint.North)]
        [TestCase(ECardinalPoint.South)]
        [TestCase(ECardinalPoint.East)]
        [TestCase(ECardinalPoint.West)]
        public void Get_Righter_OK(ECardinalPoint direction)
        {
            var outputDirection = direction.Righter();
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


            Assert.That(outputDirection, Is.EqualTo(expectedDirection));
        }


        [Test, Category("Happy")]
        [TestCase(ECardinalPoint.North)]
        [TestCase(ECardinalPoint.South)]
        [TestCase(ECardinalPoint.East)]
        [TestCase(ECardinalPoint.West)]
        public void Get_Lefter_OK(ECardinalPoint direction)
        {
            var outputDirection = direction.Lefter();
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

            Assert.That(outputDirection, Is.EqualTo(expectedDirection));
        }

        [Test, Category("Happy")]
        [TestCase(ECardinalPoint.North)]
        [TestCase(ECardinalPoint.South)]
        [TestCase(ECardinalPoint.East)]
        [TestCase(ECardinalPoint.West)]

        public void Get_Opposite_OK(ECardinalPoint direction)
        {
            var outputDirection = direction.Opposite();
            ECardinalPoint expectedDirection;
            switch (direction)
            {
                case ECardinalPoint.North:
                    expectedDirection = ECardinalPoint.South;
                    break;
                case ECardinalPoint.West:
                    expectedDirection = ECardinalPoint.East;
                    break;
                case ECardinalPoint.South:
                    expectedDirection = ECardinalPoint.North;
                    break;
                case ECardinalPoint.East:
                    expectedDirection = ECardinalPoint.West;
                    break;
                default:
                    expectedDirection = ECardinalPoint.North;
                    Assert.Fail();
                    break;
            }

            Assert.That(outputDirection, Is.EqualTo(expectedDirection));
        }
    }
}
