using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.Messaging.ServiceBus;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WmdaConnect.Models;
using WmdaConnect.Models.MessageBases;
using WmdaConnect.Shared;
using WmdaConnect.Shared.Converters;

namespace WmdaConnectCLI
{
    internal static class Program
    {
        private static Ack _ackMessage;
        private static string _accessToken;
        private static string _urlRoot;
        private static PingRequest _pingRequest;
        private static TextMessageRequest _textMessageRequest;
        private static TypingRequestRequest _typingRequestRequest;
        private static TypingResponseRequest _typingResponseRequest;
        private static SampleRequestRequest _sampleRequestRequest;
        private static SampleArrivalRequest _sampleArrivalRequest;
        private static SampleResponseRequest _sampleResponseRequest;
        private static ConnectOptions _connect;

        private static async Task Main(string[] args)
        {
            Console.ResetColor();

            var parser = new Parser(settings =>
            {
                settings.CaseSensitive = false;
                settings.HelpWriter = Console.Error;
            });

            _connect = MemoryConnectManager.GetValue();


            await parser
                .ParseArguments<ConnectOptions, PingOptions, ListenOptions, NewRegistryOptions, RemoveRegistryOptions, MessageOptions, DownloadOptions>(args)
                .MapResult(
                    (ConnectOptions opts) => ConnectRegistry(opts),
                    (PingOptions opts) => RunPingOptions(opts),
                    (ListenOptions opts) => RunListenOptions(opts),
                    (NewRegistryOptions opts) => RunNewRegistryOptions(opts),
                    (RemoveRegistryOptions opts) => RunRemoveRegistryOptions(opts),
                    (MessageOptions opts) => RunMessage(opts),
                    (DownloadOptions opts) => RunDownloadAttachments(opts),
                    HandleParseError);
        }

        /* ----------------------------------------------------------------------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */
        /* -----------------------------------------LISTEN FUNCTIONALITY---------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */

