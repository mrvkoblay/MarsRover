using MarsRover.Exceptions;
using MarsRover.Managers;
using MarsRover.Models;
using Moq;
using NUnit.Framework;
using System;

namespace MarsRover.Test
{
    [TestFixture]
    public class RoverTests
    {
        Mock<Plateau> mockPlateau;

        [SetUp]
        public void Setup()
        {
            mockPlateau = new Mock<Plateau>(5, 5);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\n")]
        [TestCase("*")]
        [TestCase("6")]
        [TestCase("N")]
        [TestCase("6 N")]
        [TestCase("6 8 n")]
        [TestCase("6 8 T")]
        [TestCase("N 8 4")]
        [TestCase("l")]
        [TestCase("r")]
        [TestCase("m")]
        [TestCase("M F")]
        public void ParseMovementPlan_WrongCommandLetters_ReturnInvalidUserInputException(string commands)
        {
            try
            {
                var movements = Program.ParseMovementPlan(commands);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf(typeof(InvalidUserInputException), ex);
                return;
            }

            Assert.IsTrue(false);
        }

        [TestCase("L")]
        [TestCase("R")]
        [TestCase("M")]
        [TestCase("MML")]
        [TestCase("LR")]
        [TestCase("LRMLRMLRM")]
        public void ParseMovementPlan_TrueCommandLetters_ReturnTrue(string commands)
        {
            try
            {
                var movements = Program.ParseMovementPlan(commands);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf(typeof(InvalidUserInputException), ex);
                Assert.IsTrue(false);
            }
            Assert.IsTrue(true);
        }

        [TestCase(1, 5, 3, 3)]
        [TestCase(1, -2, 3, 3)]
        [TestCase(1, 2, 0, 0)]
        public void PlateauIsOnMap_InitialPositionIsNotOnMap_ReturnFalse(int startX, int startY, int width, int height)
        {
            mockPlateau = new Mock<Plateau>(width, height);

            var conn = mockPlateau.Object.IsOnMap(startX, startY);

            Assert.That(conn, Is.False);
        }

        [TestCase(1, 5, 5, 5)]
        [TestCase(0, 0, 5, 5)]
        [TestCase(5, 5, 5, 5)]
        public void PlateauIsOnMap_InitialPositionIsOnMap_ReturnTrue(int startX, int startY, int width, int height)
        {
            mockPlateau = new Mock<Plateau>(width, height);

            var conn = mockPlateau.Object.IsOnMap(startX, startY);

            Assert.That(conn, Is.True);
        }

        [TestCase(1, 1, Orientations.SOUTH, 1, 2, Orientations.NORTH, "R", "R", "M")]
        [TestCase(2, 2, Orientations.WEST, 2, 0, Orientations.SOUTH, "L", "M", "M")]
        [TestCase(1, 2, Orientations.NORTH, 1, 3, Orientations.NORTH, "L", "M", "L", "M", "L", "M", "L", "M", "M")]
        [TestCase(3, 3, Orientations.EAST, 5, 1, Orientations.EAST, "M", "M", "R", "M", "M", "R", "M", "R", "R", "M")]
        public void RoverMoveExecuteMoves_CheckMoves_ReturnTrue(int startX, int startY, Orientations startDirection, int expectedX, int expectedY, Orientations expectedDirection, params string[] moves)
        {
            var startPosition = new Position(startX, startY);
            var expectedPosition = new Position(expectedX, expectedY);

            var movements = Program.ParseMovementPlan(String.Join("", moves));

            var rover = new RoverManager(startPosition, startDirection, mockPlateau.Object);
            rover.Execute(movements);

            Assert.AreEqual(expectedPosition.X, rover.Position.X);
            Assert.AreEqual(expectedPosition.Y, rover.Position.Y);
            Assert.AreEqual(expectedDirection, rover.Forward);
        }

        [TestCase(3, 3, Orientations.EAST, 5, 1, Orientations.NORTH, "M", "M", "R", "M", "M", "R", "M", "R", "R", "M")]
        public void RoverMoveExecuteMoves_CheckMovesWrongOrientation(int startX, int startY, Orientations startDirection, int expectedX, int expectedY, Orientations expectedDirection, params string[] moves)
        {
            var startPosition = new Position(startX, startY);
            var expectedPosition = new Position(expectedX, expectedY);

            var movements = Program.ParseMovementPlan(String.Join("", moves));

            var rover = new RoverManager(startPosition, startDirection, mockPlateau.Object);
            rover.Execute(movements);

            Assert.AreEqual(expectedPosition.X, rover.Position.X);
            Assert.AreEqual(expectedPosition.Y, rover.Position.Y);
            Assert.AreNotEqual(expectedDirection, rover.Forward);
        }

        [TestCase(3, 3, Orientations.EAST, 4, 3, Orientations.EAST, "M", "M", "R", "M", "M", "R", "M", "R", "R", "M")]
        public void RoverMoveExecuteMoves_CheckMovesWrongPosition(int startX, int startY, Orientations startDirection, int expectedX, int expectedY, Orientations expectedDirection, params string[] moves)
        {
            var startPosition = new Position(startX, startY);
            var expectedPosition = new Position(expectedX, expectedY);

            var movements = Program.ParseMovementPlan(String.Join("", moves));

            var rover = new RoverManager(startPosition, startDirection, mockPlateau.Object);
            rover.Execute(movements);

            Assert.AreNotEqual(expectedPosition.X, rover.Position.X);
            Assert.AreNotEqual(expectedPosition.Y, rover.Position.Y);
            Assert.AreEqual(expectedDirection, rover.Forward);
        }

        [TestCase(3, 3, Orientations.EAST, 4, 3, Orientations.NORTH, "M", "M", "R", "M", "M", "R", "M", "R", "R", "M")]
        public void RoverMoveExecuteMoves_CheckMovesWrongPositionAndOrientations(int startX, int startY, Orientations startDirection, int expectedX, int expectedY, Orientations expectedDirection, params string[] moves)
        {
            var startPosition = new Position(startX, startY);
            var expectedPosition = new Position(expectedX, expectedY);

            var movements = Program.ParseMovementPlan(String.Join("", moves));

            var rover = new RoverManager(startPosition, startDirection, mockPlateau.Object);
            rover.Execute(movements);

            Assert.AreNotEqual(expectedPosition.X, rover.Position.X);
            Assert.AreNotEqual(expectedPosition.Y, rover.Position.Y);
            Assert.AreNotEqual(expectedDirection, rover.Forward);
        }
    }
}