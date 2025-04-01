using CaseTracker.Data.ContextModels;
using System.Collections.Generic;

namespace CaseTracker.Repository.Interface
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
