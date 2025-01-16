using AmazonSimulatorApp.Data;

namespace AmazonSimulatorApp.Services
{
    public class CompoundService
    {

        public readonly IProductService _productService;
        public readonly ICategoryService _categoryService;

        public CompoundService(IProductService productService, ICategoryService categoryService )
        {
         
            _productService = productService;
            _categoryService = categoryService;

        }

        public int AddProduct( Product product, string ImgePath ) 
        {
            var productOut = _productService.CreateProductAsync(product);

            var productImg = new ProductImages
            {
                PID = product.PID,
                Product = product,
                ImgPath = ImgePath,
            };

            var productImgOut = _productService.CreateProductImagesAsync(productImg);

            var categoryCount = _categoryService.UpdateCategoryCount(1, product.CatID);

            return product.PID;
        }

        public async Task<bool> DeleteProduct(Product product) 
        {
            var Deleted = await _productService.DeleteProductAsync(product.PID);
            var categoryCount = _categoryService.UpdateCategoryCount(-1, product.CatID);
            return Deleted;
        }

    }
}
