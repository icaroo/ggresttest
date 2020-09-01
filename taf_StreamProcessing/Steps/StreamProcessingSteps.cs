using Confluent.Kafka;
using FluentAssertions;
using FluentAssertions.Common;
using FluentAssertions.Equivalency;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using taf_StreamProcessing.Helpers;
using taf_StreamProcessing.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.CommonModels;

namespace taf_StreamProcessing.Steps
{
    [Binding]
    public class StreamProcessingSteps
    {
     
        
      
        List<string> receivedMessages = new List<string>();

        List<string> producedMessages = new List<string>();

        List<Car> cars = new List<Car>();

        List<Car> carsReceived = new List<Car>();

        Producer producer = new Producer();

        Consumer consumer = new Consumer();

        [Given(@"I have cars data messages")]
        public void GivenIHaveCarsDataMessages()
        {
            cars = JsonConvertObject.GenerateData<Car>();

            Console.Out.WriteLine("JsonObject data {0}", ObjectDumper.Dump(cars));

            producedMessages = producer.CreateProducer(cars);

            Console.Out.WriteLine("producedMessages data {0}", ObjectDumper.Dump(producedMessages));

        }
       
        
        


        [Then(@"I should receive a list of cars messages")]
        public void ThenIShouldReceiveAListOfCarsMessages()
        {
            
            //List<string> consumedMessages = new List<string>();

            (receivedMessages, carsReceived) = consumer.CreateConsumer<Car>();


        }

     

        [Then(@"The messages received should be same as sent")]
        public void ThenTheMessagesReceivedShouldBeSameAsSent()
        {
             //using List
            receivedMessages.Should().BeEquivalentTo(producedMessages);
            receivedMessages.Should().Equal(producedMessages);
            receivedMessages.Should().Contain(producedMessages);

            //carsReceived[0].IsSport = true; //to test
            
            //using Object
            carsReceived.Should().BeEquivalentTo(cars);

            //Uncomment to see the Outputs
            //Console.Out.WriteLine("givenMessage Data {0}", ObjectDumper.Dump(receivedMessages));

            //Console.Out.WriteLine("producedMessages Data {0}", ObjectDumper.Dump(producedMessages));

            //Console.Out.WriteLine("carReceived Data {0}", ObjectDumper.Dump(carsReceived));

            //Console.Out.WriteLine("cars Data {0}", ObjectDumper.Dump(cars));









        }
    }
}
