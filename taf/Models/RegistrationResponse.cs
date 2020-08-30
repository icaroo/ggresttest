using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace taf.Models
{
   public class RegistrationResponse
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
