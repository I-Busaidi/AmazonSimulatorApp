namespace AmazonSimulatorApp.Data.Repositories
{
    public interface ISellerRepo
    {
        IEnumerable<Seller> GetAllSellers();
        Seller GetSellerById(int id);
        Seller AddSeller(Seller seller);
        Seller UpdateSeller(Seller seller);
        void DeletSeller(Seller seller);
        bool IsValidRole(string roleName);
        bool EmailExists(string email);
        Seller GetSellerByName(string sellerName);


    }
}
