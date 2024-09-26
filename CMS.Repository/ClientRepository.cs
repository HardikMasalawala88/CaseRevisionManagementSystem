using CMS.Data.ContextModels;
using CMS.Repository.Interface;
using CMS.Repository.Repository;
using System;
using System.Collections.Generic;

namespace CMS.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IRepository<Client> _clientRepository;
        public ClientRepository(IRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public void DeleteClient(long id)
        {
            Client client = GetClient(id);
            client.IsDelete = true;
            client.ModifiedDate = DateTime.UtcNow;
            client.ModifiedBy = client.CreatedBy;

            _clientRepository.SaveChanges();
        }

        public Client GetClient(long id)
        {
            return _clientRepository.GetById(id);
        }

        public IEnumerable<Client> GetClients()
        {
            return _clientRepository.GetAll();
        }

        public Client InsertClient(Client client)
        {
            Client clientData = _clientRepository.Insert(client);
            return clientData;
        }

        public void UpdateClient(Client client)
        {
            _clientRepository.Update(client);
        }
    }
}