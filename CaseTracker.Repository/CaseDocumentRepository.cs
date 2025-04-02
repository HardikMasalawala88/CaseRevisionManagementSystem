using CaseTracker.Data.ContextModels;
using CaseTracker.Repository.Interface;
using CaseTracker.Repository.Repository;
using System;
using System.Collections.Generic;

namespace CaseTracker.Repository
{
    public class CaseDocumentRepository : ICaseDocumentRepository
    {
        private readonly IRepository<CaseDocument> _caseDocumentRepository;
        public CaseDocumentRepository(IRepository<CaseDocument> caseDocumentRepository)
        {
            _caseDocumentRepository = caseDocumentRepository;
        }

        public void DeleteCaseDocument(Guid id)
        {
            CaseDocument caseDocInfo = GetCaseDocument(id);
            caseDocInfo.IsDelete = true;
            caseDocInfo.ModifiedDate = DateTime.UtcNow;
            caseDocInfo.ModifiedBy = caseDocInfo.CreatedBy;

            _caseDocumentRepository.SaveChanges();
        }

        public CaseDocument GetCaseDocument(Guid id)
        {
            return _caseDocumentRepository.GetById(id);
        }

        public IEnumerable<CaseDocument> GetCasesDocuments()
        {
            return _caseDocumentRepository.GetAll();
        }

        public CaseDocument InsertCaseDocument(CaseDocument caseDocument)
        {
            var caseDocDetail = _caseDocumentRepository.Insert(caseDocument);
            return caseDocDetail;
        }

        public void UpdateCaseDocument(CaseDocument caseData)
        {
            _caseDocumentRepository.Update(caseData);
        }
    }
}
