using System;
using TechTalk.SpecFlow;

namespace taf.Steps
{
    [Binding]
    public class RequestUsersListSteps
    {
        [Given(@"the endpoint name is ""(.*)""")]
        public void GivenTheEndpointNameIs(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I retrieve the Users List")]
        public void WhenIRetrieveTheUsersList()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"all users are listed in the response")]
        public void ThenAllUsersAreListedInTheResponse()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
