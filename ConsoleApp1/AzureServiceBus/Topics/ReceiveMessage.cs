using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

namespace ConsoleApp1.AzureServiceBus.Topics;
public class ReceiveMessage
{
    static async Task ReceivingMessage()
    {
        string sbConnStr = "";
        string topicName = "";
        string subscriberName = "";
        ServiceBusClient sbClient = new ServiceBusClient(sbConnStr);
        ServiceBusReceiver sbReceiver = sbClient.CreateReceiver(topicName, subscriberName);
        ServiceBusReceivedMessage sbReceivedMsg = await sbReceiver.ReceiveMessageAsync();
        Console.WriteLine(sbReceivedMsg.Body);
    }
}
