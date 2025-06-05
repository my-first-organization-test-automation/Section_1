using System;
using System.Collections.Generic;
using SimpleORM.Database;
using SimpleORM.Models;

namespace SimpleORM;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Simple ORM Demonstration\n");

        DemonstratePostgreSqlORM();

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    static void DemonstratePostgreSqlORM()
    {
        Console.WriteLine("\n=== PostgreSQL ORM Demonstration ===\n");

        // Create a database context with a connection string
        // Replace with your PostgreSQL connection string
        var dbContext = new SimplePostgreSqlDbContext(
            "Host=localhost;Database=simple_orm;Username=postgres;Password=web@1234");

        // Create a new product
        var product1 = new Product
        {
            Name = "Laptop",
            Price = 999.99m,
            StockQuantity = 10
        };

        try
        {
            // Add the product to the database
            Console.WriteLine("Adding a new product...");
            dbContext.Add(product1);
            Console.WriteLine($"Product added with ID: {product1.Id}");

            // Create another product
            var product2 = new Product
            {
                Name = "Smartphone",
                Price = 699.99m,
                StockQuantity = 15
            };

            // Add the second product to the database
            Console.WriteLine("\nAdding another product...");
            dbContext.Add(product2);
            Console.WriteLine($"Product added with ID: {product2.Id}");

            // Retrieve all products
            Console.WriteLine("\nRetrieving all products:");
            List<Product> allProducts = dbContext.GetAll<Product>();
            foreach (var product in allProducts)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: ${product.Price}, Stock: {product.StockQuantity}");
            }

            // Update a product
            Console.WriteLine("\nUpdating product 1...");
            product1.Price = 899.99m;
            product1.StockQuantity = 5;
            dbContext.Update(product1);

            // Retrieve the updated product
            Console.WriteLine("\nRetrieving updated product:");
            var updatedProduct = dbContext.GetById<Product>(product1.Id);
            Console.WriteLine($"ID: {updatedProduct.Id}, Name: {updatedProduct.Name}, Price: ${updatedProduct.Price}, Stock: {updatedProduct.StockQuantity}");

            // Remove a product
            Console.WriteLine("\nRemoving product 2...");
            dbContext.Remove(product2);

            // Retrieve all products again
            Console.WriteLine("\nRetrieving all products after removal:");
            allProducts = dbContext.GetAll<Product>();
            foreach (var product in allProducts)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: ${product.Price}, Stock: {product.StockQuantity}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
            Console.WriteLine("Make sure your PostgreSQL connection string is correct and the database exists.");
        }
    }
}
