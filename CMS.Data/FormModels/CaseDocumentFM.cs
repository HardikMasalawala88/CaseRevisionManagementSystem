using CMS.Data.ContextModels;

namespace CMS.Data.FormModels
{
    public class CaseDocumentFM
    {
        public long Id { get; set; }
        public long CaseId { get; set; }
        public virtual Case? Case { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
    }
}
