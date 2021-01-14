using MarsRover.Abstracts;
using MarsRover.Commands;
using MarsRover.Exceptions;
using MarsRover.Managers;
using MarsRover.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    public class Program
    {
        static void Main(string[] args)
        {
            var mapInput = GetMapCoordinates();
            var map = new Plateau(mapInput.X, mapInput.Y);

            var running = true;
            while (running)
            {
                var positon = GetRoverPositionInput(validation: map.IsOnMap);
                var mngr = new RoverManager(positon.Item1, positon.Item2, map);

                var commands = GetMoves();

                mngr.Execute(commands);

                Console.WriteLine($"Rover's Last Position: {mngr.Position} and Orientation: {mngr.Forward}");
                Console.WriteLine("--------------------------");

                running = AskIsComplete();
            }

            Console.WriteLine("--------------------------");
        }

        #region PLATEAU
        public static Position GetMapCoordinates()
        {
            var answer = Ask("Enter Plateau Sizes");

            Position coords;
            try
            {
                coords = ParseMapCoordinateInput(answer);
            }
            catch (InvalidUserInputException ex)
            {
                Console.WriteLine(ex.Message);
                return GetMapCoordinates();
            }

            return coords;
        }

        public static Position ParseMapCoordinateInput(string input)
        {
            var parts = input.Split(" ")
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .ToArray();

            if (parts.Length != 2)
                throw new InvalidUserInputException(
                    "Input should be space delimited and only contain 2 parts. (i.e. 4 5)"
                );

            var canParseX = int.TryParse(parts[0].Trim(), out var x);
            var canParseY = int.TryParse(parts[1].Trim(), out var y);

            if (!canParseX || !canParseY)
                throw new InvalidUserInputException(
                    "Input parts must only contain numbers. (i.e. 4 5)"
                );

            return new Position(x, y);
        }
        #endregion
       
        #region ROVERS POSITION
        public static (Position, Orientations) GetRoverPositionInput(Func<int, int, bool> validation)
        {
            var input = Ask("Enter Rover Position");

            (Position, Orientations) roverPosition;

            try
            {
                roverPosition = ParseRoverPositionInput(input);

                if (!validation(roverPosition.Item1.Item1, roverPosition.Item1.Item2))
                    throw new InvalidUserInputException("Rover's position was not on the map.");

            }
            catch (InvalidUserInputException ex)
            {
                Console.WriteLine(ex.Message);
                return GetRoverPositionInput(validation);
            }

            return roverPosition;
        }

        public static (Position, Orientations) ParseRoverPositionInput(string input)
        {
            var parts = input.Split(" ")
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .ToArray();

            if (parts.Length != 3)
                throw new InvalidUserInputException("Input should be space delimited and contain 3 parts. (i.e. 4 5 N)");

            var canParseX = int.TryParse(parts[0], out var x);
            var canParseY = int.TryParse(parts[1], out var y);

            if (!canParseX || !canParseY)
                throw new InvalidUserInputException("First 2 inputs must be numbers. (i.e. 4 5 N)");

            var validDirections = new List<string>() { "n", "s", "e", "w" };

            var ori = parts[2].ToLower();

            if (!validDirections.Contains(ori))
            {
                throw new InvalidUserInputException("Direction must be N, S, E, or W. (i.e. 4 5 N)");
            }

            Orientations facing = Orientations.UNKNOWN;
            switch (ori)
            {
                case "n":
                    facing = Orientations.NORTH;
                    break;
                case "s":
                    facing = Orientations.SOUTH;
                    break;
                case "e":
                    facing = Orientations.EAST;
                    break;
                case "w":
                    facing = Orientations.WEST;
                    break;
            }

            if (facing == Orientations.UNKNOWN)
                throw new InvalidUserInputException("Cannot detect proper facing direction.");

            return (new Position(x, y), facing);
        }
        #endregion

        #region MOVES
        public static Queue<ICommand> GetMoves()
        {
            var input = Ask($"Enter Moves");

            try
            {
                return ParseMovementPlan(input);
            }
            catch (InvalidUserInputException ex)
            {
                Console.WriteLine(ex.Message);
                return GetMoves();
            }
        }

        public static Queue<ICommand> ParseMovementPlan(string input)
        {
            var inputs = input.Trim();
            var validCommands = new List<char> { 'L', 'R', 'M' };

            var invalidChars = inputs.Except(validCommands);

            if (invalidChars.Count() > 0 || String.IsNullOrEmpty(inputs) || String.IsNullOrWhiteSpace(inputs))
                throw new InvalidUserInputException("Commands may only contain L, R, and M.");

            var commands = new Queue<ICommand>();

            foreach (var c in inputs)
            {
                switch (c)
                {
                    case 'L':
                        commands.Enqueue(new TurnLeftCommand());
                        break;
                    case 'R':
                        commands.Enqueue(new TurnRightCommand());
                        break;
                    case 'M':
                        commands.Enqueue(new MoveCommand());
                        break;
                }
            }

            return commands;
        }

        #endregion

        private static string Ask(string question)
        {
            Console.Write($"{question}: ");
            var answer = Console.ReadLine();
            return answer;
        }

        private static bool AskIsComplete()
        {
            var input = Ask("Keep going? Y/n");
            var answer = input.Trim();

            if (answer != "Y" && answer != "n")
            {
                Console.WriteLine("Incorrect input. Try again.");
                return AskIsComplete();
            }

            if (answer == "Y")
                return true;

            if (answer == "n")
                return false;

            return false;
        }
    }
}