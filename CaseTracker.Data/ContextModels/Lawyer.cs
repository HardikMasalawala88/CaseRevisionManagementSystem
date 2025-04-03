using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CaseTracker.Data.ContextModels
{
    public class Lawyer : BaseEntity
    {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; } // Using Identity's primary key

        public string LawyerUniqueNumber { get; set; }
        public string Specialization { get; set; }

        // Navigation Property
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Case> Cases { get; set; } = new List<Case>();
    }
}
