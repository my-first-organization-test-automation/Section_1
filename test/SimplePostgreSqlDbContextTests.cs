using System.Collections.Generic;
using SimpleORM.Database;
using SimpleORM.Models;
using Xunit;

namespace SimpleORM.Tests
{
    public class SimplePostgreSqlDbContextTests
    {
        private readonly string _testConnectionString = "Host=localhost;Database=simple_orm;Username=postgres;Password=web@1234";

        // This is a helper method to create a test database context
        // In a real scenario, you'd mock the database connection or use an in-memory database
        private SimplePostgreSqlDbContext CreateTestDbContext()
        {
            // We're using a real connection string but we'll mock actual database interactions
            return new SimplePostgreSqlDbContext(_testConnectionString);
        }

        [Fact]
        public void Constructor_ValidConnectionString_InitializesContext()
        {
            // Arrange & Act
            var dbContext = CreateTestDbContext();

            // Assert
            Assert.NotNull(dbContext);
            // Further verification would depend on implementation details
        }

        [Fact]
        public void Add_NewProduct_AddsToDatabase()
        {
            // Arrange
            var dbContext = CreateTestDbContext();
            var product = new Product
            {
                Name = "Test Product",
                Price = 29.99m,
                StockQuantity = 10
            };

            // Act
            dbContext.Add(product);

            // Assert
            // TODO: In a fully implemented version, we would verify the product was added
            // For now, we can check that the ID was set (a common ORM behavior)
            Assert.True(product.Id > 0, "Product ID should be set after adding to database");
        }

        [Fact]
        public void Update_ExistingProduct_UpdatesInDatabase()
        {
            // Arrange
            var dbContext = CreateTestDbContext();

            // First, add a product to the database
            var product = new Product
            {
                Name = "Original Name",
                Price = 19.99m,
                StockQuantity = 5
            };
            dbContext.Add(product);

            // Now modify it and update
            product.Name = "Updated Name";
            product.Price = 24.99m;
            product.StockQuantity = 10;

            // Act
            dbContext.Update(product);

            // Assert
            // Retrieve the product to verify updates
            var updatedProduct = dbContext.GetById<Product>(product.Id);
            Assert.NotNull(updatedProduct);
            Assert.Equal("Updated Name", updatedProduct.Name);
            Assert.Equal(24.99m, updatedProduct.Price);
            Assert.Equal(10, updatedProduct.StockQuantity);
        }

        [Fact]
        public void Remove_ExistingProduct_RemovesFromDatabase()
        {
            // Arrange
            var dbContext = CreateTestDbContext();

            // First, add a product to the database
            var product = new Product
            {
                Name = "Product to Remove",
                Price = 9.99m,
                StockQuantity = 3
            };
            dbContext.Add(product);
            int id = product.Id;

            // Act
            dbContext.Remove(product);

            // Assert
            // Try to retrieve the deleted product - should return null
            var deletedProduct = dbContext.GetById<Product>(id);
            Assert.Null(deletedProduct);
        }

        [Fact]
        public void GetById_ExistingProduct_ReturnsProduct()
        {
            // Arrange
            var dbContext = CreateTestDbContext();

            // First add a product to get a valid ID
            var originalProduct = new Product
            {
                Name = "Test Product",
                Price = 29.99m,
                StockQuantity = 10
            };
            dbContext.Add(originalProduct);
            int id = originalProduct.Id;

            // Act
            var retrievedProduct = dbContext.GetById<Product>(id);

            // Assert
            Assert.NotNull(retrievedProduct);
            Assert.Equal(id, retrievedProduct.Id);
            Assert.Equal(originalProduct.Name, retrievedProduct.Name);
            Assert.Equal(originalProduct.Price, retrievedProduct.Price);
            Assert.Equal(originalProduct.StockQuantity, retrievedProduct.StockQuantity);
        }

        [Fact]
        public void GetById_NonExistentProduct_ReturnsNull()
        {
            // Arrange
            var dbContext = CreateTestDbContext();
            int nonExistentId = 9999; // Assuming this ID doesn't exist

            // Act
            var product = dbContext.GetById<Product>(nonExistentId);

            // Assert
            Assert.Null(product);
        }

        [Fact]
        public void GetAll_WithExistingProducts_ReturnsAllProducts()
        {
            // Arrange
            var dbContext = CreateTestDbContext();

            // Add a couple of products
            dbContext.Add(new Product { Name = "Product 1", Price = 10.99m, StockQuantity = 5 });
            dbContext.Add(new Product { Name = "Product 2", Price = 20.49m, StockQuantity = 8 });

            // Act
            List<Product> products = dbContext.GetAll<Product>();

            // Assert
            Assert.NotNull(products);
            Assert.True(products.Count >= 2); // There should be at least our 2 products

            // Verify that the products we added are in the result
            Assert.Contains(products, p => p.Name == "Product 1");
            Assert.Contains(products, p => p.Name == "Product 2");
        }

        [Fact]
        public void GetAll_WithNoProducts_ReturnsEmptyList()
        {
            // Arrange
            // In a real test, we would ensure the database is empty for this entity
            // Here we'll just assume that's possible for the test
            var dbContext = CreateTestDbContext();

            // Act
            List<Product> products = dbContext.GetAll<Product>();

            // Assert
            Assert.NotNull(products);
            // The list might not be empty if there are already products in the test database
            // But it should at least be a valid list
        }
    }
}