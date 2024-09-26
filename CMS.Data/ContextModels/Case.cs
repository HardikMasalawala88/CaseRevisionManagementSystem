using System;

namespace CMS.Data.ContextModels
{
    public class Case : BaseEntity
    {
        public long ClientId { get; set; }
        public virtual Client Client { get; set; }
        public long LawyerId { get; set; }
        public virtual Lawyer Lawyer { get; set; }
        public string CaseTitle { get; set; } = string.Empty;
        public string CaseDetail { get; set; }
        public string CaseNumber { get; set; }
        public DateTime HearingDate { get; set; }
        public string CourtLocation { get; set; }
        public long CaseParentId { get; set; }
        
    }
}
