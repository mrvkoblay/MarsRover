using MarsRover.Abstracts;
using MarsRover.Models;

namespace MarsRover.Commands
{
    public class TurnLeftCommand : ICommand
    {
        public CommandType CommandType { get; set; }

        public TurnLeftCommand()
        {
            CommandType = CommandType.ROTATE;
        }

        public CommandResult Execute(Position currentPos, Orientations currentOri)
        {
            var newOrientation = Orientations.UNKNOWN;

            switch (currentOri)
            {
                case Orientations.NORTH:
                    newOrientation = Orientations.WEST;
                    break;
                case Orientations.WEST:
                    newOrientation = Orientations.SOUTH;
                    break;
                case Orientations.SOUTH:
                    newOrientation = Orientations.EAST;
                    break;
                case Orientations.EAST:
                    newOrientation = Orientations.NORTH;
                    break;
            }

            return new CommandResult(currentPos, newOrientation);
        }
    }
}
