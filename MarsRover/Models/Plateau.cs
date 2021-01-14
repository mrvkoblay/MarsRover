namespace MarsRover.Models
{
    public class Plateau
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Plateau(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public bool IsOnMap(int x, int y)
        {
            var isValidX = x >= 0 && x <= Width;
            var isValidY = y >= 0 && y <= Height;
            return isValidX && isValidY;
        }
    }
}
