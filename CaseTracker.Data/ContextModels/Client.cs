using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseTracker.Data.ContextModels
{
    public class Client : BaseEntity
    {
        public Client()
        {
            this.Cases = new HashSet<Case>();
        }

        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; } // Using Identity's primary key

        // Navigation Property
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Case> Cases { get; set; } = new List<Case>();
    }
}
