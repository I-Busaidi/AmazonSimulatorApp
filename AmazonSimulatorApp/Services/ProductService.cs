﻿using AmazonSimulatorApp.Data;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProductsAsync();
        if (products == null || !products.Any())
        {
            throw new InvalidOperationException("No products found.");
        }

        return products;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Product ID must be greater than zero.");
        }

        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with ID {id} not found.");
        }

        return product;
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        ValidateProduct(product);

        // Check if a product with the same name already exists 
        var existingProducts = await _productRepository.GetAllProductsAsync();
        if (existingProducts.Any(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException($"A product with the name '{product.Name}' already exists.");
        }

        return await _productRepository.CreateProductAsync(product);
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        if (product.PID <= 0)
        {
            throw new ArgumentException("Product ID must be greater than zero.");
        }

        var existingProduct = await _productRepository.GetProductByIdAsync(product.PID);
        if (existingProduct == null)
        {
            throw new KeyNotFoundException($"Product with ID {product.PID} not found.");
        }

        ValidateProduct(product);

        // Perform additional business logic validation
        if (product.Price <= 0)
        {
            throw new ArgumentException("Product price must be greater than zero.");
        }

        return await _productRepository.UpdateProductAsync(product);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Product ID must be greater than zero.");
        }

        var success = await _productRepository.DeleteProductAsync(id);
        if (!success)
        {
            throw new KeyNotFoundException($"Product with ID {id} not found.");
        }

        return success;
    }

    public async Task<ProductImages> CreateProductImagesAsync(ProductImages productImages)
    {
        if (productImages == null)
        {
            throw new ArgumentNullException(nameof(productImages), "Product image cannot be null.");
        }

        if (productImages.PID <= 0)
        {
            throw new ArgumentException("Invalid product ID for the image.");
        }

        if (string.IsNullOrWhiteSpace(productImages.ImgPath))
        {
            throw new ArgumentException("Image path cannot be empty or null.");
        }

        // Perform additional validation or checks if necessary
        return await _productRepository.CreateProductImagesAsync(productImages);
    }

    public async Task<ProductImages> UpdateProductImagesAsync(ProductImages productImages)
    {
        if (productImages == null)
        {
            throw new ArgumentNullException(nameof(productImages), "Product image cannot be null.");
        }

        if (productImages.PID <= 0)
        {
            throw new ArgumentException("Invalid product ID for the image.");
        }

        if (string.IsNullOrWhiteSpace(productImages.ImgPath))
        {
            throw new ArgumentException("Image path cannot be empty or null.");
        }

        // Perform any additional validation if required
        return await _productRepository.UpdateProductImagesAsync(productImages);
    }

    /// <summary>
    /// Validates the product data for common scenarios.
    /// </summary>
    /// <param name="product">The product to validate.</param>
    private void ValidateProduct(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(product.Name))
        {
            throw new ArgumentException("Product name cannot be empty.");
        }

        if (product.Price <= 0)
        {
            throw new ArgumentException("Product price must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(product.Description))
        {
            throw new ArgumentException("Product description cannot be empty.");
        }

        if (product.SID <= 0)
        {
            throw new ArgumentException("Seller ID must be greater than zero.");
        }

        if (product.CatID <= 0)
        {
            throw new ArgumentException("Category ID must be greater than zero.");
        }
    }
}
