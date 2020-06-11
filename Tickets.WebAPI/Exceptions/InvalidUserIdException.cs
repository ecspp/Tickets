using System;
namespace Tickets.WebAPI.Exceptions
{
    public class InvalidUserIdException : Exception
    {
        public InvalidUserIdException(string message) : base(message)
        {
        }

        public InvalidUserIdException()
        {
        }
    }
}