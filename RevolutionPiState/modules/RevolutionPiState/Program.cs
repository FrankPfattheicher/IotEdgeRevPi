namespace EdgeLedModule
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.Loader;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Azure.Devices.Client;

    class Program
    {
        static int counter;
        static ModuleClient ioTHubModuleClient;

        public static int Main() => MainAsync().Result;

        static async Task<int> MainAsync()
        {
            Console.WriteLine("Starting IoT-Edge system LED module for RevolutionPi");
            await Init();

Console.WriteLine("[ 1 ]");

            // Wait until the app unloads or is cancelled
            var cts = new CancellationTokenSource();
            var ctrlC = false;
            AssemblyLoadContext.Default.Unloading += (ctx) => 
            {
                Console.WriteLine("Assembly unloading");
                cts.Cancel();
            };
Console.WriteLine("[ 2 ]");
            Console.CancelKeyPress += (sender, cpe) => 
            {
                Console.WriteLine("Cancel key pressed");
                cts.Cancel();
                ctrlC = true;
            };

            var startup = new SystemEventMessage("startup");
            await ioTHubModuleClient.SendEventAsync("output1", startup.GetMessage());

Console.WriteLine("[ 3 ]");
            while(!ctrlC)
            {
Console.WriteLine("[ 4 ]");
                await Task.Delay(TimeSpan.FromSeconds(10), cts.Token).ConfigureAwait(false);
            }

Console.WriteLine("[ 5 ]");

            var terminate = new SystemEventMessage("terminate");
            await ioTHubModuleClient.SendEventAsync("output1", terminate.GetMessage());

Console.WriteLine("[ 6 ]");
            Console.WriteLine("");
            Console.WriteLine("Terminating...");
            return 0;
        }

        /// <summary>
        /// Initializes the ModuleClient and sets up the callback to receive
        /// messages containing temperature information
        /// </summary>
        static async Task Init()
        {
            AmqpTransportSettings amqpSetting = new AmqpTransportSettings(TransportType.Amqp_Tcp_Only);
            ITransportSettings[] settings = { amqpSetting };

            Console.WriteLine("Open a connection to the Edge runtime");
            ioTHubModuleClient = await ModuleClient.CreateFromEnvironmentAsync(settings);

            ioTHubModuleClient.SetConnectionStatusChangesHandler(ConnectionStatusChanged);

            await ioTHubModuleClient.OpenAsync();
            Console.WriteLine("IoT Hub module client initialized.");


        }

        public static async void ConnectionStatusChanged(ConnectionStatus status, ConnectionStatusChangeReason reason)
        {
            Console.WriteLine($"ConnectionStatusChanged: {status}, reason={reason}");

            if(status == ConnectionStatus.Connected)
            {
                // Register callback to be called when a message is received by the module
                //await ioTHubModuleClient.SetInputMessageHandlerAsync("input1", PipeMessage, ioTHubModuleClient);

                // Register callback to be called when method is called
                await ioTHubModuleClient.SetMethodDefaultHandlerAsync(DefaultMethodCallback, ioTHubModuleClient);
                Console.WriteLine("Default method handler registered.");
                await ioTHubModuleClient.SetMethodHandlerAsync("setleds", SetLedsCallback, ioTHubModuleClient);
                Console.WriteLine("Method handler 'setleds' registered.");
            }

            if(status != ConnectionStatus.Connected && status != ConnectionStatus.Disconnected_Retrying)
            {
                Console.WriteLine("Reconnect");
                await ioTHubModuleClient.OpenAsync();
            }
        }

        public static Task<MethodResponse> DefaultMethodCallback(MethodRequest request, object userContext)
        {
            var moduleClient = userContext as ModuleClient;
            if (moduleClient == null)
            {
                throw new InvalidOperationException("UserContext doesn't contain " + "expected values");
            }

            Console.WriteLine($"Called DEFAULT method handler : {request.Name}, Data: [{request.DataAsJson}]");

            return Task.FromResult(new MethodResponse(200));
        }

        static Task<MethodResponse> SetLedsCallback(MethodRequest request, object userContext)
        {
            var moduleClient = userContext as ModuleClient;
            if (moduleClient == null)
            {
                throw new InvalidOperationException("UserContext doesn't contain " + "expected values");
            }

            Console.WriteLine($"Called method : {request.Name}, Data: [{request.DataAsJson}]");

            return Task.FromResult(new MethodResponse(200));
        }


        /// <summary>
        /// This method is called whenever the module is sent a message from the EdgeHub. 
        /// It just pipe the messages without any change.
        /// It prints all the incoming messages.
        /// </summary>
        static async Task<MessageResponse> PipeMessage(Message message, object userContext)
        {
            int counterValue = Interlocked.Increment(ref counter);

            var moduleClient = userContext as ModuleClient;
            if (moduleClient == null)
            {
                throw new InvalidOperationException("UserContext doesn't contain " + "expected values");
            }

            byte[] messageBytes = message.GetBytes();
            string messageString = Encoding.UTF8.GetString(messageBytes);
            Console.WriteLine($"Received message: {counterValue}, Body: [{messageString}]");

            if (!string.IsNullOrEmpty(messageString))
            {
                var pipeMessage = new Message(messageBytes);
                foreach (var prop in message.Properties)
                {
                    pipeMessage.Properties.Add(prop.Key, prop.Value);
                }
                await moduleClient.SendEventAsync("output1", pipeMessage);
                Console.WriteLine("Received message sent");
            }
            return MessageResponse.Completed;
        }
    }
}
