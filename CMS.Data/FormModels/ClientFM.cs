using CMS.Data.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.FormModels
{
    public class ClientFM
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string State { get; set; }
        public DateTime? DateOfBirth { get; set; } = DateTime.UtcNow;
        public long AadharNumber { get; set; }
        public long PanCardNumber { get; set; }
        public long VotingId { get; set; }
    }
}
