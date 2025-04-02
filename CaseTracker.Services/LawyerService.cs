using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using CaseTracker.Repository.Interface;
using CaseTracker.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CaseTracker.Services
{
    public class LawyerService : ILawyerService
    {
        private readonly ILawyerRepository _lawyerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationContext _context;
        private UserManager<ApplicationUser> _UserManager;

        public LawyerService(ILawyerRepository lawyerRepository, ApplicationContext context, IUserRepository userRepository,
            UserManager<ApplicationUser> UserManager)
        {
            _lawyerRepository = lawyerRepository;
            _userRepository = userRepository;
            _context = context;
            _UserManager = UserManager;
        }

        //public async Task<LawyerFM> CreateOrUpdateLawyer(LawyerFM lawyerFM)
        //{
        //    try
        //    {
        //        var lawyerDetail = _context.Lawyers.Include(x => x.User).Where(x => x.UserId == lawyerFM.UserId).FirstOrDefault();

        //        //var userInfo = _context.UserData.FirstOrDefault(x => x.Id == lawyerFM.UserId);
        //        var userInfo = await _UserManager.FindByIdAsync(lawyerFM.UserId);

        //        if (lawyerDetail != null && userInfo is not null)
        //        {
        //            User user = new User();
        //            user.Name = lawyerFM.User.Name;
        //            user.Email = lawyerFM.User.Email;
        //            user.MobileNo = lawyerFM.User.MobileNo;
        //            user.Address = lawyerFM.User.Address;
        //            user.City = lawyerFM.User.City;
        //            user.Gender = lawyerFM.User.Gender;
        //            user.Role = lawyerFM.User.Role;
        //            user.Username = lawyerFM.User.Username;
        //            user.ModifiedBy = lawyerFM.User.ModifiedBy;
        //            user.Password = lawyerFM.User.Password;
        //            user.ModifiedDate = DateTime.UtcNow;

        //            _userRepository.UpdateUser(user);

        //            Lawyer lawyer = new Lawyer();
        //            lawyer.Id = lawyerDetail.Id;
        //            lawyer.UserId = user.Id;
        //            lawyer.DateOfBirth = lawyerDetail.DateOfBirth;
        //            lawyer.AadharNumber = lawyerDetail.AadharNumber;
        //            lawyer.PanCardNumber = lawyerDetail.PanCardNumber;
        //            lawyer.Lawyer_uniqueNumber = lawyerDetail.Lawyer_uniqueNumber;
        //            lawyer.VotingId = lawyerDetail.VotingId;
        //            lawyer.Specialization = lawyerDetail.Specialization; 
        //            //lawyer.CaseId = lawyerDetail.CaseId;
        //            //lawyer.AppointmentId = lawyerDetail.AppointmentId;
        //            lawyer.ModifiedDate = DateTime.UtcNow;
        //            lawyer.ModifiedBy = user.ModifiedBy;

        //            _lawyerRepository.UpdateLawyer(lawyer);
        //        }
        //        else
        //        {
        //            if(userInfo is null)
        //            {
        //                User user = new User();
        //                user.Name = lawyerFM.User.Name;
        //                user.Email = lawyerFM.User.Email;
        //                user.MobileNo = lawyerFM.User.MobileNo;
        //                user.Address = lawyerFM.User.Address;
        //                user.City = lawyerFM.User.City;
        //                user.Gender = lawyerFM.User.Gender;
        //                user.Role = lawyerFM.User.Role;
        //                user.Username = lawyerFM.User.Username;
        //                user.Password = lawyerFM.User.Password;
        //                user.CreatedBy = lawyerFM.User.CreatedBy;
        //                _userRepository.InsertUser(user);
        //            }

        //            Lawyer lawyer = new Lawyer();
        //            lawyer.UserId = userInfo.Id;
        //            lawyer.DateOfBirth = lawyerFM.DateOfBirth;
        //            lawyer.AadharNumber = lawyerFM.AadharNumber;
        //            lawyer.PanCardNumber = lawyerFM.PanCardNumber;
        //            lawyer.Lawyer_uniqueNumber = lawyerFM.Lawyer_uniqueNumber;
        //            lawyer.VotingId = lawyerFM.VotingId;
        //            lawyer.Specialization = lawyerFM.Specialization.ToString(); 
        //            lawyer.CreatedBy = userInfo.CreatedBy;

        //            _lawyerRepository.InsertLawyer(lawyer);
        //            lawyerFM.Id = lawyer.Id;
        //            lawyerFM.UserId = lawyer.User.Id;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return lawyerFM;
        //}

        public Lawyer InsertLawyer(Lawyer lawyer)
        {
            var lawyerDetail = _lawyerRepository.InsertLawyer(lawyer);

            return lawyerDetail;
        }

        public IEnumerable<Lawyer> ListLawyerData()
        {
            var lawyerInfo = _lawyerRepository.GetLawyers().Where(x => x.IsDelete != true).ToList();
          
            return lawyerInfo;
        }

        public Lawyer GetLawyerData(string lawyerId)
        {
            var lawyerDetail = _lawyerRepository.GetLawyer(Guid.Parse(lawyerId));

            return lawyerDetail;
        }

        async Task<bool> ILawyerService.RemoveLawyerData(string lawyerId)
        {
            //var lawyerData = _lawyerRepository.GetLawyer(Guid.Parse(lawyerId));
            var isExist = await _lawyerRepository.IsExistAsync(Guid.Parse(lawyerId));
            //lawyerData.User = _userRepository.GetUser(lawyerData.UserId);

            if (isExist)
            {
                _lawyerRepository.DeleteLawyer(Guid.Parse(lawyerId));
                //_userRepository.DeleteUser(lawyerData.User.Id);
                return true;
            }

            return false;
        }

        public Lawyer GetLawyerDataByUserId(string userId)
        {
            Lawyer lawyerData = _context.Lawyers.FirstOrDefault(x => x.UserId == userId);

            return lawyerData;
        }

        public List<Lawyer> GetLawyersWithUserDetails()
        {
            return _context.Lawyers
                .Include(l => l.User)
                .Where(l => !l.IsDelete && l.User != null)
                .ToList();
        }
         
    }
}
