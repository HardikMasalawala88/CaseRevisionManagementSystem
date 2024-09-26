using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using System.Collections.Generic;

namespace CMS.Services.Interface
{
    public interface ICaseService
    {
        CaseFM CreateOrUpdateCase(CaseFM caseFM);
        CaseDocumentFM CreateOrUpdateCaseDocument(CaseDocumentFM caseDocument);
        List<CaseDocument> ListCaseDocDetail();
        List<Case> ListCaseDetail();
        List<Case> ListClientCases(long clientId);
        bool RemoveCaseDetail(long caseId);
        CaseFM GetCaseById(long caseId);
    }
}
