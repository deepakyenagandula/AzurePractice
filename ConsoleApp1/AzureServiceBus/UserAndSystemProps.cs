using System;
using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;

namespace ConsoleApp1.AzureServiceBus;

public class UserAndSystemProps
{

    public void SendMessageWithUserAndSystemProperties()
    {
        
         string sbConnString = "";
        string queueName = "";
        object payloadObj = new object();
        string messageBody = JsonSerializer.Serialize(payloadObj);
        ServiceBusClient sbClient = new ServiceBusClient(sbConnString);
        ServiceBusSender sbSender = sbClient.CreateSender(queueName);
         ServiceBusMessage message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));
            // 1. Create the message body

            // 2. Add user (custom) properties
            // The Properties dictionary is of type Dictionary<string, object>
            // You can add various data types as values.
            message.ApplicationProperties["MessageType"] = "OrderConfirmation";
            message.ApplicationProperties["CustomerId"] = 12345;
            message.ApplicationProperties["Timestamp"] = DateTimeOffset.UtcNow;
            message.ApplicationProperties["IsTestMessage"] = true;
            message.ApplicationProperties["Region"] = "WestUS";
            message.ApplicationProperties["ProcessingPriority"] = 3; // Integer property

            // Example of a custom property for a software developer
            message.ApplicationProperties["DeveloperNotes"] = "Debugging info: Processed by XYZ microservice.";


            // 3. Add system properties (optional, but good to know)
            // Some properties are directly available on the ServiceBusMessage object.
            // These are often used for correlation, session management, or scheduling.
            message.ContentType = "application/json"; // Standard MIME type
            message.Subject = "New Order Processed";   // Subject field
            message.MessageId = Guid.NewGuid().ToString(); // Unique message identifier
            // message.SessionId = "session-123"; 
    }
}
