using Newtonsoft.Json;

namespace taf.Models
{
    public partial class UserResponse
    {
        public class Ad
        {
            [JsonProperty("company")]
            public string Company { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("text")]
            public string Text { get; set; }
        }


    }
}
