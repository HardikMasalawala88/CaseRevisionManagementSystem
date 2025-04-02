using CaseTracker.Data.ContextModels;
using CaseTracker.Data.Enum;
using System;

namespace CaseTracker.Data.FormModels
{
    public class LawyerFM
    {
        public Guid Id { get; set; }
        public DateTime? DateOfBirth { get; set; } 
        public string AadharNumber { get; set; }
        public string PanCardNumber { get; set; }
        public string VotingId { get; set; }
        public string LawyerUniqueNumber { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public EnumLawyerSpecialization Specialization { get; set; }
    }
}
