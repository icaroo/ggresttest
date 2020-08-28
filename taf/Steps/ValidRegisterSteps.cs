using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;
using System;
using taf.Models;
using TechTalk.SpecFlow;

namespace taf.Steps
{
    [Binding]
    public class ValidRegisterSteps

    {

        RestClient client;
        RestRequest request;
        // //IResponse represent a HTTP Response with Status, Headers and Body
        IRestResponse response;

        [Given(@"the endpoint is ""(.*)""")]
        public void GivenTheEndpointIs(string endpoint)
        {
            client = new RestClient("https://reqres.in/api/");
            request = new RestRequest($"{endpoint}", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", ParameterType.RequestBody);
        }
        
        [When(@"I register account with ""(.*)"" and ""(.*)""")]
        public void WhenIRegisterAccountWithAnd(string email, string password)
        {
            var accountInfo = new AccountInfo();
            accountInfo.Email = email;
            accountInfo.Password = password;


            Console.Out.WriteLine("Using ObjectDumper accountInfo results  {0}", ObjectDumper.Dump(accountInfo));

            //same output, different format
            Console.Out.WriteLine("Using JsonConvert Users accountInfo {0}", JsonConvert.SerializeObject(accountInfo));


            request.AddJsonBody(JsonConvert.SerializeObject(accountInfo));

            response = client.Execute(request);
        }

        //[Then(@"the response has status code (.*)")]
        //public void ThenTheResponseHasStatusCode(int p0)
        //{
        //    ScenarioContext.Current.Pending();
        //}

        
        [Then(@"the response should return a token")]
        public void ThenTheResponseShouldReturnAToken()
        {
            //var apiResponseObject = JsonConvert.DeserializeObject<>(response.Content);

            Console.Out.WriteLine("Status {0}", response.StatusCode);

            Console.Out.WriteLine("Using ObjectDumper Register results  {0} - {1}", ObjectDumper.Dump((int)response.StatusCode), ObjectDumper.Dump(response.Content));

            string res = response.Content;

            res.Should().NotBeNullOrEmpty();
            
        }
    }
}
