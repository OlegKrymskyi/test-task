using System.ComponentModel.DataAnnotations;

namespace AngelCo.Domain
{
    public class Location: IEntity
    {
        public int? Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
