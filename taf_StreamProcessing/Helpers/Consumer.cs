using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace taf_StreamProcessing.Helpers
{
    class Consumer
    {
        private KafkaSetting kafkaSettings = new KafkaSetting();

        List<string> consumedMessages = new List<string>();

        public (List<string>, List<T>) CreateConsumer<T>()
        {

            List<T> objReceived = new List<T>();

            List<string> receivedMessages = new List<string>();

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
                                objReceived.Add(JsonConvertObject.ToSingleObjectData<T>(result.Message.Value));

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

            return (consumedMessages, objReceived);
        }
    }
}
