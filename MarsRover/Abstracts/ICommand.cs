using MarsRover.Models;

namespace MarsRover.Abstracts
{
    public interface ICommand
    {
        CommandType CommandType { get; set; }
        CommandResult Execute(Position currentPos, Orientations currentOri);
    }
}
