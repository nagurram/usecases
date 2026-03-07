using web.app.Models;

namespace web.app.Services
{
    public class ProductService : IProductService
    {
        // In-memory storage for demo purposes. Replace with database in production.
        private static List<Product> products = new()
        {
            new Product 
            { 
                Id = 1, 
                Name = "Laptop", 
                Description = "High-performance laptop",
                Price = 1299.99m,
                StockQuantity = 10,
                Category = "Electronics",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            },
            new Product 
            { 
                Id = 2, 
                Name = "Wireless Mouse", 
                Description = "Comfortable wireless mouse",
                Price = 29.99m,
                StockQuantity = 50,
                Category = "Electronics",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            },
            new Product 
            { 
                Id = 3, 
                Name = "USB-C Cable", 
                Description = "High-speed USB-C cable",
                Price = 14.99m,
                StockQuantity = 100,
                Category = "Accessories",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            }
        };

        public Task<List<Product>> GetAllProductsAsync()
        {
            return Task.FromResult(products.Where(p => p.IsActive).ToList());
        }

        public Task<Product?> GetProductByIdAsync(int id)
        {
            return Task.FromResult(products.FirstOrDefault(p => p.Id == id && p.IsActive));
        }

        public Task<List<Product>> GetProductsByCategoryAsync(string category)
        {
            return Task.FromResult(products.Where(p => p.Category == category && p.IsActive).ToList());
        }

        public Task<Product> CreateProductAsync(Product product)
        {
            product.Id = products.Max(p => p.Id) + 1;
            product.CreatedAt = DateTime.UtcNow;
            products.Add(product);
            return Task.FromResult(product);
        }

        public Task<Product> UpdateProductAsync(int id, Product product)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
                throw new KeyNotFoundException($"Product with ID {id} not found");

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.Category = product.Category;
            existingProduct.ImageUrl = product.ImageUrl;
            existingProduct.IsActive = product.IsActive;

            return Task.FromResult(existingProduct);
        }

        public Task<bool> DeleteProductAsync(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return Task.FromResult(false);

            product.IsActive = false;
            return Task.FromResult(true);
        }
    }
}
