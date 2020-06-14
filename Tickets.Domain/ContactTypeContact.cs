namespace Tickets.Domain
{
    public class ContactTypeContact
    {
        public int? ContactId { get; set; }
        public virtual Contact Contact { get; set; }
        public int ContactTypeId { get; set; }
        public virtual ContactType ContactType { get; set; }
    }
}