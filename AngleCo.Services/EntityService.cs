namespace AngleCo.Services
{
    using AngelCo.Domain;
    using AngelCo.Repositories;
    using System.Linq;

    /// <summary>
    /// Represents base entity management service class.
    /// </summary>
    public class EntityService<T> : IEntityService<T> where T : class, IEntity
    {
        public EntityService(IRepository<T> repository)
        {
            this.Repository = repository;
        }

        protected virtual IRepository<T> Repository { get; private set; }

        /// <summary>
        /// Gets the entity with specified id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <returns>Entity with specified id if entity exists; otherwise null.</returns>
        public virtual T Get(int id)
        {
            return this.Repository.GetByKey(id);
        }

        /// <summary>
        /// Loads all entities.
        /// </summary>
        /// <returns>The list of entities.</returns>
        public virtual IQueryable<T> All()
        {
            return this.Repository.All();
        }

        /// <summary>
        /// Saves specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Save(T entity)
        {
            this.Repository.Save(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(T entity)
        {
            this.Repository.Delete(entity);
        }

        /// <summary>
        /// Deletes the entity with specified id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        public void Delete(int id)
        {
            var entity = this.Get(id);

            if (entity != null)
            {
                this.Delete(entity);
            }
        }
    }
}
