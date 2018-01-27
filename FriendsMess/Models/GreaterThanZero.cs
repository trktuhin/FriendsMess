using System;
using System.ComponentModel.DataAnnotations;

namespace FriendsMess.Models
{
    public class GreaterThanZero:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int numValue=0;
            try
            {
                numValue = (int) value;
            }
            catch (Exception e)
            {
                return new ValidationResult(value+" is not valid for Expense");
            }

            if(numValue<=0)
                return new ValidationResult("Expense should be greater than zero");
            return ValidationResult.Success;
        }
    }
}