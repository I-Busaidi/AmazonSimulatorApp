using AmazonSimulatorApp.Data;

public interface IProductRepository
{
    Task<Product> CreateProductAsync(Product product);
    Task<bool> DeleteProductAsync(int id);
    Task<List<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(int id);
    Task<Product> UpdateProductAsync(Product product);
    Task<ProductImages> CreateProductImagesAsync(ProductImages productImages);
    Task<ProductImages> UpdateProductImagesAsync(ProductImages productImages);
}