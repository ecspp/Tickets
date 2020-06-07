using System.Collections.Generic;

namespace Tickets.WebAPI.Contracts.v1.Responses
{
    public class TicketUpdateResponse
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}