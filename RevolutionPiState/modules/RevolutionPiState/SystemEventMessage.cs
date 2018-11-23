using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Azure.Devices.Client;

namespace EdgeLedModule
{
    public class SystemEventMessage
    {
        public DateTime timeStamp { get; set; } = DateTime.UtcNow;
        public string systemEvent { get; set; }
        public Dictionary<string, string> parameters { get; set; } = new Dictionary<string, string>();

        public SystemEventMessage(string name)
        {
            systemEvent = name;
        }

        public Message GetMessage()
        {
            var json = JsonConvert.SerializeObject(this);
            var message = new Message(Encoding.UTF8.GetBytes(json));
            return message;
        }
    }
}
