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
     
        private KafkaSetting kafkaSettings = new KafkaSetting();
      
        List<string> receivedMessages = new List<string>();

        List<string> producedMessages = new List<string>();

        List<Car> cars = new List<Car>();

        List<Car> carsReceived = new List<Car>();


        [Given(@"I have cars data messages")]
        public void GivenIHaveCarsDataMessages()
        {
          
            var myJsonString = File.ReadAllText("..\\..\\..\\carsData.json");
            var myJsonObject = JsonConvert.DeserializeObject<Car>(myJsonString);
            Console.Out.WriteLine("myJsonObject datata {0}", ObjectDumper.Dump(myJsonObject));

            //option 1
            //cars = myJsonObject.Cars;
            //option 2
            cars = JsonConvertObject.ToObjectData<Car>(myJsonString);

            Console.Out.WriteLine("myJsonObject datata {0}", ObjectDumper.Dump(cars));


            var producerConfig = new ProducerConfig
            {
                BootstrapServers = kafkaSettings.BootstrapServers
            };


            //Create the Producer
            using (var producer = new ProducerBuilder<string, string>(producerConfig).Build())
            {
                try
                {
                    foreach (Car car in cars)
                    {
                        var key = "car";

                        var val = JObject.FromObject(car).ToString(Formatting.None);

                        producedMessages.Add(val);

                        Console.WriteLine($"Producing record: {key} {val}");

                        producer.Produce(kafkaSettings.Topic, new Message<string, string> { Key = key, Value = val },
                            (deliveryReport) =>
                            {
                                if (deliveryReport.Error.Code != ErrorCode.NoError)
                                {
                                    Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                                }
                                else
                                {
                                    Console.WriteLine($"Produced message to TopicPartitionOffset : {deliveryReport.TopicPartitionOffset}");

                                }
                            });
                    }
                    producer.Flush(TimeSpan.FromSeconds(5));
                }
                catch (Exception prodError)
                {

                    Console.Out.WriteLine("Produce error {0}", prodError);
                }
                
            }

        }
       
        
        


        [Then(@"I should receive a list of cars messages")]
        public void ThenIShouldReceiveAListOfCarsMessages()
        {
            
            List<string> consumedMessages = new List<string>();

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = kafkaSettings.BootstrapServers,
                GroupId = kafkaSettings.GroupId,
                AutoOffsetReset = kafkaSettings.AutoOffsetReset,
                EnableAutoCommit = kafkaSettings.EnableAutoCommit,
                AutoCommitIntervalMs = kafkaSettings.AutoCommitIntervalMs,
                SessionTimeoutMs = kafkaSettings.SessionTimeoutMs,

            };

            CancellationTokenSource cancToken = new CancellationTokenSource();
            
            cancToken.CancelAfter((int)consumerConfig.SessionTimeoutMs);

            //Create the Consumer
            using (var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build())
            {
                try
                {
                    //Subscribe to the Kafka topic
                    consumer.Subscribe(new List<string>() { kafkaSettings.Topic });


                    while (!cancToken.IsCancellationRequested)
                    {
                        if (consumer != null)
                        {
                            try
                            {
                                var result = consumer.Consume(cancToken.Token);

                                //using Car object
                                carsReceived.Add(JsonConvertObject.ToSingleObjectData<Car>(result.Message.Value));

                                //using List
                                consumedMessages.Add(result.Message.Value);
                                

                                Console.Out.WriteLine("consumedMessages Data {0}", ObjectDumper.Dump(consumedMessages));

                                //check for duplicate list
                                receivedMessages = consumedMessages.Distinct().ToList();
                            }
                            catch (OperationCanceledException consumeCanceled)
                            {
                                consumer.Unsubscribe();

                                consumer.Close();

                                Console.Out.WriteLine(" Cancellation Resquested - consumer Unsubscribe - consumer Close ");
                            }
                            catch (ConsumeException consumeError)
                            {
                                Console.Out.WriteLine(" Consume error {0}", consumeError);
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    Console.Out.WriteLine("Subscribe error {0}", ex);
                }
            }

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
