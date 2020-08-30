using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace taf.Models
{
   public class Response
    {
   
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
