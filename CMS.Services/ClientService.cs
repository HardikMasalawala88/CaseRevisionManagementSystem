using AutoMapper;
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

        public ClientFM CreateClient(ClientFM clientFM)
        {
            try
            {
                User userData = new User();
                userData.Name = clientFM.User.Name;
                userData.Email = clientFM.User.Email;
                userData.MobileNo = clientFM.User.MobileNo;
                userData.Address = clientFM.User.Address;
                userData.City = clientFM.User.City;
                userData.Gender = clientFM.User.Gender;
                userData.Role = clientFM.User.Role;
                userData.Username = clientFM.User.Username;
                userData.Password = clientFM.User.Password;
                userData.CreatedBy = clientFM.User.CreatedBy;
                _userRepository.InsertUser(userData);

                Client client = new Client();
                client.UserId = userData.Id;
                client.DateOfBirth = clientFM.DateOfBirth;
                client.AadharNumber = clientFM.AadharNumber;
                client.State = clientFM.State;
                client.PanCardNumber = clientFM.PanCardNumber;
                client.VotingId = clientFM.VotingId;
                client.User = userData;
                client.CreatedBy = userData.CreatedBy;

                _clientRepository.InsertClient(client);
                clientFM.Id = client.Id;
                clientFM.UserId = client.User.Id;

                return clientFM;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public ClientFM UpdateClient(long clientFMId)
        {
            try
            {
                ClientFM clientFM = new ClientFM();
                var clientDetail = _context.Clients.FirstOrDefault(x => x.Id == clientFMId);
                var user = _context.UserData.FirstOrDefault(x => x.Id == clientDetail.UserId);
                
                if (clientDetail is not null && user is not null)
                {
                    _context.Entry(clientDetail).State = EntityState.Detached;

                    user.Name = clientDetail.User.Name;
                    user.Email = clientDetail.User.Email;
                    user.MobileNo = clientDetail.User.MobileNo;
                    user.Address = clientDetail.User.Address;
                    user.City = clientDetail.User.City;
                    user.Gender = clientDetail.User.Gender;
                    user.Role = clientDetail.User.Role;
                    user.Username = clientDetail.User.Username;
                    user.ModifiedBy = clientDetail.User.ModifiedBy;
                    user.Password = clientDetail.User.Password;
                    user.ModifiedDate = DateTime.UtcNow;

                    _userRepository.UpdateUser(user);

                    //Client client = clientDetail;
                    //client.Id = clientDetail.Id;
                    //client.UserId = user.Id;
                    //client.State = clientDetail.State;
                    //client.DateOfBirth = clientDetail.DateOfBirth;
                    //client.AadharNumber = clientDetail.AadharNumber;
                    //client.PanCardNumber = clientDetail.PanCardNumber;
                    //client.VotingId = clientDetail.VotingId;
                    clientDetail.ModifiedDate = DateTime.UtcNow;
                    clientDetail.ModifiedBy = user.ModifiedBy;

                    _clientRepository.UpdateClient(clientDetail);

                    clientFM = _mapper.Map<ClientFM>(clientDetail);
                }

                return clientFM;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<Client> ListClientData()
        {
            var clientInfo = _clientRepository.GetClients().Where(x => !x.IsDelete).ToList();
            clientInfo.ForEach(x => x.User = _userRepository.GetUser(x.UserId));
            return clientInfo;
        }

        public Client GetClientData(long clientId)
        {
            var clientDetail = _clientRepository.GetClient(clientId);
            return clientDetail;
        }

        public ClientFM GetClientById(long clientId)
        {
            ClientFM clientFM = new();
            Client clientData = _context.Clients.Include(x => x.User).FirstOrDefault(x => x.Id == clientId);

            clientFM.UserId = clientData.UserId;
            clientFM.AadharNumber = clientData.AadharNumber;
            clientFM.PanCardNumber = clientData.PanCardNumber;
            clientFM.VotingId = clientData.VotingId;
            clientFM.User = clientData.User;
            clientFM.State = clientData.State;
            clientFM.DateOfBirth = clientData.DateOfBirth;
            clientFM.Id = clientData.Id;

            return clientFM;
        }

        public Client GetClientUsingUserId(long userId)
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

        public bool RemoveClient(long clientId)
        {
            var clientData = _clientRepository.GetClient(clientId);
            clientData.User = _userRepository.GetUser(clientData.UserId);
            
            if (clientData != null)
            {
                _clientRepository.DeleteClient(clientId);
                _userRepository.DeleteUser(clientData.User.Id);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
