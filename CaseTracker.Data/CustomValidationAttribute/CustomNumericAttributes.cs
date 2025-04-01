using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CaseTracker.Data.CustomValidationAttribute
{
    public class CustomNumericAttributes : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Consider null as valid; modify if needed
            }

            string input = value.ToString();
            if (Regex.IsMatch(input, "^[0-9]+$"))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Only numbers (0-9) are allowed.");
        }
    }
}
