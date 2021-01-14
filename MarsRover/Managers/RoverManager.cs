using MarsRover.Abstracts;
using MarsRover.Models;
using System;
using System.Collections.Generic;

namespace MarsRover.Managers
{
    public class RoverManager : IManager
    {
        public Orientations Forward { get; set; }
        public Position Position { get; set; }
        public Plateau Plateau { get; set; }

        public RoverManager(Position initialPosition, Orientations ori, Plateau pla)
        {
            Position = initialPosition;
            Forward = ori;
            Plateau = pla;
        }

        public void Execute(Queue<ICommand> commands)
        {
            while (commands.Count > 0)
            {
                var cmd = commands.Dequeue();
                var result = cmd.Execute(Position, Forward);
                var onMap = true;

                if (cmd.CommandType == CommandType.MOVE)
                    onMap = Plateau.IsOnMap(result.Position.X, result.Position.Y);

                if (onMap)
                {
                    SetNewPosition(new Position(result.Position.X, result.Position.Y));
                    SetNewRotate(result.Orientation);
                }
                else
                {
                    Console.WriteLine($"Destination {result.Position} is not on the map.");
                }
            }
        }

        private void SetNewPosition(Position destination)
        {
            if (Position.CompareTo(destination) != 0)
                Position = destination;
        }

        private void SetNewRotate(Orientations ori)
        {
            if (Forward != ori)
                Forward = ori;
        }
    }
}
