using System.ComponentModel.DataAnnotations;

namespace AngelCo.Domain
{
    public class Education: IEntity
    {
        public int? Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string DegreeType { get; set; }

        [StringLength(200)]
        public string FullDegreeName { get; set; }

        public int? GraduationMonth { get; set; }

        public int? GraduationYear { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
