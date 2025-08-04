using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace ConsoleApp1.AzureServiceBus.Queues;

public class ReceiveMessage
{
    static async Task ReceivingMessage()
    {
        string sbConnStr = "";
        string queueName = "";
        ServiceBusClient sbClient = new ServiceBusClient(sbConnStr);
        ServiceBusReceiver sbReceiver = sbClient.CreateReceiver(queueName);
        ServiceBusReceivedMessage sbReceivedMsg = await sbReceiver.ReceiveMessageAsync();
        Console.WriteLine(sbReceivedMsg.Body);
    }
}