        public static async Task ConnectRegistry(ConnectOptions opts)
        {
            IConfigurationRoot _configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location))
                .AddJsonFile($"appsettings.{opts.Environment}.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();

            var tenantId = _configuration["tenantId"];
            var clientId = opts.ClientId;
            var clientSecret = opts.ClientSecret;
            var azureFunctionAppClientId = _configuration["azureFunctionAppClientId"];
            _urlRoot = _configuration["urlRoot"];

            try
            {
                var app = ConfidentialClientApplicationBuilder.Create(clientId)
                    .WithClientSecret(clientSecret)
                    .WithTenantId(tenantId)
                    .Build();

                var azureFunctionAppScope = $"{azureFunctionAppClientId}/.default";

                var result1 = await app.AcquireTokenForClient(new[] { azureFunctionAppScope }).ExecuteAsync();

                _accessToken = result1.AccessToken;

                MemoryConnectManager.SetValue(opts);

                if (opts.Show)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(_accessToken);
                    Console.ResetColor();
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
            finally
            {
                Console.ResetColor();
            }
            Console.WriteLine("Successfully connected!");
        }

        /* ----------------------------------------------------------------------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */
        /* -----------------------------------------LISTEN FUNCTIONALITY---------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */

        public static async Task RunListenOptions(ListenOptions opts)
        {
            IConfigurationRoot _configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location))
                .AddJsonFile($"appsettings.{opts.Environment ?? _connect.Environment}.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();

            var tenantId = _configuration["tenantId"];
            var clientId = opts.ClientId ?? _connect.ClientId;
            var clientSecret = opts.ClientSecret ?? _connect.ClientSecret;
            var fullyQualifiedNamespace = _configuration["ServiceBusNamespace"];
            var azureFunctionAppClientId = _configuration["azureFunctionAppClientId"];
            _urlRoot = _configuration["urlRoot"];

            try
            {
                var app = ConfidentialClientApplicationBuilder.Create(clientId)
                    .WithClientSecret(clientSecret)
                    .WithTenantId(tenantId)
                    //.WithLogging(new LogCallback((a, b, _) => Console.WriteLine($"{a} {b}")))
                    .Build();

                var azureFunctionAppScope = $"{azureFunctionAppClientId}/.default";

                var result1 = await app.AcquireTokenForClient(new[] { azureFunctionAppScope }).ExecuteAsync();

                _accessToken = result1.AccessToken;

                var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

                await using var client = new ServiceBusClient(fullyQualifiedNamespace, credential);

                var registryDetails = await GetRegistryDetails(_accessToken);

                var queueName = registryDetails.QueueName;

                var processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

                processor.ProcessMessageAsync += MessageHandler;

                processor.ProcessErrorAsync += ErrorHandler;

                await processor.StartProcessingAsync();

                Console.WriteLine("Listening...Press any key to stop listening");

                Console.ReadKey();
                Console.WriteLine("Stopping the receiver...");

                await processor.StopProcessingAsync();

                Console.WriteLine("Stopped receiving messages");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
        }

        /* ----------------------------------------------------------------------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */
        /* -------------------------------------------PING FUNCTIONALITY---------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */

        public static async Task RunPingOptions(PingOptions opts)
        {
            IConfigurationRoot _configuration;

            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location))
                .AddJsonFile($"appsettings.{opts.Environment ?? _connect.Environment}.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();

            var tenantId = _configuration["tenantId"];
            var clientId = opts.ClientId ?? _connect.ClientId;
            var clientSecret = opts.ClientSecret ?? _connect.ClientSecret;

            var fullyQualifiedNamespace = _configuration["ServiceBusNamespace"];
            var azureFunctionAppClientId = _configuration["azureFunctionAppClientId"];
            _urlRoot = _configuration["urlRoot"];

            try
            {
                var app = ConfidentialClientApplicationBuilder.Create(clientId)
                    .WithClientSecret(clientSecret)
                    .WithTenantId(tenantId)
                  //  .WithLogging(new LogCallback((a, b, _) => Console.WriteLine($"{a} {b}")))
                    .Build();

                var azureFunctionAppScope = $"{azureFunctionAppClientId}/.default";

                var authenticationResult = await app.AcquireTokenForClient(new[] { azureFunctionAppScope }).ExecuteAsync();

                _accessToken = authenticationResult.AccessToken;

                Console.WriteLine(_accessToken);

                TokenCredential credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

                await using var client = new ServiceBusClient(fullyQualifiedNamespace, credential);

                var registryDetails = await GetRegistryDetails(authenticationResult.AccessToken);

                var queueName = registryDetails.QueueName;

                var processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

                processor.ProcessMessageAsync += MessageHandler;

                processor.ProcessErrorAsync += ErrorHandler;

                await processor.StartProcessingAsync();

                var targetRegistryName = opts.TargetRegistry;

                _pingRequest = new PingRequest { Recipient = targetRegistryName };
                var pingRequestJson = JsonConvert.SerializeObject(_pingRequest, Formatting.Indented, new StringEnumConverter());
                var httpContent = new StringContent(pingRequestJson, Encoding.UTF8, "application/json");

                var url = $"{_urlRoot}/PingRequest";

                var req = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = httpContent
                };

                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authenticationResult.AccessToken);

                using var httpClient = new HttpClient();

                var response = await httpClient.SendAsync(req);

