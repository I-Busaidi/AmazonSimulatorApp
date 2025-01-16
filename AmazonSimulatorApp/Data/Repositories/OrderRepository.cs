using Microsoft.EntityFrameworkCore;

namespace AmazonSimulatorApp.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.Include(o => o.Client).Include(o => o.OrderDetails);
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.Include(o => o.Client)
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OID == id);
        }

        public IEnumerable<Order> GetClientOrders(int clientId)
        {
            return _context.Orders.Include(o => o.Client)
                .Include(o => o.OrderDetails)
                .Where(o => o.CID == clientId);
        }

        public int AddOrder(Order order)
        {
            _context.Orders.Add(order);
            return order.OID;
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}
