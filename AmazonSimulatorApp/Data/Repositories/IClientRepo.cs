namespace AmazonSimulatorApp.Data.Repositories
{
    public interface IClientRepo
    {
        IEnumerable<Client> GetAllClients();
        Client GetClientsById(int id);
        Client AddClient(Client client);
        Client UpdateClient(Client client);
        void DeletClient(Client client);
        bool IsValidRole(string roleName);
        bool EmailExists(string email);
        Client GetClientByName(string ClientName);

    }
}
