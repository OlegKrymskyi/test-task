using System.ComponentModel.DataAnnotations;

namespace AngelCo.Domain
{
    public class Experience: IEntity
    {
        public int? Id { get; set; }

        [StringLength(200)]
        public string CompanyName { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Role { get; set; }

        public int? StartAtYear { get; set; }

        public int? StartAtMonth { get; set; }

        public int? EndedAtYear { get; set; }

        public int? EndedAtMonth { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
