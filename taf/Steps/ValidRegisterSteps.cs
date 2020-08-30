using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.Net;
using taf.Helpers;
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

        
        // //IResponse represent a HTTP Response with Status, Headers and Body
        IRestResponse response;
        private RestAPI restApi;

        [Given(@"the endpoint is ""(.*)""")]
        public void GivenTheEndpointIs(string endpoint)
        {
           restApi = new RestAPI(endpoint);
        }

        
                
        
        [When(@"I register account with ""(.*)"" and ""(.*)"""), Scope(Tag = "ValidRegister")]
        public void WhenIRegisterAccountWithAnd(string email, string password)
        {
            var accountInfo = new Registration();
            accountInfo.Email = email;
            accountInfo.Password = password;

            //same output, different format
            //Console.Out.WriteLine("Using JsonConvert Users accountInfo {0}", JsonConvert.SerializeObject(accountInfo));
            Console.Out.WriteLine("Using ObjectDumper accountInfo results  {0}", ObjectDumper.Dump(accountInfo));

            response = restApi.PostRequest(accountInfo);
        }

        //[Then(@"the response has status code (.*)")]
        [Then(@"the response has status code (.*)"), Scope(Tag = "ValidRegister")]
        public void ThenTheResponseHasStatusCode(int expectedStatusCode)
        {
            //HttpStatusCode statusCode = response.StatusCode;
            //int numericStatusCode = (int)statusCode;
            //numericStatusCode.Should().Be(expectedStatusCode);
            Console.Out.WriteLine("status code: {0}  - {1} ", response.StatusCode, response.StatusDescription);

            ((int)response.StatusCode).Should().Be(expectedStatusCode);

            //Assert.That(numericStatusCode, Is.EqualTo(expectedStatusCode))
            //Assert.That((int)response.StatusCode, Is.EqualTo(expectedStatusCode))
        }


        [Then(@"the response should return a token")]
        public void ThenTheResponseShouldReturnAToken()
        {

            RegistrationResponse apiResponseObj = restApi.PostResponse<RegistrationResponse>();

            //same output, different format
            //Console.Out.WriteLine("Using JsonConvert Error results {0}" , JsonConvert.SerializeObject(apiResponseObj));
            Console.Out.WriteLine("output {0}", ObjectDumper.Dump(apiResponseObj));
            Console.Out.WriteLine("Token {0}", ObjectDumper.Dump(apiResponseObj.Token));

            apiResponseObj.Token.Should().NotBeNullOrEmpty();

                      
        }

     
    }
}
