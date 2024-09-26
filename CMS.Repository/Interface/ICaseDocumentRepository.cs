using CMS.Data.ContextModels;
using System.Collections.Generic;

namespace CMS.Repository.Interface
{
    public interface ICaseDocumentRepository
    {
        IEnumerable<CaseDocument> GetCasesDocuments();
        CaseDocument GetCaseDocument(long id);
        CaseDocument InsertCaseDocument(CaseDocument caseData);
        void UpdateCaseDocument(CaseDocument caseData);
        void DeleteCaseDocument(long id);
    }
}
