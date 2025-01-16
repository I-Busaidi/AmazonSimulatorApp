using AmazonSimulatorApp.Data;
using AmazonSimulatorApp.Data.DTOs;

namespace AmazonSimulatorApp.Services
{
    public interface ISellerService
    {
        IEnumerable<Seller> GetAllSeller();
        Seller GetSellerById(int sellerId);
        bool EmailExists(string email);
        SellerOutPutDTO GetSellerData(string? sellerName, int? sellerId);
        void AddSeller(SellerOutPutDTO input);
        void UpdateSeller(Seller seller);
        Seller GetSellerByName(string sellerName);

    }
}
