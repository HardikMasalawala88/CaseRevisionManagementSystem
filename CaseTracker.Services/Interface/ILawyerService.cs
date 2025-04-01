using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTracker.Services.Interface
{
    public interface ILawyerService
    {
        LawyerFM CreateOrUpdateLawyer(LawyerFM lawyer);
        IEnumerable<Lawyer> ListLawyerData();
        Lawyer GetLawyerData(long lawyerId);
        Lawyer GetLawyerDataByUserId(long userId);
        bool RemoveLawyerData(long lawyerId);
        List<Lawyer> GetLawyersWithUserDetails();
    }
}
