using CaseTracker.Data.ContextModels;
using System;

namespace CaseTracker.Data.FormModels
{
    public class CaseDocumentFM
    {
        public Guid Id { get; set; }
        public Guid CaseId { get; set; }
        public virtual Case? Case { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
    }
}
