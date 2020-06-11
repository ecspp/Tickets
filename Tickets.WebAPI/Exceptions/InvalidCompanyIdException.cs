using System;
namespace Tickets.WebAPI.Exceptions
{
    public class InvalidCompanyIdException : Exception
    {
        public InvalidCompanyIdException(string message) : base(message)
        {
        }

        public InvalidCompanyIdException()
        {
        }
    }
}