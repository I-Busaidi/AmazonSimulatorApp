using System.ComponentModel.DataAnnotations;

namespace AmazonSimulatorApp.Data
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }

        // Navigation Properties
        public ICollection<Client> Clients { get; set; }
        public ICollection<Seller> Sellers { get; set; }
    }

    public class Client
    {
        [Key]
        public int CID { get; set; }
        public int UID { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int CompletedOrders { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<ProductReview> ProductReviews { get; set; }
    }

    public class Seller
    {
        [Key]
        public int SID { get; set; }
        public int UID { get; set; }
        public float Rating { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public ICollection<Product> Products { get; set; }
    }
    public class Product
    {
        [Key]
        public int PID { get; set; }
        public int SID { get; set; }
        public int CatID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        // Navigation Properties
        public Seller Seller { get; set; }
        public Category Category { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<ProductReview> ProductReviews { get; set; }
    }

    public class Category
    {
        [Key]
        public int CatID { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

        // Navigation Properties
        public ICollection<Product> Products { get; set; }
    }


    public class Order
    {
        [Key]
        public int OID { get; set; }
        public int CID { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation Properties
        public Client Client { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }

    public class OrderDetail
    {
        public int OID { get; set; }
        public int PID { get; set; }
        public int Quantity { get; set; }

        // Navigation Properties
        public Order Order { get; set; }
        public Product Product { get; set; }
    }

    public class ProductReview
    {
        [Key]
        public int RID { get; set; }
        public int PID { get; set; }
        public int CID { get; set; }
        public string Comment { get; set; }
        public float Rate { get; set; }

        // Navigation Properties
        public Product Product { get; set; }
        public Client Client { get; set; }
    }

}
