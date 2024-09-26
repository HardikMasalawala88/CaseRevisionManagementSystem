using CMS.Data.ContextModels;
using System;

namespace CMS.Data.FormModels
{
    public class ClientFM
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public virtual User? User { get; set; }
        public string? State { get; set; }
        public DateTime? DateOfBirth { get; set; } = DateTime.UtcNow;
        public string AadharNumber { get; set; }
        public string PanCardNumber { get; set; }
        public string VotingId { get; set; }
    }
}
