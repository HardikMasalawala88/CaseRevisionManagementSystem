using CaseTracker.Data.ContextModels;
using System;

namespace CaseTracker.Data.FormModels
{
    public class CaseFM
    {
        public Guid Id { get; set; }
        public string CaseTitle { get; set; }
        public string CaseDetail { get; set; } = string.Empty;
        public DateTime HearingDate { get; set; } = DateTime.Now;
        public string CourtLocation { get; set; } = string.Empty;
        public Guid? CaseParentId { get; set; }
        public string CaseNumber { get; set; }
        public string ClientId { get; set; }
        public virtual Client Client { get; set; }
        public string LawyerId { get; set; }
        public virtual Lawyer Lawyer { get; set; }
    }
}
