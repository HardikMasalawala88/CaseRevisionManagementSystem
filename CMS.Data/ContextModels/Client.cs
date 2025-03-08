using System;
using System.Collections.Generic;

namespace CMS.Data.ContextModels
{
    public class Client : BaseEntity
    {
        public Client()
        {
            this.Cases = new HashSet<Case>();
        }

        public long UserId { get; set; }
        public virtual User User { get; set; }
        public string State { get; set; }
        public DateTime? DateOfBirth { get; set; } = DateTime.UtcNow;
        public string AadharNumber { get; set; }
        public string PanCardNumber { get; set; }
        public string VotingId { get; set; }
        public virtual ICollection<Case> Cases { get; set; }
    }
}
