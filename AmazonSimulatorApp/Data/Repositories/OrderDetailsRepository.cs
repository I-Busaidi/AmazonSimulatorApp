using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AmazonSimulatorApp.Data.Repositories
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderDetail> GetOrderProducts(int orderId)
        {
            return _context.OrderDetails.Include(o => o.Order)
                .Include(o => o.Product)
                .Where(o => o.OID == orderId);
        }

        public void AddOrderDetails(List<OrderDetail> orderDetails)
        {
            _context.OrderDetails.AddRange(orderDetails);
        }
    }
}
