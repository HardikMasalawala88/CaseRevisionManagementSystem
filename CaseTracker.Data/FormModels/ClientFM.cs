using CaseTracker.Data.ContextModels;
using System;

namespace CaseTracker.Data.FormModels
{
    public class ClientFM : BaseEntityFM
    {
        public Guid Id { get; set; }
        public string? State { get; set; }
        public DateTime? DateOfBirth { get; set; } = DateTime.UtcNow;
        public string AadharNumber { get; set; }
        public string PanCardNumber { get; set; }
        public string VotingId { get; set; }
        //public string UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