                if (response.IsSuccessStatusCode)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Write($"Message sent to {url}\n{pingRequestJson}");
                }
                else
                {
                    var result = response.Content.ReadAsStringAsync().Result + $": {response}";
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write($"ERROR: [from {url}]\n{result}");
                }
                Console.ResetColor();
                Console.WriteLine();

                var stopwatch = Stopwatch.StartNew();

                while (_ackMessage == null && stopwatch.ElapsedMilliseconds < 10_000)
                {
                    await Task.Delay(500);
                }

                Console.WriteLine("Stopping the receiver...");

                await processor.StopProcessingAsync();

                Console.WriteLine("Stopped receiving messages");

                if (_ackMessage == null)
                {
                    throw new TimeoutException("Acknowledgement not received.");
                }
            }

            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e);
                Console.ResetColor();
            }
            finally
            {
                Console.ResetColor();
            }
        }

        private static async Task<RegistryDetailsResponseModel> GetRegistryDetails(string accessToken)
        {
            var url = $"{_urlRoot}/RegistryDetails";

            var req = new HttpRequestMessage(HttpMethod.Get, url)
            {
            };

            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            using var httpClient = new HttpClient();

            var responseRegistry = await httpClient.SendAsync(req);

            responseRegistry.EnsureSuccessCodeReportBody();

            var responseBody = await responseRegistry.Content.ReadAsStringAsync();

            var registry = JsonConvert.DeserializeObject<RegistryDetailsResponseModel>(responseBody);

            return registry;
        }

       

        /* ----------------------------------------------------------------------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */
        /* -----------------------------------------NEW REGISTRY FUNCTIONALITY---------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */

        public static async Task RunNewRegistryOptions(NewRegistryOptions opts)
        {
            IConfigurationRoot _configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location))
                .AddJsonFile($"appsettings.{opts.Environment ?? _connect.Environment}.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();

            var tenantId = _configuration["tenantId"];
            var clientId = opts.ClientId ?? _connect.ClientId;
            var clientSecret = opts.ClientSecret ?? _connect.ClientSecret;

            var useAzApiEndpoint = opts.AzApiEndpoint;
            var azureFunctionAppClientId = _configuration["mdmApiClientId"];
            _urlRoot = _configuration["mdmApiUrlRoot"];
            var azApiUrl = _configuration["azApiUrl"];

            try
            {
                var app = ConfidentialClientApplicationBuilder.Create(clientId)
                    .WithClientSecret(clientSecret)
                    .WithTenantId(tenantId)
                    //.WithLogging(new LogCallback((a, b, _) => Console.WriteLine($"{a} {b}")))
                    .Build();

                var azureFunctionAppScope = $"{azureFunctionAppClientId}/.default";

                var result1 = await app.AcquireTokenForClient(new[] { azureFunctionAppScope }).ExecuteAsync();

                _accessToken = result1.AccessToken;

                var url = "";

                url = useAzApiEndpoint == "true"
                    ? $"{azApiUrl}/new-registry?queueName={opts.RegistryName}&appName=TestDelete-{opts.RegistryName}-{new Random().Next(1000, 10000)}"
                    : $"{_urlRoot}/New-Registry?ion={opts.RegistryName}";


                var req = new HttpRequestMessage(HttpMethod.Post, url)
                {
                };

                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

                using var httpClient = new HttpClient();

                var responseRegistry = await httpClient.SendAsync(req);

                responseRegistry.EnsureSuccessCodeReportBody();

                var responseBody = responseRegistry.Content.ReadAsStringAsync().Result;

                Console.WriteLine(responseBody);
            }
            catch (Exception e)
            {
                await Console.OpenStandardOutput().WriteAsync(Encoding.UTF8.GetBytes(e.Message));
            }

        }



        /* ----------------------------------------------------------------------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */
        /* -----------------------------------------REMOVE REGISTRY FUNCTIONALITY------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */

        public static async Task RunRemoveRegistryOptions(RemoveRegistryOptions opts)
        {
            IConfigurationRoot _configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location))
                .AddJsonFile($"appsettings.{opts.Environment ?? _connect.Environment}.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();

            var tenantId = _configuration["tenantId"];
            var clientId = opts.ClientId ?? _connect.ClientId;
            var clientSecret = opts.ClientSecret ?? _connect.ClientSecret;
            var azureFunctionAppClientId = _configuration["mdmApiClientId"];
            _urlRoot = _configuration["mdmApiUrlRoot"];

            try
            {
                var app = ConfidentialClientApplicationBuilder.Create(clientId)
                    .WithClientSecret(clientSecret)
                    .WithTenantId(tenantId)
                    //.WithLogging(new LogCallback((a, b, _) => Console.WriteLine($"{a} {b}")))
                    .Build();

                var azureFunctionAppScope = $"{azureFunctionAppClientId}/.default";

                var result1 = await app.AcquireTokenForClient(new[] { azureFunctionAppScope }).ExecuteAsync();

                _accessToken = result1.AccessToken;

                var url = $"{_urlRoot}/Remove-Registry?ion={opts.RegistryName}";

                var req = new HttpRequestMessage(HttpMethod.Post, url)
                {
                };

                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

                using var httpClient = new HttpClient();

                var responseRegistry = await httpClient.SendAsync(req);

                responseRegistry.EnsureSuccessCodeReportBody();

                var responseBody = responseRegistry.Content.ReadAsStringAsync().Result;

                Console.WriteLine(responseBody);
            }
            catch (Exception e)
            {
                await Console.OpenStandardOutput().WriteAsync(Encoding.UTF8.GetBytes(e.Message));
            }

        }

        /* ----------------------------------------------------------------------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */
        /* -----------------------------------------DOWNLOAD ATTACHMENT FUNCTIONALITY--------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */

        public static async Task RunDownloadAttachments(DownloadOptions opts)
        {
            IConfigurationRoot _configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location))
                .AddJsonFile($"appsettings.{opts.Environment ?? _connect.Environment}.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();

            var tenantId = _configuration["tenantId"];
            var clientId = opts.ClientId ?? _connect.ClientId;
            var clientSecret = opts.ClientSecret ?? _connect.ClientSecret;
            var azureFunctionAppClientId = _configuration["mdmApiClientId"];
            _urlRoot = _configuration["mdmApiUrlRoot"];

            await DownloadAttachment(opts.AttachmentGuid);

        }

        /* ----------------------------------------------------------------------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */
        /* -----------------------------------------SEND MESSAGE FUNCTIONALITY----------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */

        public static async Task RunMessage(MessageOptions opts)
        {
            IConfigurationRoot _configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location))
                .AddJsonFile($"appsettings.{opts.Environment ?? _connect.Environment}.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();


            var tenantId = _configuration["tenantId"];
            var clientId = opts.ClientId ?? _connect.ClientId;
            var clientSecret = opts.ClientSecret ?? _connect.ClientSecret;
            var fullyQualifiedNamespace = _configuration["ServiceBusNamespace"];
            var azureFunctionAppClientId = _configuration["azureFunctionAppClientId"];
            _urlRoot = _configuration["urlRoot"];


            var messageType = opts.MessageType;
            var fileName = opts.File;
            var attachment = opts.Attachment;
            try
            {

                var app = ConfidentialClientApplicationBuilder.Create(clientId)
                    .WithClientSecret(clientSecret)
                    .WithTenantId(tenantId)
                    .Build();

                var azureFunctionAppScope = $"{azureFunctionAppClientId}/.default";

                var result1 = await app.AcquireTokenForClient(new[] { azureFunctionAppScope }).ExecuteAsync();

                _accessToken = result1.AccessToken;

                TokenCredential credential = new ClientSecretCredential(tenantId, clientId, clientSecret);


                await using var client = new ServiceBusClient(fullyQualifiedNamespace, credential);

                var registryDetails = await GetRegistryDetails(result1.AccessToken);

                var queueName = registryDetails.QueueName;

                var processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

                processor.ProcessMessageAsync += MessageHandler;

                processor.ProcessErrorAsync += ErrorHandler;

                await processor.StartProcessingAsync();

                var messageContent = await File.ReadAllTextAsync(fileName);
                Console.WriteLine(messageContent);

                string contentToSend = null;

                var url = "";

                switch (messageType)
                {
                    case MessageTypes.TextMessage:
                    {
                        //Need to get download URL
                        var attachmentTicket = await GetAttachmentTicket(_accessToken, opts.Attachment);

                        UploadFileToAzureBlobStorage(opts.Attachment, attachmentTicket);
                        
                        _textMessageRequest = JsonConvert.DeserializeObject<TextMessageRequest>(messageContent);

                    
                        _textMessageRequest.Payload.AttachmentGuids.Add(attachmentTicket.AttachmentGuid);
                        if (opts.TargetRegistry is not null)
                            _textMessageRequest.Recipient = opts.TargetRegistry;

                        contentToSend = JsonConvert.SerializeObject(_textMessageRequest);

                        url = $"{_urlRoot}/TextMessageRequest";
                        break;
                    }
                    case MessageTypes.TypingRequest:
                    {
                        _typingRequestRequest = JsonConvert.DeserializeObject<TypingRequestRequest>(messageContent, new MultiFormatDateConverter());
                    

                        if (opts.TargetRegistry is not null)
                            _typingRequestRequest.Recipient = opts.TargetRegistry;

                        contentToSend = JsonConvert.SerializeObject(_typingRequestRequest);

                        url = $"{_urlRoot}/TypingRequestRequest";
                        break;
                    }
                    case MessageTypes.TypingResponse:
                    {
                        _typingResponseRequest = JsonConvert.DeserializeObject<TypingResponseRequest>(messageContent);

                        if (opts.TargetRegistry is not null)
                            _typingResponseRequest.Recipient = opts.TargetRegistry;

                        contentToSend = JsonConvert.SerializeObject(_typingResponseRequest);

                        url = $"{_urlRoot}/TypingResponseRequest";
                        break;
                    }
                    case MessageTypes.SampleRequest:
                    {
                        _sampleRequestRequest = JsonConvert.DeserializeObject<SampleRequestRequest>(messageContent);

                        if (opts.TargetRegistry is not null)
                            _sampleRequestRequest.Recipient = opts.TargetRegistry;

                        contentToSend = JsonConvert.SerializeObject(_sampleRequestRequest);


                        url = $"{_urlRoot}/SampleRequestRequest";
                        break;
                    }
                    case MessageTypes.SampleArrival:
                    {
                        _sampleArrivalRequest = JsonConvert.DeserializeObject<SampleArrivalRequest>(messageContent);
                    
                        if (opts.TargetRegistry is not null)
                            _sampleArrivalRequest.Recipient = opts.TargetRegistry;

                        contentToSend = JsonConvert.SerializeObject(_sampleArrivalRequest);

                        url = $"{_urlRoot}/SampleArrivalRequest";
                        break;
                    }
                    case MessageTypes.SampleInfo:
                    {
                        var sampleInfoRequest = JsonConvert.DeserializeObject<SampleInfoRequest>(messageContent);

                        if (opts.TargetRegistry is not null)
                            sampleInfoRequest.Recipient = opts.TargetRegistry;

                        contentToSend = JsonConvert.SerializeObject(sampleInfoRequest);

                        url = $"{_urlRoot}/SampleInfoRequest";
                        break;
                    }
                    case MessageTypes.SampleResponse:
                    {
                        _sampleResponseRequest = JsonConvert.DeserializeObject<SampleResponseRequest>(messageContent);
                    
                        if (opts.TargetRegistry is not null)
                            _sampleResponseRequest.Recipient = opts.TargetRegistry;

                        contentToSend = JsonConvert.SerializeObject(_sampleResponseRequest);

                        url = $"{_urlRoot}/SampleResponseRequest";
                        break;
                    }
                    case MessageTypes.Ping:
                    case MessageTypes.Ack:
                    default:
                        throw new ArgumentOutOfRangeException(messageType.ToString());
                }

                var httpContent = new StringContent(contentToSend, Encoding.UTF8, "application/json");

                var req = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = httpContent
                };


                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result1.AccessToken);

                using var httpClient = new HttpClient();

                var response = await httpClient.SendAsync(req);

                if (response.IsSuccessStatusCode)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Write($"Message sent.\n{httpContent.ReadAsStringAsync().Result}");
                }
                else
                {
                    var result = response.Content.ReadAsStringAsync().Result + $": {response}";

                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("ERROR: " + result);
                }
                Console.ResetColor();
                Console.WriteLine();

                var stopwatch = new Stopwatch();

                while (_ackMessage == null)
                {

                    stopwatch.Start();

                    if (stopwatch.ElapsedMilliseconds >= 10000)
                    {
                        stopwatch.Stop();
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new TimeoutException("Acknowledgement not received.");

                    }
                }

                Console.WriteLine("Stopping the receiver...");

                await processor.StopProcessingAsync();

                Console.WriteLine("Stopped receiving messages");
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.ResetColor();
            }

        }


        private static async Task<AttachmentTicketResponse> GetAttachmentTicket(string accessToken, string fileName)
        {

            var url = $"{_urlRoot}/AttachmentTicket";


            var attachmentTicketRequest = new AttachmentTicketRequest()
            {
                FileName = Path.GetFileName(fileName)
            };

            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var responseAttachmentTicket = await httpClient.PostAsJsonAsync(url, attachmentTicketRequest);

            responseAttachmentTicket.EnsureSuccessCodeReportBody();

            var responseBody = await responseAttachmentTicket.Content.ReadAsStringAsync();

            var attachmentTicket = JsonConvert.DeserializeObject<AttachmentTicketResponse>(responseBody);

            return attachmentTicket;
        }

        private static async Task<AttachmentDownloadResponse> GetAttachmentDownload(Guid attachmentGuid, string accessToken)
        {

            var url = $"{_urlRoot}/AttachmentDownloadURL";


            var attachmentTicketRequest = new AttachmentDownloadRequest()
            {
                AttachmentGuid = attachmentGuid
            };

            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var responseAttachmentTicket = await httpClient.PostAsJsonAsync(url, attachmentTicketRequest);

            responseAttachmentTicket.EnsureSuccessCodeReportBody();

            var responseBody = await responseAttachmentTicket.Content.ReadAsStringAsync();

            var attachmentTicket = JsonConvert.DeserializeObject<AttachmentDownloadResponse>(responseBody);

            return attachmentTicket;
        }

        static void UploadFileToAzureBlobStorage(string attachmentPath, AttachmentTicketResponse attachmentTicket)
        {

            var uploadFileStream = File.ReadAllBytes(attachmentPath);

            string method = "PUT";
            string sampleContent = attachmentPath;
            int contentLength = uploadFileStream.Length;

            //string requestUri = $"https://{storageAccount}.blob.core.windows.net/{containerName}/{blobName}?{sasToken}";
            string requestUri = attachmentTicket.AttachmentUploadUrl;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = method;
            request.ContentType = "application/octet-stream";
            request.ContentLength = contentLength;
            request.Headers.Add("x-ms-blob-type", "BlockBlob");

            
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(uploadFileStream, 0, uploadFileStream.Length);
            }

            using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
            {
                if (resp.StatusCode == HttpStatusCode.Created)
                { 
                    Console.WriteLine($"Blob uploaded: {contentLength:N0} bytes");
                }
                else
                {
                    throw new Exception(resp.StatusCode.ToString());
                }
            }
        }

    

        /* ----------------------------------------------------------------------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */
        /* -------------------------------------------- HANDLING ERRORS ---------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */
        /* ----------------------------------------------------------------------------------------------------------- */


        public static Task HandleParseError(IEnumerable<Error> errs)
        {
            foreach (var i in errs)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(i);
            }
            Console.ResetColor();

            return Task.CompletedTask;
        }

        private static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            if (!args.Message.ApplicationProperties.TryGetValue("MessageType", out var messageTypeString))
            {
                Console.WriteLine("NO MESSAGE TYPE");
                return;
            }

            if (!Enum.TryParse<MessageTypes>(messageTypeString?.ToString(), out var messageType))
            {
                Console.WriteLine($"FAILED TO PARSE MESSAGE TYPE {messageTypeString}");
                return;
            }

            var body = args.Message.Body.ToString();


            switch (messageType)
            {
                case MessageTypes.Ack:
                    {
                        await args.CompleteMessageAsync(args.Message);

                        var ackMessage = JsonConvert.DeserializeObject<Ack>(body);

                        if (ackMessage.CorrelationGuid != _pingRequest?.CorrelationGuid && ackMessage.CorrelationGuid != _textMessageRequest?.CorrelationGuid
                        && ackMessage.CorrelationGuid != _typingRequestRequest?.CorrelationGuid && ackMessage.CorrelationGuid != _typingResponseRequest?.CorrelationGuid
                        && ackMessage.CorrelationGuid != _sampleRequestRequest?.CorrelationGuid && ackMessage.CorrelationGuid != _sampleArrivalRequest?.CorrelationGuid
                        && ackMessage.CorrelationGuid != _sampleResponseRequest?.CorrelationGuid)
                        {

                            Console.WriteLine(_pingRequest?.CorrelationGuid.ToString() ?? _textMessageRequest?.CorrelationGuid.ToString() ?? "NullCorrelationGuid");
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.Write("Stale Message: " + ackMessage.CorrelationGuid);
                            Console.ResetColor();
                            Console.WriteLine();

                            break;

                        }

                        _ackMessage = ackMessage;
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.Write("Message Acknowledged: " + ackMessage.CorrelationGuid);
                        Console.ResetColor();
                        Console.WriteLine();
                        break;
                    }

                default:

                    try
                    {
                        await args.CompleteMessageAsync(args.Message);

                        body = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(body), Formatting.Indented);

                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write($"Received:\n{body}");
                        Console.ResetColor();
                        Console.WriteLine();

                        var pingMessage = JsonConvert.DeserializeObject<Ping>(body); //May need a refactor for using Message class type
                        var textMessage = JsonConvert.DeserializeObject<TextMessage>(body);

                        if (textMessage.Payload?.AttachmentGuids != null)
                        {
                            foreach (var attachmentGuid in textMessage.Payload.AttachmentGuids)
                            {
                                await DownloadAttachment(attachmentGuid);
                            }
                        }
                        var url = $"{_urlRoot}/AckRequest";

                        var req = new HttpRequestMessage(HttpMethod.Post, url);

                        var ackRequestModel = new AckRequest() { Recipient = pingMessage.Sender, CorrelationGuid = pingMessage.CorrelationGuid };
                        var myContent = JsonConvert.SerializeObject(ackRequestModel);
                        var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

                        req.Content = httpContent;

                        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

                        var httpClient = new HttpClient();

                        var response = await httpClient.SendAsync(req);

                        response.EnsureSuccessCodeReportBody();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    break;
            }
        }

        private static async Task DownloadAttachment(Guid attachmentGuid)
        {
            
            var downloadUrl = await GetAttachmentDownload(attachmentGuid, _accessToken);
            string downloadLocation = $@"{attachmentGuid}_{downloadUrl.FileName}";
            Console.WriteLine(downloadUrl.DownloadUrl);
            try
            {

                using (var client = new WebClient())
                {
                   await client.DownloadFileTaskAsync(downloadUrl.DownloadUrl, $"{Path.Combine(Path.GetTempPath(), downloadLocation)}");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Downloaded {downloadLocation} to {Path.GetTempPath()}");
                Console.ResetColor();
            }
            catch (RequestFailedException e)
            {

                if (e.Status == 403)
                {
                    Console.WriteLine("Read operation failed for SAS {0}", downloadUrl);
                    Console.WriteLine("Additional error information: " + e.Message);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                    throw;
                }
            }
        }


        // handle any errors when receiving messages
        private static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(args.Exception.ToString());
            Console.ResetColor();
            return Task.CompletedTask;
        }

        private sealed class MemoryConnectManager
        {
            private static readonly string ConfigFilePath; 

            static MemoryConnectManager()
            {
                var envHome = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
                if (!Directory.Exists(envHome + @"/.wm"))
                    Directory.CreateDirectory(envHome + @"/.wm");
                
                ConfigFilePath =  $@"{envHome}/.wm/connectInfo.txt";
            }

            public static void SetValue(ConnectOptions value)
            {
                var jsonString = System.Text.Json.JsonSerializer.Serialize(value);
                File.WriteAllText(ConfigFilePath, jsonString);
            }

            public static ConnectOptions GetValue()
            {
                if (!File.Exists(ConfigFilePath)) return null;

                var jsonString = File.ReadAllText(ConfigFilePath);
                
                return System.Text.Json.JsonSerializer.Deserialize<ConnectOptions>(jsonString);
            }
        }
    }

    [Verb("connect", HelpText = "Connect to a registry.")]
    internal class ConnectOptions
    {
        [Option('c', "clientId", Required = true, HelpText = "Your Client ID")]
        public string ClientId { get; set; }

        [Option('s', "clientSecret", Required = true, HelpText = "Your Client Secret")]
        public string ClientSecret { get; set; }

        [Option('e', "env", Required = true, HelpText = "Environment")]
        public string Environment { get; set; }

        [Option('w', "show", Required = false, HelpText = "Prints token to console")]
        public bool Show { get; set; }
    }

    [Verb("ping", HelpText = "Ping another registry")]
    internal class PingOptions
    {
        [Option('c', "clientId", Required = false, HelpText = "Your Client ID")]
        public string ClientId { get; set; }

        [Option('s', "clientSecret", Required = false, HelpText = "Your Client Secret")]
        public string ClientSecret { get; set; }

        [Option('t', "targetRegistry", Required = true, HelpText = "Target Registry")]
        public string TargetRegistry { get; set; }

        [Option('e', "env", Required = false, HelpText = "Environment")]
        public string Environment { get; set; }

    }

    [Verb("listen", HelpText = "Listen for any incoming pings")]
    internal class ListenOptions
    {
        [Option('c', "clientId", Required = false, HelpText = "Your Client ID")]
        public string ClientId { get; set; }

        [Option('s', "clientSecret", Required = false, HelpText = "Your Client Secret")]
        public string ClientSecret { get; set; }

        [Option('e', "env", Required = false, HelpText = "Environment")]
        public string Environment { get; set; }
    }

    [Verb("newregistry", HelpText = "Create new Registry")]
    internal class NewRegistryOptions
    {
        [Option('c', "clientId", Required = false, HelpText = "Your Client ID")]
        public string ClientId { get; set; }

        [Option('s', "clientSecret", Required = false, HelpText = "Your Client Secret")]
        public string ClientSecret { get; set; }

        [Option('e', "env", Required = false, HelpText = "Environment")]
        public string Environment { get; set; }

        [Option('n', "Name", Required = true, HelpText = "Registry Name")]
        public string RegistryName { get; set; }

        [Option('a', "useAzApi", Required = false, HelpText = "Can be set to true for testing purposes only")]
        public string AzApiEndpoint { get; set; }
    }

    [Verb("removeregistry", HelpText = "Remove/Delete Registry")]
    internal class RemoveRegistryOptions
    {
        [Option('c', "clientId", Required = false, HelpText = "Your Client ID")]
        public string ClientId { get; set; }

        [Option('s', "clientSecret", Required = false, HelpText = "Your Client Secret")]
        public string ClientSecret { get; set; }

        [Option('e', "env", Required = false, HelpText = "Environment")]
        public string Environment { get; set; }

        [Option('n', "Name", Required = true, HelpText = "Registry Name")]
        public string RegistryName { get; set; }
    }

    [Verb("message", HelpText = "Send a message")]
    internal class MessageOptions
    {
        [Option('t', "messageType", Required = true, HelpText = "Type of message to send")]
        public MessageTypes MessageType { get; set; }

        [Option('f', "file", Required = true, HelpText = "File name to read from")]
        public string File { get; set; }

        [Option('c', "clientId", Required = false, HelpText = "Your Client ID")]
        public string ClientId { get; set; }

        [Option('s', "clientSecret", Required = false, HelpText = "Your Client Secret")]
        public string ClientSecret { get; set; }

        [Option('e', "env", Required = false, HelpText = "Environment")]
        public string Environment { get; set; }

        [Option('t', "targetRegistry", Required = false, HelpText = "Target Registry")]
        public string TargetRegistry { get; set; }

        [Option('a', "attachments", Required = false, HelpText = "File attachments")]
        public string Attachment { get; set; }
        //TODO Use comma seperated attachments
    }

    [Verb("download", HelpText = "Download any incoming attachments")]
    internal class DownloadOptions
    {
        [Option('c', "clientId", Required = false, HelpText = "Your Client ID")]
        public string ClientId { get; set; }

        [Option('s', "clientSecret", Required = false, HelpText = "Your Client Secret")]
        public string ClientSecret { get; set; }

        [Option('e', "env", Required = false, HelpText = "Environment")]
        public string Environment { get; set; }

        [Option('g', "guid", Required = false, HelpText = "Attachment Guid")]
        public Guid AttachmentGuid { get; set; }
    }

}