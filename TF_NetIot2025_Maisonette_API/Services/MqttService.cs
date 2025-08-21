
using MQTTnet;

namespace TF_NetIot2025_Maisonette_API.Services
{
    public class MqttService : IHostedService
    {
        private readonly ILogger<MqttService> _logger;

        public MqttService(ILogger<MqttService> logger)
        {
            _logger = logger;
        }

        private IMqttClient? _client;
        private MqttClientOptions? _options;

        public bool IsConnected => _client?.IsConnected ?? false;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            MqttClientFactory factory = new MqttClientFactory();
            _client = factory.CreateMqttClient();

            _options = new MqttClientOptionsBuilder()
                .WithTcpServer("localhost",1883) // Broker local (image docker)
                .WithClientId("MaisonnetteAPI") // Id de l'api dans le broker
                .Build();

            _client.ConnectedAsync += (ct) =>
            {
                _logger.LogInformation("Connected");
                return Task.CompletedTask;
            };

            await _client.ConnectAsync(_options, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (IsConnected) 
            {
                await _client.DisconnectAsync();
                _logger.LogInformation("Disconected");
            }
        }

        public async Task PublishAsync(string topic, string payload)
        {
            if(!IsConnected)
            {
                _logger.LogError("Not connected");
                return;
            }

            MqttApplicationMessage message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .Build();

            await _client!.PublishAsync(message);
            _logger.LogInformation("Message send");
        }
    }
}
