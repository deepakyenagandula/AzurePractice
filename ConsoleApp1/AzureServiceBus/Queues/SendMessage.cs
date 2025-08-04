using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;

namespace ConsoleApp1.AzureServiceBus.Queues;
public class SendMessage
{
    static async Task SendingMessage()
    {
        string sbConnString = "";
        string queueName = "";
        object payloadObj = new object();
        string message = JsonSerializer.Serialize(payloadObj);
        ServiceBusClient sbClient = new ServiceBusClient(sbConnString);
        ServiceBusSender sbSender = sbClient.CreateSender(queueName);
        ServiceBusMessage sbMessage = new ServiceBusMessage(message);
        await sbSender.SendMessageAsync(sbMessage);

        // sending the message in bytes
        byte[] messageInBytes = Encoding.UTF8.GetBytes(message);
        ServiceBusMessage sbMessageForSendingInBytes = new ServiceBusMessage(messageInBytes);
        await sbSender.SendMessageAsync(sbMessageForSendingInBytes);
    }
}
