using Newtonsoft.Json;
using System;

namespace AngelCo.Parser.Objects
{
    public class DateRangeObject
    {
        [JsonProperty("started_at")]
        public DateRangeItemObject StartAt { get; set; }

        [JsonProperty("ended_at")]
        public DateRangeItemObject EndedAt { get; set; }
    }

    public class DateRangeItemObject
    {
        [JsonProperty("year")]
        public int? Year { get; set; }

        [JsonProperty("month")]
        public int? Month { get; set; }
    }
}
