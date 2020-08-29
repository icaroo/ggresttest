using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using taf.Models;
using TechTalk.SpecFlow;

namespace taf.Steps
{
    [Binding]
    public class InvalidRegisterSteps
    {

        RestClient client;
        RestRequest request;
        // //IResponse represent a HTTP Response with Status, Headers and Body
        IRestResponse response;

        [Given(@"the endpoint is ""(.*)"""), Scope(Tag = "InvalidRegister")]
        public void GivenTheEndpointIs(string endpoint)
        {
            client = new RestClient("https://reqres.in/api/");
            request = new RestRequest($"{endpoint}", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", ParameterType.RequestBody);
        }


        [When(@"I register account with ""(.*)"""), Scope(Tag = "InvalidRegister")]
        public void WhenIRegisterAccountWith(string email)
        {
            var accountInfo = new AccountInfo();
            accountInfo.Email = email;
           

            Console.Out.WriteLine("Using ObjectDumper accountInfo results  {0}", ObjectDumper.Dump(accountInfo));

            //same output, different format
            //Console.Out.WriteLine("Using JsonConvert Users accountInfo {0}", JsonConvert.SerializeObject(accountInfo));


            request.AddJsonBody(JsonConvert.SerializeObject(accountInfo));

            response = client.Execute(request);
        }

        

        [Then(@"the response has status code (.*)"), Scope(Tag = "InvalidRegister")]
        public void ThenTheResponseHasStatusCode(int expectedStatusCode)
        {
            HttpStatusCode statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            numericStatusCode.Should().Be(expectedStatusCode);
            Console.Out.WriteLine("Status Code: {0} - {1}", numericStatusCode, response.StatusCode);
        }


        [Then(@"the response should return an error")]
        public void ThenTheResponseShouldReturnAnError()
        {
            Console.Out.WriteLine("Status {0}", response.StatusCode);

            Console.Out.WriteLine("Invalid Register results  {0} - {1}", ObjectDumper.Dump((int)response.StatusCode), ObjectDumper.Dump(response.Content));

            string res = response.Content;

            

            res.Should().NotBeNullOrEmpty();

           
            string statusDescription = response.StatusDescription;
            string protocolVersion = response.ProtocolVersion.ToString();
            Console.Out.WriteLine("statusDescription {0} ", statusDescription);
         
            
        }
    }
}
