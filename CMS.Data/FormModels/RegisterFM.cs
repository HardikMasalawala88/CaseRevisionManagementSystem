using System.ComponentModel.DataAnnotations;

namespace CMS.Data.FormModels
{
    public class RegisterFM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string MobileNo { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }

        public virtual LawyerFM LawyerFM { get; set; }
    }
}
