using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using CaseTracker.Data.ParameterModels;
using CaseTracker.Data.ServiceResponse;
using CaseTracker.Repository;
using CaseTracker.Repository.Interface;
using CaseTracker.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseTracker.Services
{
    public class CaseService : ICaseService
    {
        private readonly ICaseRepository _clientAndcaseRepository;
        private readonly ICaseDocumentRepository _caseDocumentRepository;
        private readonly ICaseRepository _caseRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationContext _context;

        public CaseService(ICaseRepository clientAndcaseRepository, ApplicationContext context, ICaseDocumentRepository caseDocumentRepository, ICaseRepository caseRepository, IClientRepository clientRepository, IUserRepository userRepository)
        {
            _clientAndcaseRepository = clientAndcaseRepository;
            _context = context;
            _caseDocumentRepository = caseDocumentRepository;
            _caseRepository = caseRepository;
            _clientRepository = clientRepository;
            _userRepository = userRepository;
        }

        public CaseFM CreateOrUpdateCase(CaseFM caseFM)
        {
            try
            {
                var caseDetail = _context.Cases.Where(x => x.Id == caseFM.Id).FirstOrDefault();
                if (caseDetail != null)
                {
                    caseDetail.ClientId = caseFM.ClientId;
                    caseDetail.LawyerId = caseFM.LawyerId;
                    caseDetail.HearingDate = caseFM.HearingDate;
                    caseDetail.CaseTitle = caseFM.CaseTitle;
                    caseDetail.CaseDetail = caseFM.CaseDetail;
                    caseDetail.CourtLocation = caseFM.CourtLocation;
                    caseDetail.CaseParentId = caseFM.CaseParentId.Value;
                    caseDetail.CaseNumber = caseFM.CaseNumber;
                    caseDetail.ModifiedDate = DateTime.UtcNow;
                    caseDetail.ModifiedBy = caseDetail.CreatedBy;

                    _clientAndcaseRepository.UpdateCase(caseDetail);
                }
                else
                {
                    Case caseInfo = new Case();
                    caseInfo.ClientId = caseFM.ClientId;
                    caseInfo.LawyerId = caseFM.LawyerId;
                    caseInfo.HearingDate = caseFM.HearingDate;
                    caseInfo.CaseTitle = caseFM.CaseTitle;
                    caseInfo.CaseDetail = caseFM.CaseDetail;
                    caseInfo.CourtLocation = caseFM.CourtLocation;
                    caseInfo.CaseNumber = caseFM.CaseNumber;
                    //caseInfo.CaseParentId = caseFM.CaseParentId.HasValue ? caseFM.CaseParentId.Value : 0;
                    caseInfo.CaseParentId = caseFM.CaseParentId.Value;
                    caseInfo.CreatedBy = caseFM.Lawyer.CreatedBy;

                    _clientAndcaseRepository.InsertCase(caseInfo);
                    caseFM.Id = caseInfo.Id;
                }
            }
            catch (Exception ex)
            { }

            return caseFM;
        }

        public async Task<ServiceResponse<Paginate<Case>>> GetCaseAsync(GetCaseParameters getCaseParameters)
        {
            ServiceResponse<Paginate<Case>> response = new ServiceResponse<Paginate<Case>>();

            try
            {
                Paginate<Case> result = new Paginate<Case>();
                var caseList = await _caseRepository.GetCaseAsync(getCaseParameters);
                caseList.Data.ForEach(x => x.Client = _clientRepository.GetClient(x.ClientId));
                caseList.Data.ForEach(x => x.Client.User = _userRepository.GetUser(x.Client.UserId));

                result.TotalCount = caseList.TotalCount;
                result.Data = caseList.Data;
                response.Result = result;
            }
            catch (Exception ex)
            {
                response.Success = false;
            }

            return response;
        }

        public CaseDocumentFM CreateOrUpdateCaseDocument(CaseDocumentFM caseDocument)
        {
            var caseDocDetail = _context.CaseDocuments.Include(x => x.Case).Where(x => x.Id == caseDocument.Id).FirstOrDefault();
            var caseDetail = _context.Cases.Where(x => x.Id == caseDocument.CaseId).FirstOrDefault();

            if (caseDocDetail != null)
            {
                caseDocDetail.FileName = caseDocument.FileName;
                caseDocDetail.Url = caseDocument.Url;
                caseDocDetail.CaseId = caseDocument.CaseId;
                caseDocDetail.ModifiedBy = caseDocDetail.CreatedBy;
                caseDocDetail.ModifiedDate = DateTime.UtcNow;

                _caseDocumentRepository.UpdateCaseDocument(caseDocDetail);

            }
            else
            {
                CaseDocument caseDocumentInfo = new CaseDocument();
                caseDocumentInfo.FileName = caseDocument.FileName;
                caseDocumentInfo.Url = caseDocument.Url;
                caseDocumentInfo.CaseId = caseDocument.CaseId;
                caseDocumentInfo.CreatedBy = caseDetail.CreatedBy;
                caseDocumentInfo.CreatedDate = DateTime.UtcNow;

                _caseDocumentRepository.InsertCaseDocument(caseDocumentInfo);
                caseDocument.Id = caseDocumentInfo.Id;
            }

            return caseDocument;
        }

        public List<CaseDocument> ListCaseDocDetail()
        {
            var caseDocInfo = _caseDocumentRepository.GetCasesDocuments().Where(x => x.IsDelete != true).ToList();
            return caseDocInfo;
        }

        public List<Case> ListCaseDetail()
        {
            var caseInfo = _context.Cases.Include(c => c.Client).ThenInclude(c => c.User).Where(c => !c.IsDelete).ToList();
            return caseInfo;
        }
        
        public List<Case> ListClientCases(long clientId)
        {
            var caseInfo = _context.Cases
                            .Where(c => !c.IsDelete && c.ClientId == clientId).ToList();

            return caseInfo;
        }

        public CaseFM GetCaseById(long caseId)
        {
            CaseFM caseFM = new();
            if (caseId > 0)
            {
                Case caseData = _clientAndcaseRepository.GetCase(caseId);

                caseFM.CaseTitle = caseData.CaseTitle;
                caseFM.ClientId = caseData.ClientId;
                caseFM.CaseDetail = caseData.CaseDetail;
                caseFM.CourtLocation = caseData.CourtLocation;
                caseFM.HearingDate = caseData.HearingDate;
                caseFM.CaseNumber = caseData.CaseNumber;
                caseFM.CaseParentId = caseData.CaseParentId;
                caseFM.LawyerId = caseData.LawyerId;
                caseFM.Id = caseData.Id;
            }

            return caseFM;
        }

        public bool RemoveCaseDetail(long caseId)
        {
            var caseData = _clientAndcaseRepository.GetCase(caseId);
            if (caseData != null)
            {
                _clientAndcaseRepository.DeleteCase(caseId);
                return true;
            }
            else
            {
                return false;
            }
        }

        public ServiceResponse<bool> BulkDeleteCase(List<long> ids)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            try
            {
                if (!ids.Any())
                {
                    response.Result = false;
                    response.Message = "Please provide case to delete.";
                    return response;
                }

                response.Result = _caseRepository.BulkDeleteCase(ids);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error while bulk deleting Case";
            }

            return response;
        }
    }
}
