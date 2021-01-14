using MarsRover.Exceptions;
using NUnit.Framework;

namespace MarsRover.Test
{
    [TestFixture]
    public class PlateauTests
    {
        [TestCase("5 4", 5, 4)]
        public void ParseMapCoordinateInput_CorrectFormat(string sizes, int width, int height)
        {
            var upperRight = Program.ParseMapCoordinateInput(sizes);

            var x = upperRight.Item1;
            var y = upperRight.Item2;

            Assert.AreEqual(x, width);
            Assert.AreEqual(y, height);
        }

        [TestCase("4")]
        public void ParseMapCoordinateInput_NotEnoughParts(string sizes)
        {
            AssertThrowsInvalidUserInputException(sizes);
        }

        [TestCase("3 2 4")]
        public void ParseMapCoordinateInput_TooManyParts(string sizes)
        {
            AssertThrowsInvalidUserInputException(sizes);
        }

        [TestCase("four 4")]
        public void ParseMapCoordinateInput_XNotNumberParsable(string sizes)
        {
            AssertThrowsInvalidUserInputException(sizes);
        }

        [TestCase("4 four")]
        public void ParseMapCoordinateInput_YNotNumberParsable(string sizes)
        {
            AssertThrowsInvalidUserInputException(sizes);
        }

        [TestCase("one two")]
        public void ParseMapCoordinateInput_NeitherNumberParsable(string sizes)
        {
            AssertThrowsInvalidUserInputException(sizes);
        }

        private void AssertThrowsInvalidUserInputException(string input)
        {
            //Assert.Throws<InvalidUserInputException>(() => Program.ParseMapCoordinateInput(input));
            try
            {
                var upperRight = Program.ParseMapCoordinateInput(input);
            }
            catch (InvalidUserInputException ex)
            {
                Assert.IsInstanceOf(typeof(InvalidUserInputException), ex);
                return;
            }

            Assert.IsTrue(false);
        }
    }
}