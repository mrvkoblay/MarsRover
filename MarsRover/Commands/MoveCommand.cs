using MarsRover.Abstracts;
using MarsRover.Models;

namespace MarsRover.Commands
{
    public class MoveCommand : ICommand
    {
        public CommandType CommandType { get; set; }

        public MoveCommand()
        {
            CommandType = CommandType.MOVE;
        }

        public CommandResult Execute(Position currentPos, Orientations currentOri)
        {
            Position pos;

            switch (currentOri)
            {
                case Orientations.NORTH:
                    pos = currentPos.GoNorth;
                    break;

                case Orientations.SOUTH:
                    pos = currentPos.GoSouth;
                    break;

                case Orientations.WEST:
                    pos = currentPos.GoWest;
                    break;

                case Orientations.EAST:
                    pos = currentPos.GoEast;
                    break;

                default:
                    pos = new Position(-1, -1);
                    break;
            }

            return new CommandResult(pos, currentOri);
        }
    }
}
