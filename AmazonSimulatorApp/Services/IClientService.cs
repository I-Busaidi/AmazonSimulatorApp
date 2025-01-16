using AmazonSimulatorApp.Data;
using AmazonSimulatorApp.Data.DTOs;

namespace AmazonSimulatorApp.Services
{
    public interface IClientService
    {
        Client GetClientById(int clientId);
        IEnumerable<Client> GetAllClients();
        bool EmailExists(string email);
        ClientOutPutDTO GetClientData(string? clientName, int? clientId);
        void AddClient(ClientOutPutDTO input);
        void UpdateClientDetails(updateClientDTO input);
        void UpdateClient(Client client);
        Client GetClientByName(string clientName);
    }
}
