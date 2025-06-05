namespace SimpleORM.Models
{
    /// <summary>
    /// Sample entity representing a product
    /// </summary>
    public class Product : IEntity
    {
        /// <summary>
        /// Primary key (required by IEntity)
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Name of the product
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Price of the product
        /// </summary>
        public decimal Price { get; set; }
        
        /// <summary>
        /// Number of items in stock
        /// </summary>
        public int StockQuantity { get; set; }
    }
}
