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
        //LawyerFM CreateOrUpdateLawyer(LawyerFM lawyer);
        //Task<LawyerFM> CreateOrUpdateLawyer(LawyerFM lawyerFM);
        IEnumerable<Lawyer> ListLawyerData();
        Lawyer InsertLawyer(Lawyer lawyer);
        Lawyer GetLawyerData(string lawyerId);
        Lawyer GetLawyerDataByUserId(string userId);
        Task<bool> RemoveLawyerData(string lawyerId);
        List<Lawyer> GetLawyersWithUserDetails();
    }
}
