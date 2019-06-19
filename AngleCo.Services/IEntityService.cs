namespace AngleCo.Services
{
    using AngelCo.Domain;
    using System.Linq;

    /// <summary>
    /// Represents entity management service interface.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface IEntityService<T> where T : class, IEntity
    {
        /// <summary>
        /// Gets the entity with specified id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <returns>Entity with specified id if entity exists; otherwise null.</returns>
        T Get(int id);

        /// <summary>
        /// Loads all entities.
        /// </summary>
        /// <returns>The list of entities.</returns>
        IQueryable<T> All();

        /// <summary>
        /// Saves specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Save(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes the entity with specified id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        void Delete(int id);
    }
}
