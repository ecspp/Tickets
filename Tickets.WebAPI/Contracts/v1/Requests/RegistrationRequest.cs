using System.Security.Authentication.ExtendedProtection;
using System.ComponentModel.DataAnnotations;

namespace Tickets.WebAPI.Contracts.v1.Requests
{
    public class RegistrationRequest
    {
        [Required]
        public CompanyRegistrationDTO Company { get; set; }
        [Required]
        public CompanyUserRegistrationDTO User { get; set; }
    }

    public class CompanyRegistrationDTO
    {
        [Required]
        public string CorporateName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // TODO: validate this
        [Required]
        public string CpfCnpj { get; set; }
    }

    public class CompanyUserRegistrationDTO 
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
    }
}