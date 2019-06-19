using AngelCo.Domain;

namespace AngelCo.Repositories
{
    public class EntityInUseExpection : EntityExpection
    {
        public EntityInUseExpection(IEntity entity) : base(entity, "Entity is in use") { }
    }
}
