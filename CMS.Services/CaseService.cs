using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using CMS.Repository.Interface;
using CMS.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Services
{
    public class CaseService : ICaseService
    {
        private readonly ICaseRepository _clientAndcaseRepository;
        private readonly ICaseDocumentRepository _caseDocumentRepository;
        private readonly ApplicationContext _context;

        public CaseService(ICaseRepository clientAndcaseRepository, ApplicationContext context, ICaseDocumentRepository caseDocumentRepository)
        {
            _clientAndcaseRepository = clientAndcaseRepository;
            _context = context;
            _caseDocumentRepository = caseDocumentRepository;
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
                    caseInfo.CaseParentId = caseFM.CaseParentId.HasValue ? caseFM.CaseParentId.Value : 0;
                    caseInfo.CreatedBy = caseFM.Lawyer.CreatedBy;

                    _clientAndcaseRepository.InsertCase(caseInfo);
                    caseFM.Id = caseInfo.Id;
                }
            }
            catch (Exception ex)
            { }

            return caseFM;
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
            var caseInfo = _context.Cases.AsNoTracking()
                            .Include(c => c.Client)
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
    }
}
