using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using CaseTracker.Data.ParameterModels;
using CaseTracker.Data.ServiceResponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseTracker.Services.Interface
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
        Task<ServiceResponse<Paginate<Case>>> GetCaseAsync(GetCaseParameters getCaseParameters);
        ServiceResponse<bool> BulkDeleteCase(List<long> ids);
    }
}
