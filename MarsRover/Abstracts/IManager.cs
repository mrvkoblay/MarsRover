using MarsRover.Models;
using System.Collections.Generic;

namespace MarsRover.Abstracts
{
    public interface IManager
    {
        Position Position { get; set; }
        Orientations Forward { get; set; }
        Plateau Plateau { get; set; }

        void Execute(Queue<ICommand> commands);
    }
}
