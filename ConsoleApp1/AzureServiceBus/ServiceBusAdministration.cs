using System;
using System.Threading.Tasks;
using Azure;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace ConsoleApp1.AzureServiceBus;

public class ServiceBusAdministration
{

    public async Task Administration()
    {

        // this connection for should be namespace level conn string
        string sbconnStr = "";

        ServiceBusAdministrationClient serviceBusAdministrationClient = new ServiceBusAdministrationClient(sbconnStr);


        // creating queue from c# code
        string newQueueNameToCreate = "";
        Response<QueueProperties> queueCreationResponse = await serviceBusAdministrationClient.CreateQueueAsync(newQueueNameToCreate);

        // creating new topic from c# code
        Response<TopicProperties> topicCreationResponse = await serviceBusAdministrationClient.CreateTopicAsync(newQueueNameToCreate);

        // creating new topic from c# code
        string existingTopicName = "";         // exisiting topic inside the namespace
        string newSubscriptionName = "";    // new subscription we gonna create
        Response<SubscriptionProperties> subscriptionCreationResponse = await serviceBusAdministrationClient.CreateSubscriptionAsync(existingTopicName, newSubscriptionName);

        // filters => Rules

        // we can only create rules on user and system properties but not with body
        // These filters will be checked while message is being received at subscription. 
        // If the message passes the filters of this subcription then the message will be received else it will left and other remaining subscribers can receive the message
        // Creating new filter on exsiting subscription
        string topicName = "";         // exisiting topic inside the namespace
        string subscriptionName = "";    // exisiting subcription inside the topic

        CorrelationRuleFilter correlationRuleFilter = new CorrelationRuleFilter() { ApplicationProperties = { { "paymentstatus", "completed" } } };
        CreateRuleOptions createSubscriptionOptions = new CreateRuleOptions() { Filter = correlationRuleFilter, Name = "paymentStatusCheckFilter" };

        // this will create new filter(rule) on this exisiting subcription.
        await serviceBusAdministrationClient.CreateRuleAsync(topicName, subscriptionName, createSubscriptionOptions);




        // creating a new subcription along with corelation rule
        CreateSubscriptionOptions subscriptionOptions = new CreateSubscriptionOptions(topicName, subscriptionName);
        CorrelationRuleFilter coRelFilter = new CorrelationRuleFilter() { ApplicationProperties = { { "City", "Mumbai" } } };
        CreateRuleOptions createRuleOP = new CreateRuleOptions() { Filter = coRelFilter, Name = "CityCheckFilter" };
        await serviceBusAdministrationClient.CreateSubscriptionAsync(subscriptionOptions, createRuleOP);


        // creating a new subcription along with sqk rule (which check the message with IN and LIke operator as like SQL)
        CreateSubscriptionOptions subsOpt = new CreateSubscriptionOptions(topicName, subscriptionName);
        // here in SqlRuleFilter constructor we need to pass sql expression ex: name like '%dee%' or name in ('deepak', 'shashank') or name not in ('kishore')
        SqlRuleFilter sqlRule = new SqlRuleFilter("'name like '%mumbai%''") {};
        CreateRuleOptions sqlRUleOpt = new CreateRuleOptions() { Filter = coRelFilter, Name = "CityCheckFilter" };
        await serviceBusAdministrationClient.CreateSubscriptionAsync(subsOpt, sqlRUleOpt);
        

        // creating a new subcription along with boolean rule
        CreateSubscriptionOptions subsOpts = new CreateSubscriptionOptions(topicName, subscriptionName);
        SqlRuleFilter trueFilter = new TrueRuleFilter(){ };
        CreateRuleOptions trueBoolRuleOpt = new CreateRuleOptions() { Filter = coRelFilter, Name = "isactivelistener" };
        await serviceBusAdministrationClient.CreateSubscriptionAsync(subsOpts, trueBoolRuleOpt);

    }
}
