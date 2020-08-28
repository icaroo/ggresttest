using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.Net;
using taf.Models;
using TechTalk.SpecFlow;

namespace taf.Steps
{
    [Binding]
    public class ValidRegisterSteps

    {
        //ScenarioContext _scenarioContext;

        //public ValidRegisterSteps(ScenarioContext scenarioContext)
        //{
        //    _scenarioContext = scenarioContext;
        //}

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

        
                
        //[When(@"I register account with ""(.*)"" and ""(.*)""")]
        [When(@"I register account with ""(.*)"" and ""(.*)"""), Scope(Tag = "ValidRegister")]
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
        [Then(@"the response has status code (.*)"), Scope(Tag = "ValidRegister")]
        public void ThenTheResponseHasStatusCode(int expectedStatusCode)
        {
            HttpStatusCode statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            numericStatusCode.Should().Be(expectedStatusCode);
            Console.Out.WriteLine("status code: {0}", numericStatusCode);

            //Assert.That(numericStatusCode, Is.EqualTo(code))
            //Assert.That((int)response.StatusCode, Is.EqualTo(code))
        }


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
