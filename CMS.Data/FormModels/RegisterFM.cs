using CMS.Data.CustomValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace CMS.Data.FormModels
{
    public class RegisterFM
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [PhoneNumber(ErrorMessage = "Please enter a valid phone number in the format 9999-999-999")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string Role { get; set; }

        public virtual LawyerFM LawyerFM { get; set; } = new();
    }
}
