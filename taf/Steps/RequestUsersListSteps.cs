using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Net;
using taf.Helpers;
using taf.Models;
using TechTalk.SpecFlow;

namespace taf.Steps
{
    [Binding]
    public class RequestUsersListSteps
    {
        //IResponse represent a HTTP Response with Status, Headers and Body
        IRestResponse response;

        RestAPI restApi;

        [Given(@"the endpoint name is ""(.*)""")]
        public void GivenTheEndpointNameIs(string endpoint)
        {
            restApi = new RestAPI(endpoint);

        }

        [When(@"I retrieve the Users List")]
        public void WhenIRetrieveTheUsersList()
        {
                    
            response = restApi.GetRequest();  
        }

      


        [Then(@"the response has status code (200|404|500)")]
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

        [Then(@"the users are listed in the response")]
        public void ThenAllUsersAreListedInTheResponse()
        {
            
            UserResponse userResponseObj = JsonConvert.DeserializeObject<UserResponse>(response.Content);

            Console.Out.WriteLine("Users results  {0}", ObjectDumper.Dump(userResponseObj.Users));

            //same output, different format
            //Console.Out.WriteLine("Using JsonConvert Users results {0}" , JsonConvert.SerializeObject(userResponseObj.Users));

            userResponseObj.Users.Count.Should().Be(userResponseObj.PerPage);
            Console.Out.WriteLine("Users {0}  - PerPage {1} ", userResponseObj.Users.Count, userResponseObj.PerPage);
            userResponseObj.Users.Should().NotBeNull();

        }


       
    }
}
