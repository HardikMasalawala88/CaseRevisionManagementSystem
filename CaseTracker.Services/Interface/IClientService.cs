using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using CaseTracker.Data.ParameterModels;
using CaseTracker.Data.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseTracker.Services.Interface
{
    public interface IClientService
    {
        //ClientFM CreateClient(ClientFM clientFM);
        //ClientFM UpdateClient(ClientFM clientFM);
        IEnumerable<Client> ListClientData();
        Client InsertClient(Client client);
        Client GetClientData(string clientId);
        ClientFM GetClientById(string clientId);
        Task<ServiceResponse<Paginate<Client>>> GetClientsAsync(GetClientsParameters getClientsParameters);
        Client GetClientUsingEmail(string email);
        //bool RemoveClient(string clientId);
        Client GetClientUsingUserId(string userId);
        List<Client> GetClientsUsingLawyerId(string userName);
        Task<bool> RemoveClientData(string clientId);
        ServiceResponse<bool> BulkDeleteClient(List<Guid> ids);
    }
}
