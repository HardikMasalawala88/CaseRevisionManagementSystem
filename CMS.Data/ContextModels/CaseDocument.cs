namespace CMS.Data.ContextModels
{
    public class CaseDocument : BaseEntity
    {
        public long CaseId { get; set; }
        public virtual Case Case { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
    }
}
