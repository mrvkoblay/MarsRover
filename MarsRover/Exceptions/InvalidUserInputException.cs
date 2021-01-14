using System;

namespace MarsRover.Exceptions
{
    public class InvalidUserInputException : Exception
    {
        public InvalidUserInputException(string message) : base(message)
        {

        }
    }
}
