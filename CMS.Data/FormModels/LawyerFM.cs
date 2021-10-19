using CMS.Data.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.FormModels
{
    public class LawyerFM
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime? DateOfBirth { get; set; } = DateTime.UtcNow;
        public long AadharNumber { get; set; }
        public long PanCardNumber { get; set; }
        public long VotingId { get; set; }
        public long Lawyer_uniqueNumber { get; set; }
        public long SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
        public long CaseId { get; set; }
        public long AppointmentId { get; set; }
    }
}
