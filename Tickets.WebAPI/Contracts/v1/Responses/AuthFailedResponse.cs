using System.Collections;
namespace Tickets.WebAPI.Contracts.v1.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable Errors { get; set; }
    }
}