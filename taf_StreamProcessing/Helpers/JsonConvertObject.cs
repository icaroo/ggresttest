using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace taf_StreamProcessing.Helpers
{
   public static class JsonConvertObject
    {

       

        ////Convert jsonData to a List of Object
        public static List<T> ToObjectData<T>(this string jsonParams)
        {
            try
            {
                if (string.IsNullOrEmpty(jsonParams)) return new List<T>();
                JObject userObject = JObject.Parse(jsonParams);
                JArray data = (JArray)userObject["data"];
                List<T> objectData = JsonConvert.DeserializeObject<List<T>>(data.ToString());
                return objectData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to Convert - error {0}", e);
                throw;
               
                
            }
        }

  
       
        //Convert jsonData to Object
        public static T ToSingleObjectData<T>(this string jsonParams)
        {
            try
            {
                T objectData = JsonConvert.DeserializeObject<T>(jsonParams);
                return objectData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to Convert - error {0}", e);
                throw;
            }
        }

       

    }
}

