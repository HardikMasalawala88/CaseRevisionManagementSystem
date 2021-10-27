using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.ContextModels
{
    public class Lawyer : BaseEntity
    {
        public Lawyer()
        {
            this.Cases = new HashSet<Case>();
        }
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTime? DateOfBirth { get; set; } = DateTime.UtcNow;
        public long AadharNumber { get; set; }
        public long PanCardNumber { get; set; }
        public long VotingId { get; set; }
        public long Lawyer_uniqueNumber { get; set; }
        public string Specialization { get; set; }
        public long AppointmentId { get; set; }
        public long CaseId { get; set; }
        public virtual ICollection<Case> Cases { get; set; }
    }
}
