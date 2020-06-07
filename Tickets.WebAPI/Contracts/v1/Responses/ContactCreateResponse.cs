namespace Tickets.WebAPI.Contracts.v1.Responses
{
    public class ContactCreateResponse
    {
        public bool Succes { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}