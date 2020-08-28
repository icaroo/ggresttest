using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace taf.Models
{
    public partial  class RootObject
    {

       
            [JsonProperty("page")]
            public int Page { get; set; }

            [JsonProperty("per_page")]
            public int PerPage { get; set; }

            [JsonProperty("total")]
            public int Total { get; set; }

            [JsonProperty("total_pages")]
            public int TotalPages { get; set; }

            [JsonProperty("data")]
            public List<User> Users { get; set; }

            [JsonProperty("ad")]
            public Ad Ads { get; set; }
        


    }
}
