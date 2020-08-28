using System;
using TechTalk.SpecFlow;

namespace taf.Steps
{
    [Binding]
    public class InvalidRegisterSteps
    {
        //[When(@"I register account with ""(.*)""")]
        //public void WhenIRegisterAccountWith(string p0)
        //{
        //    ScenarioContext.Current.Pending();
        //}
        
        [Then(@"the response should return an error")]
        public void ThenTheResponseShouldReturnAnError()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
