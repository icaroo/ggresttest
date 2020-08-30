using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using taf.Helpers;
using taf.Models;
using TechTalk.SpecFlow;

namespace taf.StepsDefinition
{
    [Binding]   
    public class InvalidRegisterSteps 
    {

        //IResponse represent a HTTP Response with Status, Headers and Body
        IRestResponse response;

        private RestAPI restApi;

      

        [Given(@"the endpoint is ""(.*)"""), Scope(Tag = "InvalidRegister")]
        public void GivenTheEndpointIs(string endpoint)
        {
            restApi = new RestAPI(endpoint);

        }


        [When(@"I register account with ""(.*)"""), Scope(Tag = "InvalidRegister")]
        public void WhenIRegisterAccountWith(string email)
        {
            var accountInfo = new Registration();
            accountInfo.Email = email;


            //same output, different format
            //Console.Out.WriteLine("Using JsonConvert Users accountInfo {0}", JsonConvert.SerializeObject(accountInfo));
            Console.Out.WriteLine("Registration results  {0}", ObjectDumper.Dump(accountInfo));

            response = restApi.PostRequest(accountInfo);


        }

        

        [Then(@"the response has status code (.*)"), Scope(Tag = "InvalidRegister")]
        public void ThenTheResponseHasStatusCode(int expectedStatusCode)
        {
            //HttpStatusCode statusCode = response.StatusCode;
            //int numericStatusCode = (int)statusCode;
            //numericStatusCode.Should().Be(expectedStatusCode);
            Console.Out.WriteLine("status code: {0}  - {1} ", (int)response.StatusCode, response.StatusDescription);

            ((int)response.StatusCode).Should().Be(expectedStatusCode);

            //Assert.That(numericStatusCode, Is.EqualTo(expectedStatusCode))
            //Assert.That((int)response.StatusCode, Is.EqualTo(expectedStatusCode))
        }


        [Then(@"the response should return an error")]
        public void ThenTheResponseShouldReturnAnError()
        {

            RegistrationResponse apiResponseObj = restApi.PostResponse<RegistrationResponse>();

            //same output, different format
            //Console.Out.WriteLine("Using JsonConvert Error results {0}" , JsonConvert.SerializeObject(apiResponseObj));
            Console.Out.WriteLine("output {0}", ObjectDumper.Dump(apiResponseObj));
            Console.Out.WriteLine("Error results  {0}", ObjectDumper.Dump(apiResponseObj.Error));

            apiResponseObj.Error.Should().NotBeNullOrEmpty();


        }
    }
}
