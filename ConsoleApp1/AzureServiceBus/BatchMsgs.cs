using Azure.Messaging.ServiceBus;
namespace ConsoleApp1.AzureServiceBus;
public class BatchMsgs
{
    public static async Task SendMessagesInBatches()
    {
        string message = "service bus queue sample message";
        string sbConnString = "replace this with real service bus conn str";
        string queueName = "queue1";
        ServiceBusClient sbClient = new ServiceBusClient(sbConnString);
        ServiceBusSender sbSender = sbClient.CreateSender(queueName);
        ServiceBusMessageBatch sbMsgBatch  = await sbSender.CreateMessageBatchAsync();
        for (int i = 0; i < 10; i++) {
            if (! sbMsgBatch.TryAddMessage(new ServiceBusMessage(message+$" {i}")))
            {
                break;
            }
        }
        await sbSender.SendMessagesAsync(sbMsgBatch);
    }
}
