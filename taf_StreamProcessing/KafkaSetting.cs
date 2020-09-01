using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;

namespace taf_StreamProcessing
{
    class KafkaSetting
    {
      
        public string GroupId = "cars-group";
        public string BootstrapServers = "127.0.0.1:9092";
        public int AutoCommitIntervalMs = 5000;
        public AutoOffsetReset AutoOffsetReset = AutoOffsetReset.Earliest;
        public string Topic = "cars";
        public bool EnableAutoCommit = true;
        public int SessionTimeoutMs = 6000;
    }
}
