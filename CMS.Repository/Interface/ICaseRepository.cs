using CMS.Data.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository.Interface
{
    public interface ICaseRepository
    {
        IEnumerable<Case> GetCases();
        Case GetCase(long id);
        Case InsertCase(Case caseData);
        void UpdateCase(Case caseData);
        void DeleteCase(long id);
    }
}
