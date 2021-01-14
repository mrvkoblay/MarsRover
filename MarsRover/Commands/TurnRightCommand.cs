using MarsRover.Abstracts;
using MarsRover.Models;

namespace MarsRover.Commands
{
    public class TurnRightCommand : ICommand
    {
        public CommandType CommandType { get; set; }

        public TurnRightCommand()
        {
            CommandType = CommandType.ROTATE;
        }

        public CommandResult Execute(Position currentPos, Orientations currentOri)
        {
            var newOrientation = Orientations.UNKNOWN;

            switch (currentOri)
            {
                case Orientations.NORTH:
                    newOrientation = Orientations.EAST;
                    break;
                case Orientations.WEST:
                    newOrientation = Orientations.NORTH;
                    break;
                case Orientations.SOUTH:
                    newOrientation = Orientations.WEST;
                    break;
                case Orientations.EAST:
                    newOrientation = Orientations.SOUTH;
                    break;
            }

            return new CommandResult(currentPos, newOrientation);
        }
    }
}
