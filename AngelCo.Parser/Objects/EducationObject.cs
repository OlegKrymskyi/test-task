using Newtonsoft.Json;

namespace AngelCo.Parser.Objects
{
    public class EducationObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("degree_type")]
        public string DegreeType { get; set; }

        [JsonProperty("full_degree_name")]
        public string FullDegreeName { get; set; }

        [JsonProperty("graduation_month")]
        public int? GraduationMonth { get; set; }

        [JsonProperty("graduation_year")]
        public int? GraduationYear { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
