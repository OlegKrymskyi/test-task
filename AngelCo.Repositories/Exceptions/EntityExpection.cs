namespace AngelCo.Repositories
{
    using AngelCo.Domain;
    using System;

    public class EntityExpection : Exception
    {
        private IEntity entity;

        public EntityExpection(IEntity entity, string message)
            : base(message)
        {
            this.entity = entity;
        }

        public IEntity Entity { get { return this.entity; } }
    }
}
