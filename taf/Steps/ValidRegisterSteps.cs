using System;
using TechTalk.SpecFlow;

namespace taf.Steps
{
    [Binding]
    public class ValidRegisterSteps
    {
        [Given(@"the endpoint is ""(.*)""")]
        public void GivenTheEndpointIs(string endpoint)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I register account with ""(.*)"" and ""(.*)""")]
        public void WhenIRegisterAccountWithAnd(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }
        
        //[Then(@"the response has status code (.*)")]
        //public void ThenTheResponseHasStatusCode(int p0)
        //{
        //    ScenarioContext.Current.Pending();
        //}
        
        [Then(@"the response should return a token")]
        public void ThenTheResponseShouldReturnAToken()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
