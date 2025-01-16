
namespace AmazonSimulatorApp.Data.Repositories
{
    public interface IOrderDetailsRepository
    {
        void AddOrderDetails(List<OrderDetail> orderDetails);
        IEnumerable<OrderDetail> GetOrderProducts(int orderId);
    }
}