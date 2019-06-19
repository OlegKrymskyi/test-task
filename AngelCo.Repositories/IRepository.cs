namespace AngelCo.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Domain;

    /// <summary>
    /// Represents base domain repository interface.
    /// </summary>
    public interface IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Gets entity by id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <returns>The entity with specified id otherwise null.</returns>
        T GetByKey(int id);

        /// <summary>
        /// Saves specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        bool Save(T entity);

        /// <summary>
        /// Deletes specified entity.
        /// </summary>
        /// <param name="entity">The entity which should be deleted.</param>
        /// <returns><c>true</c> if entity was deleted successfully otherwise <c>false</c>.</returns>
        bool Delete(T entity);

        /// <summary>
        /// Deletes entity with specified id.
        /// </summary>
        /// <param name="entityid">The entity id.</param>
        /// <returns><c>true</c> if entity was deleted successfully otherwise <c>false</c>.</returns>
        bool Delete(int entityid);

        /// <summary>
        /// Loads all entities from data context.
        /// </summary>
        /// <returns>The list of all entities.</returns>
        IQueryable<T> All();

        /// <summary>
        /// Takes specified number of entities starting from specified number.
        /// </summary>
        /// <param name="skip">The number of entities which should be skipped.</param>
        /// <param name="take">The number of entities which should be taken.</param>
        /// <returns>The list of entities starting from specified entity index.</returns>
        IList<T> SkipTake(int skip, int take);

        /// <summary>
        /// Gets the total count of entities.
        /// </summary>
        /// <returns>The number of entities.</returns>
        int TotalCount();

        /// <summary>
        /// Gets the list of entities by specified conditions.
        /// </summary>
        /// <param name="predicate">The conditions.</param>
        /// <returns>The list of entities which are satisfied to specified conditions.</returns>
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets the entity which is satisfied specified conditions.
        /// </summary>
        /// <param name="predicate">The conditions.</param>
        /// <returns>The entity which are satisfied to specified conditions or null.</returns>
        T SingleOrDefault(Expression<Func<T, bool>> predicate);
    }
}
