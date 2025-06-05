using SimpleORM.Models;
using Xunit;

namespace SimpleORM.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Product_Constructor_InitializesDefaultValues()
        {
            // Arrange & Act
            var product = new Product();

            // Assert
            Assert.Equal(0, product.Id);
            Assert.Equal(string.Empty, product.Name);
            Assert.Equal(0m, product.Price);
            Assert.Equal(0, product.StockQuantity);
        }

        [Fact]
        public void Product_SetProperties_PropertiesAreSet()
        {
            // Arrange
            var product = new Product();
            
            // Act
            product.Id = 1;
            product.Name = "Test Product";
            product.Price = 9.99m;
            product.StockQuantity = 5;

            // Assert
            Assert.Equal(1, product.Id);
            Assert.Equal("Test Product", product.Name);
            Assert.Equal(9.99m, product.Price);
            Assert.Equal(5, product.StockQuantity);
        }

        [Fact]
        public void Product_ImplementsIEntity_Interface()
        {
            // Arrange & Act
            var product = new Product();
            
            // Assert
            Assert.IsAssignableFrom<IEntity>(product);
        }
    }
}
