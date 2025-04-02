using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CaseTracker.Data.ContextModels
{
    public class Case : BaseEntityWithKey
    {
        public string ClientId { get; set; }
        public virtual Client Client { get; set; }
        public string LawyerId { get; set; }
        public virtual Lawyer Lawyer { get; set; }
        public string CaseTitle { get; set; } = string.Empty;
        public string CaseDetail { get; set; }
        public string CaseNumber { get; set; }
        public DateTime HearingDate { get; set; }
        public string CourtLocation { get; set; }
        public Guid CaseParentId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime FilingDate { get; set; } = DateTime.UtcNow;
        public string CaseStatus { get; set; } = "Open";  // Open, Closed, Pending
        // Navigation Property
        public virtual ICollection<CaseDocument> CaseDocuments { get; set; } = new List<CaseDocument>();

    }
}
