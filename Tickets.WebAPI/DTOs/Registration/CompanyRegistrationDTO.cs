namespace Tickets.WebAPI.DTOs
{
    public class CompanyRegistrationDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public FirstUserDTO User { get; set; }
    }
}