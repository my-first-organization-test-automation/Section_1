using System.Collections.Generic;
using SimpleORM.Models;

namespace SimpleORM.Database;

/// <summary>
/// Interface for a database context that manages entities
/// </summary>
public interface IDbContext
{
    /// <summary>
    /// Add a new entity to the database
    /// </summary>
    /// <typeparam name="T">Type of entity (must implement IEntity)</typeparam>
    /// <param name="entity">Entity to add</param>
    void Add<T>(T entity) where T : class, IEntity;

    /// <summary>
    /// Update an existing entity in the database
    /// </summary>
    /// <typeparam name="T">Type of entity (must implement IEntity)</typeparam>
    /// <param name="entity">Entity to update</param>
    void Update<T>(T entity) where T : class, IEntity;

    /// <summary>
    /// Remove an entity from the database
    /// </summary>
    /// <typeparam name="T">Type of entity (must implement IEntity)</typeparam>
    /// <param name="entity">Entity to remove</param>
    void Remove<T>(T entity) where T : class, IEntity;

    /// <summary>
    /// Get an entity by its ID
    /// </summary>
    /// <typeparam name="T">Type of entity (must implement IEntity)</typeparam>
    /// <param name="id">ID of the entity to retrieve</param>
    /// <returns>The entity if found, null otherwise</returns>
    T GetById<T>(int id) where T : class, IEntity;

    /// <summary>
    /// Get all entities of a specific type
    /// </summary>
    /// <typeparam name="T">Type of entity (must implement IEntity)</typeparam>
    /// <returns>List of all entities of type T</returns>
    List<T> GetAll<T>() where T : class, IEntity;

    /// <summary>
    /// Save all changes to the database
    /// </summary>
    /// <returns>Number of affected rows</returns>
}
