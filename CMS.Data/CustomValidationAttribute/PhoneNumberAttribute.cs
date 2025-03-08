using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CMS.Data.CustomValidationAttribute
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]

    sealed public class PhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string phoneNumber = value.ToString();
                string pattern = @"^\(\d{4}\) \d{3}-\d{3}$";

                if (!Regex.IsMatch(phoneNumber, pattern))
                {
                    return new ValidationResult("Phone number format is invalid.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
