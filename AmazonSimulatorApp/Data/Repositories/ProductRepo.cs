using System.Collections.Generic;
using System.Threading.Tasks;
using AmazonSimulatorApp;
using AmazonSimulatorApp.Data;
using Microsoft.EntityFrameworkCore;



public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products
            .Include(p => p.Seller)
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Seller)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.PID == id);
    }
    public async Task<ProductImages> CreateProductImagesAsync(ProductImages productImages)
    {
        _context.ProductsImages.Add(productImages);
        await _context.SaveChangesAsync();
        return productImages;
    }
    public async Task<ProductImages> UpdateProductImagesAsync(ProductImages productImages)
    {
        _context.ProductsImages.Add(productImages);
        await _context.SaveChangesAsync();
        return productImages;
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}