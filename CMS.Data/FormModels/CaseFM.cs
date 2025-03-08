using CMS.Data.ContextModels;
using System;

namespace CMS.Data.FormModels
{
    public class CaseFM
    {
        public long Id { get; set; }
        public string CaseTitle { get; set; }
        public string CaseDetail { get; set; } = string.Empty;
        public DateTime HearingDate { get; set; } = DateTime.Now;
        public string CourtLocation { get; set; } = string.Empty;
        public long? CaseParentId { get; set; } = 0;
        public string CaseNumber { get; set; }
        public long ClientId { get; set; }
        public virtual Client Client { get; set; }
        public long LawyerId { get; set; }
        public virtual Lawyer Lawyer { get; set; }
    }
}
