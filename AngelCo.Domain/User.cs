using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngelCo.Domain
{
    public class User: IEntity
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(200)]
        public string ExternalId { get; set; }

        [StringLength(300)]
        public string FullName { get; set; }

        [StringLength(500)]
        public string ProfileLink { get; set; }

        [StringLength(500)]
        public string AvatarUrl { get; set; }

        public string Bio { get; set; }

        [StringLength(200)]
        public string Type { get; set; }

        [StringLength(500)]
        public string FacebookUrl { get; set; }

        [StringLength(500)]
        public string LinkedInUrl { get; set; }

        [StringLength(500)]
        public string TwitterUrl { get; set; }

        public IList<Experience> Experiences { get; set; }

        public IList<Education> Educations { get; set; }

        public IList<Location> Locations { get; set; }

        public IList<Market> Markets { get; set; }
    }
}
