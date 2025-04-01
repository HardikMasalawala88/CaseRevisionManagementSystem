using CaseTracker.Data.CustomValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace CaseTracker.Data.ContextModels
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Email { get; set; }

        [Required]
        [CustomNumericAttributes]
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
