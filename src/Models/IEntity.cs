namespace SimpleORM.Models
{
    /// <summary>
    /// Interface for all database entities
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Primary key for the entity
        /// </summary>
        int Id { get; set; }
    }
}
