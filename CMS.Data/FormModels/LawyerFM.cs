using CMS.Data.ContextModels;
using System;

namespace CMS.Data.FormModels
{
    public class LawyerFM
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime? DateOfBirth { get; set; } 
        public string AadharNumber { get; set; }
        public string PanCardNumber { get; set; }
        public string VotingId { get; set; }
        public string Lawyer_uniqueNumber { get; set; }
        public Specialization Specialization { get; set; }
    }
}
