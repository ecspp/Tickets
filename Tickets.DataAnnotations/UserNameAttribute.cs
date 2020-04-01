using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Tickets.DataAnnotations
{
    public class UserNameAttribute : ValidationAttribute
    {
        public UserNameAttribute()
        {
        }

        public string GetErrorMessage() => $"Username deve conter 3 numeros";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) 
        {
            Regex rgx = new Regex(@"^[0-9]{3}$");
            if (value is string && rgx.IsMatch(value as string)) {
                return ValidationResult.Success;
            }
            return new ValidationResult(GetErrorMessage());
        }
    }
}