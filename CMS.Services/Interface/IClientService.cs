using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using CMS.Data.ParameterModels;
using CMS.Data.ServiceResponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Services.Interface
{
    public interface IClientService
    {
        ClientFM CreateClient(ClientFM clientFM);
        ClientFM UpdateClient(ClientFM clientFM);
        IEnumerable<Client> ListClientData();
        Client GetClientData(long clientId);
        ClientFM GetClientById(long clientId);
        Task<ServiceResponse<Paginate<Client>>> GetClientsAsync(GetClientsParameters getClientsParameters);
        Client GetClientUsingEmail(string email);
        bool RemoveClient(long clientId);
        Client GetClientUsingUserId(long userId);
        List<Client> GetClientsUsingLawyerId(string userName);
        ServiceResponse<bool> BulkDeleteClient(List<long> ids);
    }
}
