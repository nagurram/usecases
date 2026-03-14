using web.app.Models;
using web.app.Services;

namespace web.app.Tests;

public class ProductServiceTests
{
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _productService = new ProductService();
    }

    [Fact]
    public async Task GetAllProductsAsync_ReturnsActiveProducts()
    {
        // Act
        var products = await _productService.GetAllProductsAsync();

        // Assert
        Assert.NotNull(products);
        Assert.True(products.Count > 0);
        Assert.All(products, p => Assert.True(p.IsActive));
    }

    [Fact]
    public async Task GetProductByIdAsync_ReturnsProduct_WhenExists()
    {
        // Act
        var product = await _productService.GetProductByIdAsync(1);

        // Assert
        Assert.NotNull(product);
        Assert.Equal(1, product.Id);
        Assert.Equal("Laptop", product.Name);
    }

    [Fact]
    public async Task GetProductByIdAsync_ReturnsNull_WhenNotExists()
    {
        // Act
        var product = await _productService.GetProductByIdAsync(999);

        // Assert
        Assert.Null(product);
    }

    [Fact]
    public async Task CreateProductAsync_AddsNewProduct()
    {
        // Arrange
        var newProduct = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 99.99m,
            StockQuantity = 10,
            Category = "Test"
        };

        // Act
        var createdProduct = await _productService.CreateProductAsync(newProduct);

        // Assert
        Assert.NotNull(createdProduct);
        Assert.True(createdProduct.Id > 0);
        Assert.Equal("Test Product", createdProduct.Name);
        Assert.True(createdProduct.IsActive);
    }
}
