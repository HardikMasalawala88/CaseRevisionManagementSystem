using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using CaseTracker.Data.ParameterModels;
using CaseTracker.Data.ServiceResponse;
using System;
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
        List<Case> ListClientCases(string clientId);
        bool RemoveCaseDetail(Guid caseId);
        CaseFM GetCaseById(Guid caseId);
        Task<ServiceResponse<Paginate<Case>>> GetCaseAsync(GetCaseParameters getCaseParameters);
        ServiceResponse<bool> BulkDeleteCase(List<Guid> ids);
    }
}
