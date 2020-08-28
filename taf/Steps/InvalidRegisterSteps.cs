using FluentAssertions;
using RestSharp;
using System;
using System.Net;
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
        public void WhenIRegisterAccountWith(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        

        [Then(@"the response has status code (.*)"), Scope(Tag = "InvalidRegister")]
        public void ThenTheResponseHasStatusCode(int expectedStatusCode)
        {
            HttpStatusCode statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            numericStatusCode.Should().Be(expectedStatusCode);
            Console.Out.WriteLine("status code: {0}", numericStatusCode);
        }


        [Then(@"the response should return an error")]
        public void ThenTheResponseShouldReturnAnError()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
