using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Net;
using taf.Models;
using TechTalk.SpecFlow;

namespace taf.Steps
{
    [Binding]
    public class RequestUsersListSteps
    {
        RestClient client;
        RestRequest request;
        // //IResponse represent a HTTP Response with Status, Headers and Body
        IRestResponse response;

        [Given(@"the endpoint name is ""(.*)""")]
        public void GivenTheEndpointNameIs(string endpoint)
        {
            client = new RestClient("https://reqres.in/api/");
            request = new RestRequest($"{endpoint}", Method.GET);
            

        }

        [When(@"I retrieve the Users List")]
        public void WhenIRetrieveTheUsersList()
        {
            response = client.Execute(request);
        }

      


        [Then(@"the response has status code (200|404|500)")]
        public void ThenTheResponseHasStatusCode(int expectedStatusCode)
        {
            HttpStatusCode statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            numericStatusCode.Should().Be(expectedStatusCode);
            Console.Out.WriteLine("status code: {0}", numericStatusCode);

            //Assert.That(numericStatusCode, Is.EqualTo(code))
            //Assert.That((int)response.StatusCode, Is.EqualTo(code))

        }

        [Then(@"all users are listed in the response")]
        public void ThenAllUsersAreListedInTheResponse()
        {
            
            RootObject apiResponseObject = JsonConvert.DeserializeObject<RootObject>(response.Content);

            Console.Out.WriteLine("Using ObjectDumper Users results  {0}", ObjectDumper.Dump(apiResponseObject.Users));
            
            //same output, different format
            //Console.Out.WriteLine("Using JsonConvert Users results {0}" , JsonConvert.SerializeObject(apiResponseObject.Users));

            apiResponseObject.Users.Should().NotBeNull();

        }
    }
}
