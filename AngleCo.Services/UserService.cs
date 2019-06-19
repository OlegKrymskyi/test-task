using AngelCo.Domain;
using AngelCo.Repositories;
using System.Linq;

namespace AngleCo.Services
{
    public class UserService : EntityService<User>
    {
        public UserService(IRepository<User> repository) : base(repository)
        {
        }

        public User GetByExternalId(string externalId)
        {
            return All().Where(x => x.ExternalId == externalId).FirstOrDefault();
        }
    }
}
