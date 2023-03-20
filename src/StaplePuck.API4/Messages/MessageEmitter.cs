using Amazon.SimpleNotificationService;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StaplePuck.Core.Models;
using StaplePuck.Data;

public class MessageEmitter : IMessageEmitter
{
    private readonly SNSSettings _settings;
    private readonly ILogger _logger;

    public MessageEmitter(IOptions<SNSSettings> options, ILogger<MessageEmitter> logger)
    {
        _settings = options.Value;
        _logger = logger;
    }

    public async Task StatsUpdated(StatsUpdated data)
    {
        if (_settings?.StatsUpdatedTopicARN != null)
        {
            var client = new AmazonSimpleNotificationServiceClient();
            var message = JsonConvert.SerializeObject(data);
            _logger.LogInformation($"Sending message: {message}. To topic: {_settings.StatsUpdatedTopicARN}");
            await client.PublishAsync(_settings.StatsUpdatedTopicARN, message);
        }
    }

    public async Task ScoreUpdated(ScoreUpdated scoreUpdated)
    {
        if (_settings?.ScoreUpdatedTopicARN != null)
        {
            var client = new AmazonSimpleNotificationServiceClient();
            var message = JsonConvert.SerializeObject(scoreUpdated);
            _logger.LogInformation($"Sending message: {message}. To topic: {_settings.ScoreUpdatedTopicARN}");
            await client.PublishAsync(_settings.ScoreUpdatedTopicARN, message);
        }
    }
}
