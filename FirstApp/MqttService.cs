using MQTTnet;
using MQTTnet.Client;
using System.Threading.Tasks;

public class MqttService
{
    private IMqttClient _mqttClient;

    public async Task ConnectAsync(string broker, int port)
    {
        var factory = new MqttFactory();
        _mqttClient = factory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(broker, port)
            .Build();

        await _mqttClient.ConnectAsync(options);
    }

    public async Task PublishAsync(string topic, string message)
    {
        if (_mqttClient?.IsConnected == true)
        {
            var mqttMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(message)
                .Build();

            await _mqttClient.PublishAsync(mqttMessage);
        }
    }

    public async Task DisconnectAsync()
    {
        if (_mqttClient?.IsConnected == true)
        {
            await _mqttClient.DisconnectAsync();
        }
    }
}