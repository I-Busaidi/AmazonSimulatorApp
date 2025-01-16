using AmazonSimulatorApp.Data.DTOs;
using AmazonSimulatorApp.Data.Repositories;
using AmazonSimulatorApp.Data;
using AmazonSimulatorApp.Repositories;

namespace AmazonSimulatorApp.Services
{
    public class SellerService : ISellerService
    {
        private readonly ISellerRepo _sellerRepo;
        private readonly IUserService _userService;

        public SellerService(ISellerRepo sellerRepo, IUserService userService)
        {
            _sellerRepo = sellerRepo;
            _userService = userService;
        }

        public IEnumerable<Seller> GetAllSeller()
        {
            return _sellerRepo.GetAllSellers();
        }

        public Seller GetSellerById(int sellerId)
        {
            var seller = _sellerRepo.GetSellerById(sellerId);
            if (seller == null)
                throw new KeyNotFoundException($"seller with ID {sellerId} not found.");
            return seller;
        }

        public bool EmailExists(string email)
        {
            return _sellerRepo.EmailExists(email);
        }

        public SellerOutPutDTO GetSellerData(string? sellerName, int? sellerId)
        {
            if (string.IsNullOrWhiteSpace(sellerName) && !sellerId.HasValue)
                throw new ArgumentException("Either client name or client ID must be provided.");

            Seller seller = null;

            if (!string.IsNullOrEmpty(sellerName))
                seller = GetSellerByName(sellerName);

            if (sellerId.HasValue)
                seller = GetSellerById(sellerId.Value);

            if (seller == null)
                throw new KeyNotFoundException("seller not found.");

            return new SellerOutPutDTO
            {
                SID = seller.SID,
                Rating = seller.Rating,
                
            };
        }

        public void AddSeller(SellerOutPutDTO input)
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

            var seller = new Seller
            {
                SID = user.ID,
                Rating = input.Rating
              
            };

            _sellerRepo.AddSeller(seller);
        }

 

        public void UpdateSeller(Seller seller)
        {
            if (seller == null)
                throw new ArgumentNullException(nameof(seller), "Client information is required.");

            var existingSeller = _sellerRepo.GetSellerById(seller.SID);
            if (existingSeller == null)
                throw new KeyNotFoundException($"Client with ID {seller.SID} not found.");

            _sellerRepo.UpdateSeller(seller);
        }

        public Seller GetSellerByName(string sellerName)
        {
            if (string.IsNullOrEmpty(sellerName))
                throw new ArgumentException("Client name cannot be null or empty.");

            var seller = _sellerRepo.GetSellerByName(sellerName);
            if (seller == null)
                throw new KeyNotFoundException($"seller with name '{sellerName}' not found.");

            return seller;
        }
    }
}