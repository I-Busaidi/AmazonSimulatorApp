using AmazonSimulatorApp.Data;
using AmazonSimulatorApp.Data.DTOs;
using AmazonSimulatorApp.Data.Repositories;

namespace AmazonSimulatorApp.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepo _clientRepo;
        private readonly IUserService _userService;

        public ClientService(IClientRepo clientRepo, IUserService userService)
        {
            _clientRepo = clientRepo;
            _userService = userService;
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _clientRepo.GetAllClients();
        }

        public Client GetClientById(int clientId)
        {
            var client = _clientRepo.GetClientsById(clientId);
            if (client == null)
                throw new KeyNotFoundException($"Client with ID {clientId} not found.");
            return client;
        }

        public bool EmailExists(string email)
        {
            return _clientRepo.EmailExists(email);
        }

        public ClientOutPutDTO GetClientData(string? clientName, int? clientId)
        {
            if (string.IsNullOrWhiteSpace(clientName) && !clientId.HasValue)
                throw new ArgumentException("Either client name or client ID must be provided.");

            Client client = null;

            if (!string.IsNullOrEmpty(clientName))
                client = GetClientByName(clientName);

            if (clientId.HasValue)
                client = GetClientById(clientId.Value);

            if (client == null)
                throw new KeyNotFoundException("Client not found.");

            return new ClientOutPutDTO
            {
                CID = client.CID,
                Phone = client.Phone,
                Address = client.Address,
                CompletedOrders = client.CompletedOrders
            };
        }

        public void AddClient(ClientOutPutDTO input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Client information is missing.");

            if (input.ID <= 0)
                throw new ArgumentException("Invalid client ID.");

            var user = _userService.GetUserById(input.ID);
            if (user == null)
                throw new KeyNotFoundException($"No user found with ID {input.ID}.");

            if (!user.Role.Equals("client", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("The provided user ID does not belong to a client.");

            var client = new Client
            {
                CID = user.ID,
                Phone = input.Phone,
                Address = input.Address,
                CompletedOrders = input.CompletedOrders
            };

            _clientRepo.AddClient(client);
        }

        public void UpdateClientDetails(updateClientDTO input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Client update details are required.");

            if (!input.CID.HasValue)
                throw new ArgumentException("Client ID is required.");

            var existingClient = _clientRepo.GetClientsById(input.CID.Value);
            var existingUser = _userService.GetUserById(input.CID.Value);

            if (existingClient == null || existingUser == null)
                throw new KeyNotFoundException("Client or associated user not found.");

            if (!existingUser.IsActive)
                throw new InvalidOperationException("This client is no longer active in the system.");

            existingClient.Phone = input.Phone;
            existingClient.Address = input.Address;

            _clientRepo.UpdateClient(existingClient);
        }

        public void UpdateClient(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client), "Client information is required.");

            var existingClient = _clientRepo.GetClientsById(client.CID);
            if (existingClient == null)
                throw new KeyNotFoundException($"Client with ID {client.CID} not found.");

            _clientRepo.UpdateClient(client);
        }

        public Client GetClientByName(string clientName)
        {
            if (string.IsNullOrEmpty(clientName))
                throw new ArgumentException("Client name cannot be null or empty.");

            var client = _clientRepo.GetClientByName(clientName);
            if (client == null)
                throw new KeyNotFoundException($"Client with name '{clientName}' not found.");

            return client;
        }
    }
}
