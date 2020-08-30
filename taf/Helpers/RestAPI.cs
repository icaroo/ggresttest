using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using taf.Models;

namespace taf.Helpers
{
    /// <summary>
    /// Provide configuration to endpoint and manipulate RestResponse using RestSharp
    /// </summary>
   public class RestAPI
    {

        private RestClient client;
        private RestRequest request;
        // //IResponse represent a HTTP Response with Status, Headers and Body
        private IRestResponse response;

        private string endpoint;

        public RestAPI(string endpoint)
        {
            this.endpoint = endpoint;
        }

        public IRestResponse GetRequest() {
            
            client = new RestClient("https://reqres.in/api/");
            request = new RestRequest($"{endpoint}", Method.GET);

            //Add parameter to filter amount of number
            //request.AddParameter("per_page", 12);

            return response = client.Execute(request);

            
        }


        public IRestResponse PostRequest(Registration registerData) {

            client = new RestClient("https://reqres.in/api/");
            request = new RestRequest($"{endpoint}", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", ParameterType.RequestBody);

            request.AddJsonBody(JsonConvert.SerializeObject(registerData));

           return response = client.Execute(request);

            
        }



        /// <summary>
        /// Generic PostResponse, deserializing the content to object of a class model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T PostResponse<T>()
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
