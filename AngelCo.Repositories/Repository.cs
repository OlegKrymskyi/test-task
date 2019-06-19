namespace AngelCo.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngelCo.Domain;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents base repository class.
    /// </summary>
    /// <typeparam name="T">The entity.</typeparam>
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Stores database context.
        /// </summary>
        private DomainContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public Repository(DomainContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets database context.
        /// </summary>
        protected DomainContext Context
        {
            get { return this.context; }
        }

        /// <summary>
        /// Gets database entities with specified type.
        /// </summary>
        protected DbSet<T> Set
        {
            get
            {
                return this.context.Set<T>();
            }
        }

        /// <summary>
        /// Gets entity by id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <returns>The entity with specified id otherwise null</returns>
        public T GetByKey(int id)
        {
            return this.Set.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Saves specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if entity was saved successfully otherwise <c>false</c>.</returns>
        public bool Save(T entity)
        {
            var entry = this.Context.Entry<T>(entity);

            if (!entity.Id.HasValue)
            {
                this.Set.Add(entity);
            }
            else
            {
                this.Set.Attach(entity);
                entry.State = EntityState.Modified;
            }

            this.Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Deletes specified entity.
        /// </summary>
        /// <param name="entity">The entity which should be deleted.</param>
        /// <returns><c>true</c> if entity was deleted successfully otherwise <c>false</c>.</returns>
        public virtual bool Delete(T entity)
        {
            if (this.IsEntityInUse(entity))
            {
                throw new EntityInUseExpection(entity);
            }

            this.Set.Remove(entity);
            this.Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Deletes entity with specified id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <returns><c>true</c> if entity was deleted successfully otherwise <c>false</c>.</returns>
        public bool Delete(int id)
        {
            return this.Delete(this.GetByKey(id));
        }

        /// <summary>
        /// Loads all entities from data context.
        /// </summary>
        /// <returns>The list of all entities.</returns>
        public virtual IQueryable<T> All()
        {
            return this.Set;
        }

        /// <summary>
        /// Takes specified number of entities starting from specified number.
        /// </summary>
        /// <param name="skip">The number of entities which should be skipped.</param>
        /// <param name="take">The number of entities which should be taken.</param>
        /// <returns>The list of entities starting from specified entity index.</returns>
        public IList<T> SkipTake(int skip, int take)
        {
            return this.All().Skip(skip).Take(take).ToList();
        }

        /// <summary>
        /// Gets the total count of entities.
        /// </summary>
        /// <returns>The number of entities.</returns>
        public int TotalCount()
        {
            return this.All().Count();
        }

        /// <summary>
        /// Gets the list of entities by specified conditions.
        /// </summary>
        /// <param name="predicate">The conditions.</param>
        /// <returns>The list of entities which are satisfied to specified conditions.</returns>
        public IQueryable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this.All().Where(predicate);
        }

        /// <summary>
        /// Gets the entity which is satisfied specified conditions.
        /// </summary>
        /// <param name="predicate">The conditions.</param>
        /// <returns>The entity which are satisfied to specified conditions or null.</returns>
        public T SingleOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this.All().SingleOrDefault(predicate);
        }

        /// <summary>
        /// Checks that specified entity is used by one of other entity.
        /// </summary>
        /// <param name="entity">The entity which should be checked.</param>
        /// <returns><c>true</c> if entity is in use otherwise <c>false</c>.</returns>
        protected virtual bool IsEntityInUse(T entity)
        {
            return false;

            //return this.Context.Database.SqlQuery<bool>
            //    ("SELECT dbo.IsEnityInUse(@id, @type)",
            //    new SqlParameter("@id", entity.Id),
            //    new SqlParameter("@type", typeof(T).Name)).FirstOrDefault();
        }
    }
}
