using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTracker.Data.ContextModels
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(100)]
        public string Firstname { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Lastname { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        [MaxLength(12)]
        public string AadharNumber { get; set; } = string.Empty;

        [MaxLength(10)]
        public string PAN { get; set; } = string.Empty;

        public string VotingId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; } = "INDIA";

        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public string Name => $"{Firstname} {Lastname}".Trim();

        // Navigation Property
        public virtual ICollection<Case> Cases { get; set; } = new List<Case>();
    }
}
