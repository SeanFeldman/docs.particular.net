﻿using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Gateway;

class Program
{
    static async Task Main()
    {
        Console.Title = "Samples.Gateway.RemoteSite";
        var endpointConfiguration = new EndpointConfiguration("Samples.Gateway.RemoteSite");
        endpointConfiguration.EnableInstallers();
        endpointConfiguration.UseTransport(new LearningTransport());

        #region RemoteSiteGatewayConfig
        var gatewayConfig = endpointConfiguration.Gateway(new NonDurableDeduplicationConfiguration());
        gatewayConfig.AddReceiveChannel("http://localhost:25899/RemoteSite/");
        #endregion

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        Console.WriteLine("\r\nPress any key to stop program\r\n");
        Console.ReadKey();
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}