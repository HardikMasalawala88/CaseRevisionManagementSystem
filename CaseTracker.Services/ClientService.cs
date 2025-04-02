using AutoMapper;
using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using CaseTracker.Data.ParameterModels;
using CaseTracker.Data.ServiceResponse;
using CaseTracker.Repository;
using CaseTracker.Repository.Interface;
using CaseTracker.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseTracker.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, ApplicationContext context, IUserRepository userRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _context = context;
            _mapper = mapper;
        }

        //public ClientFM CreateClient(ClientFM clientFM)
        //{
        //    try
        //    {
        //        User userData = new User();
        //        userData.Name = clientFM.User.Name;
        //        userData.Email = clientFM.User.Email;
        //        userData.MobileNo = clientFM.User.MobileNo;
        //        userData.Address = clientFM.User.Address;
        //        userData.City = clientFM.User.City;
        //        userData.Gender = clientFM.User.Gender;
        //        userData.Role = clientFM.User.Role;
        //        userData.Username = clientFM.User.Username;
        //        userData.Password = clientFM.User.Password;
        //        userData.CreatedBy = clientFM.User.CreatedBy;
        //        _userRepository.InsertUser(userData);

        //        Client client = new Client();
        //        client.UserId = userData.Id;
        //        client.DateOfBirth = clientFM.DateOfBirth;
        //        client.AadharNumber = clientFM.AadharNumber;
        //        client.State = clientFM.State;
        //        client.PanCardNumber = clientFM.PanCardNumber;
        //        client.VotingId = clientFM.VotingId;
        //        client.User = userData;
        //        client.CreatedBy = userData.CreatedBy;

        //        _clientRepository.InsertClient(client);
        //        clientFM.Id = client.Id;
        //        clientFM.UserId = client.User.Id;

        //        return clientFM;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //public ClientFM UpdateClient(ClientFM clientFM)
        //{
        //    try
        //    {
        //        //ClientFM clientFM = new ClientFM();
        //        var clientDetail = _context.Clients.FirstOrDefault(x => x.Id == clientFM.Id);
        //        //var user = _context.UserData.FirstOrDefault(x => x.Id == clientDetail.UserId);
        //        var userInfo = await _UserManager.FirstOrDefault(clientFM.User);

        //        if (clientDetail is not null && user is not null)
        //        {
        //            _context.Entry(clientDetail).State = EntityState.Detached;

        //            user.Name = clientFM.User.Name;
        //            user.Email = clientFM.User.Email;
        //            user.MobileNo = clientFM.User.MobileNo;
        //            user.Address = clientFM.User.Address;
        //            user.City = clientFM.User.City;
        //            user.Gender = clientFM.User.Gender;
        //            user.Role = clientFM.User.Role;
        //            user.Username = clientFM.User.Username;
        //            user.ModifiedBy = clientFM.User.ModifiedBy;
        //            user.Password = clientFM.User.Password;
        //            user.ModifiedDate = DateTime.UtcNow;

        //            _userRepository.UpdateUser(user);

        //            //Client client = clientDetail;
        //            clientDetail.Id = clientFM.Id;
        //            clientDetail.UserId = user.Id;
        //            clientDetail.State = clientFM.State;
        //            clientDetail.DateOfBirth = clientFM.DateOfBirth;
        //            clientDetail.AadharNumber = clientDetail.AadharNumber;
        //            clientDetail.PanCardNumber = clientFM.PanCardNumber;
        //            clientDetail.VotingId = clientFM.VotingId;
        //            clientDetail.ModifiedDate = DateTime.UtcNow;
        //            clientDetail.ModifiedBy = user.ModifiedBy;

        //            _clientRepository.UpdateClient(clientDetail);

        //            clientFM = _mapper.Map<ClientFM>(clientDetail);
        //        }

        //        return clientFM;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public Client InsertClient(Client client)
        {
            var clientDetail = _clientRepository.InsertClient(client);

            return clientDetail;
        }

        public IEnumerable<Client> ListClientData()
        {
            var clientInfo = _clientRepository.GetClients().Where(x => x.IsDelete).ToList();

            return clientInfo;
        }

        public async Task<ServiceResponse<Paginate<Client>>> GetClientsAsync(GetClientsParameters getClientsParameters)
        {
            ServiceResponse<Paginate<Client>> response = new ServiceResponse<Paginate<Client>>();

            try
            {
                Paginate<Client> result = new Paginate<Client>();
                var clients = await _clientRepository.GetClientsAsync(getClientsParameters);
                foreach (var client in clients.Data)
                {
                    client.User = await _userRepository.GetUserByIdAsync(client.UserId);
                }

                result.TotalCount = clients.TotalCount;
                result.Data = clients.Data;
                response.Result = result;
            }
            catch (Exception ex)
            {
                response.Success = false;
            }

            return response;
        }

        public Client GetClientData(string clientId)
        {
            var clientDetail = _clientRepository.GetClient(Guid.Parse(clientId));
            return clientDetail;
        }

        public ClientFM GetClientById(string clientId)
        {
            ClientFM clientFM = new();
            Client clientData = _context.Clients.Include(x => x.User).FirstOrDefault(x => x.UserId == clientId);

            clientFM.Id = Guid.Parse(clientData.UserId);
            clientFM.AadharNumber = clientData.User.AadharNumber;
            clientFM.PanCardNumber = clientData.User.PAN;
            clientFM.VotingId = clientData.User.VotingId;
            clientFM.User = clientData.User;
            clientFM.State = clientData.User.State;
            clientFM.DateOfBirth = clientData.User.DateOfBirth;

            return clientFM;
        }

        public Client GetClientUsingUserId(string userId)
        {
            var clientDetail = _context.Clients.FirstOrDefault(x => x.UserId == userId);
            return clientDetail;
        }
        
        public List<Client> GetClientsUsingLawyerId(string userName)
        {
            var clientDetail = _context.Clients.Include(x => x.User)
                                               .Where(x => x.CreatedBy.Equals(userName) && !x.IsDelete).ToList();
            return clientDetail;
        }
        
        public Client GetClientUsingEmail(string email)
        {
            var clientDetail = _context.Clients.Include(x => x.User)
                                               .FirstOrDefault(x => x.User.Email.Equals(email) && !x.IsDelete);
            return clientDetail;
        }

        async Task<bool> IClientService.RemoveClientData(string clientId)
        {
            //var clientData = _clientRepository.GetClient(Guid.Parse(clientId));
            var isExist = await _clientRepository.IsExistAsync(Guid.Parse(clientId));
            //clientData.User = await _userRepository.GetUserByIdAsync(clientData.UserId);
            
            if (isExist)
            {
                _clientRepository.DeleteClient(Guid.Parse(clientId));
                //await _userRepository.DeleteUserAsync(clientData.User.Id);

                return true;
            }
            else
            {
                return false;
            }
        }

        public ServiceResponse<bool> BulkDeleteClient(List<Guid> ids)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            try
            {
                if (!ids.Any())
                {
                    response.Result = false;
                    response.Message = "Please provide clients to delete.";
                    return response;
                }

                response.Result = _clientRepository.BulkDeleteClient(ids);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error while bulk deleting clients";
            }

            return response;
        }
    }
}
