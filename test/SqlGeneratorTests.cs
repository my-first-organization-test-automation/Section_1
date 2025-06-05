using System;
using SimpleORM.Database;
using SimpleORM.Models;
using Xunit;

namespace SimpleORM.Tests
{
    public class SqlGeneratorTests
    {
        [Fact]
        public void GetTableName_ForProductEntity_ReturnsProductsTableName()
        {
            // Arrange & Act
            string tableName = SqlGenerator.GetTableName<Product>();

            // Assert
            Assert.Equal("products", tableName.ToLower());
        }

        [Fact]
        public void GenerateCreateTableSql_ForProductEntity_ReturnsValidSql()
        {
            // Arrange & Act
            string createTableSql = SqlGenerator.GenerateCreateTableSql<Product>();

            // Assert
            Assert.Contains("CREATE TABLE", createTableSql, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("id", createTableSql, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("name", createTableSql, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("price", createTableSql, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("stockquantity", createTableSql, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void GenerateInsertSql_ForProductEntity_ReturnsValidSql()
        {
            // Arrange
            var product = new Product
            {
                Name = "Test Product",
                Price = 10.99m,
                StockQuantity = 5
            };

            // Act
            string insertSql = SqlGenerator.GenerateInsertSql(product);

            // Assert
            Assert.Contains("INSERT INTO", insertSql, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("VALUES", insertSql, StringComparison.OrdinalIgnoreCase);
            // Should have parameter placeholders
            Assert.Contains("@", insertSql);
        }

        [Fact]
        public void GenerateUpdateSql_ForProductEntity_ReturnsValidSql()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Updated Product",
                Price = 19.99m,
                StockQuantity = 25
            };

            // Act
            string updateSql = SqlGenerator.GenerateUpdateSql(product);

            // Assert
            Assert.Contains("UPDATE", updateSql, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("SET", updateSql, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("WHERE", updateSql, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("id", updateSql, StringComparison.OrdinalIgnoreCase);
            // Check for parameter placeholders
            Assert.Contains("@", updateSql);
            // Entity properties should be in the SQL
            Assert.Contains("name", updateSql, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("price", updateSql, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("stockquantity", updateSql, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void GenerateDeleteSql_ForProductEntity_ReturnsValidSql()
        {
            // Arrange & Act
            string deleteSql = SqlGenerator.GenerateDeleteSql<Product>();

            // Assert
            Assert.Contains("DELETE FROM", deleteSql, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("WHERE", deleteSql, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("id", deleteSql, StringComparison.OrdinalIgnoreCase);
            // Check for parameter placeholder
            Assert.Contains("@", deleteSql);
        }

        [Fact]
        public void GenerateSelectByIdSql_ForProductEntity_ReturnsValidSql()
        {
            // Arrange & Act
            string selectByIdSql = SqlGenerator.GenerateSelectByIdSql<Product>();

            // Assert
            Assert.Contains("SELECT", selectByIdSql, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("FROM", selectByIdSql, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("WHERE", selectByIdSql, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("id", selectByIdSql, StringComparison.OrdinalIgnoreCase);
            // Check for parameter placeholder
            Assert.Contains("@", selectByIdSql);
        }

        [Fact]
        public void GenerateSelectAllSql_ForProductEntity_ReturnsValidSql()
        {
            // Arrange & Act
            string selectAllSql = SqlGenerator.GenerateSelectAllSql<Product>();

            // Assert
            Assert.Contains("SELECT", selectAllSql, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("FROM", selectAllSql, StringComparison.OrdinalIgnoreCase);
            // Should not contain a WHERE clause
            Assert.DoesNotContain("WHERE", selectAllSql, StringComparison.OrdinalIgnoreCase);
            // Should include the appropriate table name
            string tableName = SqlGenerator.GetTableName<Product>().ToLower();
            Assert.Contains(tableName, selectAllSql.ToLower());
        }

        [Fact]
        public void GetPostgreSqlType_ForDifferentCSharpTypes_ReturnsCorrectSqlTypes()
        {
            // This is a private method, so we'll test it indirectly through the CREATE TABLE statement
            string createTableSql = SqlGenerator.GenerateCreateTableSql<Product>();

            // Assert for different PostgreSQL types
            Assert.Contains("INTEGER", createTableSql, StringComparison.OrdinalIgnoreCase); // for Id and StockQuantity
            Assert.Contains("TEXT", createTableSql, StringComparison.OrdinalIgnoreCase); // for Name
            Assert.Contains("NUMERIC", createTableSql, StringComparison.OrdinalIgnoreCase); // for Price (decimal)
        }
    }
}
