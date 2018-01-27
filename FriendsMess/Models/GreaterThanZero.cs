using System.ComponentModel.DataAnnotations;

namespace FriendsMess.Models
{
    public class GreaterThanZero:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if((int)value<=0)
                return new ValidationResult("Expense should be greater than zero");
            return ValidationResult.Success;
        }
    }
}