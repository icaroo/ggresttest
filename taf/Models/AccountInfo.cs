using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace taf.Models
{
    class AccountInfo
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
       
            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("password")]
            public string Password { get; set; }
        


    }
}
