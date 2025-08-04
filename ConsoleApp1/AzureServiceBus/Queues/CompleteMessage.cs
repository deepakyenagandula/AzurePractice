using Azure.Messaging.ServiceBus;

namespace ConsoleApp1.AzureServiceBus.Queues;

public class CompleteMessage
{

    public async Task ChangeMessageStatusAndGetFromDeadLetter()
    {
        string sbQueueConnStr = "";
        string queueName = "";
        ServiceBusClient sbClient = new ServiceBusClient(sbQueueConnStr);
        ServiceBusReceiver sbReceiver = sbClient.CreateReceiver(queueName);
        ServiceBusReceivedMessage sbMessage = await sbReceiver.ReceiveMessageAsync();


        // abondon a message
        await sbReceiver.AbandonMessageAsync(sbMessage);

        // defer a message
        long deferringMsgId = sbMessage.SequenceNumber;
        await sbReceiver.DeferMessageAsync(sbMessage);

        // get deferred message
        await sbReceiver.CompleteMessageAsync(sbMessage);

        // moving a message to deadletter
        await sbReceiver.DeadLetterMessageAsync(sbMessage);

        // receiving differed message
        ServiceBusReceivedMessage sbDeferredReceiveMessage = await sbReceiver.ReceiveDeferredMessageAsync(deferringMsgId);

        // receive message from the dead letter queue (which is a subset of main queue)
        sbClient.CreateReceiver(queueName, new ServiceBusReceiverOptions() { SubQueue = SubQueue.DeadLetter });
        
    }

}
