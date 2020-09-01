using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace taf_StreamProcessing.Models
{
   public class Car
    {
        [JsonProperty("Brand")]
        public string Brand { get; set; }
        [JsonProperty("Model")]
        public string Model { get; set; }
        [JsonProperty("Doors")]
        public int Doors { get; set; }
        [JsonProperty("IsSport")]
        public bool IsSport { get; set; }

        [JsonProperty("data")]
        public List<Car> Cars { get; set; }


        //public Car(string brand, string model, int doors, bool sport)
        //{
        //    this.Brand = brand;
        //    this.Model = model;
        //    this.Doors = doors;
        //    this.IsSport = sport;
        //}
    }
}
