using Newtonsoft.Json;

namespace AngelCo.Parser.Objects
{
    public class ExperienceObject
    {
        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("pending_approval")]
        public bool? PendingApproval { get; set; }

        [JsonProperty("who_confirms")]
        public string WhoConfirms { get; set; }

        [JsonProperty("featured")]
        public bool? Featured { get; set; }

        [JsonProperty("startup_company_name")]
        public string StartupCompanyName { get; set; }

        [JsonProperty("dates_for_select")]
        public DateRangeObject DatesForSelect { get; set; }
    }
}
