using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.ContextModels
{
    public class Client : BaseEntity
    {
        public Client()
        {
            this.Cases = new HashSet<Case>();
        }
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string State { get; set; }
        public DateTime? DateOfBirth { get; set; } = DateTime.UtcNow;
        public long AadharNumber { get; set; }
        public long PanCardNumber { get; set; }
        public long VotingId { get; set; }
        public virtual ICollection<Case> Cases { get; set; }
    }
}
