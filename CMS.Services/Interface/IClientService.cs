using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using System.Collections.Generic;

namespace CMS.Services.Interface
{
    public interface IClientService
    {
        ClientFM CreateClient(ClientFM clientFM);
        ClientFM UpdateClient(long clientFMId);
        IEnumerable<Client> ListClientData();
        Client GetClientData(long clientId);
        ClientFM GetClientById(long clientId);
        Client GetClientUsingEmail(string email);
        bool RemoveClient(long clientId);
        Client GetClientUsingUserId(long userId);
        List<Client> GetClientsUsingLawyerId(string userName);
    }
}
