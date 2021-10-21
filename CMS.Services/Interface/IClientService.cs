using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.Interface
{
    public interface IClientService
    {
        ClientFM CreateOrUpdateClient(ClientFM clientFM);
        IEnumerable<Client> ListClientData();
        Client GetClientData(long clientId);
        bool RemoveClient(long clientId);
    }
}
