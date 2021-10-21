using CMS.Data.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.FormModels
{
    public class CaseFM
    {
        public long Id { get; set; }
        public string CaseDetail { get; set; }
        public DateTime HearingDate { get; set; }
        public string CourtLocation { get; set; }
        public long CaseParentId { get; set; }
        public long ClientId { get; set; }
        public virtual Client Client { get; set; }
        public long LawyerId { get; set; }
        public virtual Lawyer Lawyer { get; set; }
    }
}
