namespace Tickets.Domain
{
    public class TicketContact
    {
        public int? ContactId { get; set; }
        public virtual Contact Contact { get; set; }
        public long TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}