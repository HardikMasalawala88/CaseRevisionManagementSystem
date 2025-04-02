using System;

namespace CaseTracker.Data.ContextModels
{
    public class CaseDocument : BaseEntityWithKey
    {
        public Guid CaseId { get; set; }
        public virtual Case Case { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
    }
}
