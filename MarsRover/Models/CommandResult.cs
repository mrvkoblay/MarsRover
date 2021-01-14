namespace MarsRover.Models
{
    public class CommandResult
    {
        public Position Position { get; }
        public Orientations Orientation { get; }

        public CommandResult(Position pos, Orientations ori)
        {
            Position = pos;
            Orientation = ori;
        }
    }
}
