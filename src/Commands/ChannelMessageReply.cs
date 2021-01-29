using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StreamChat.Cli.Commands.Extensions;

namespace StreamChat.Cli.Commands
{
    [CommandDescriptor("channelMessage", "reply", new[]
    {
        "--channelId={ChannelId}",
        "--userId={UserId}",
        "--messageId={MessageId}",
        "--message='{Message}'"
    })]
    public class ChannelMessageReply : ICommand
    {
        private readonly Client _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ChannelMessageReply> _logger;

        public ChannelMessageReply(
            Client client,
            IConfiguration configuration,
            ILogger<ChannelMessageReply> logger)
        {
            _client = client;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> Execute()
        {
            var channelId = _configuration.GetValue<string>("channelId");
            if(string.IsNullOrWhiteSpace(channelId))
                throw Extensions.Extensions.GetInvalidParameterNullOrWhiteSpaceException(nameof(channelId));
            _logger.LogInformation($"Channel ID: {channelId}");

            var userId = _configuration.GetValue<string>("userId");
            if(string.IsNullOrWhiteSpace(userId))
                throw Extensions.Extensions.GetInvalidParameterNullOrWhiteSpaceException(nameof(userId));
            _logger.LogInformation($"User ID: {userId}");

            var messageId = _configuration.GetValue<string>("messageId");
            if(string.IsNullOrWhiteSpace(messageId))
                throw Extensions.Extensions.GetInvalidParameterNullOrWhiteSpaceException(nameof(messageId));
            _logger.LogInformation($"Message ID: {messageId}");

            var message = _configuration.GetValue<string>("message");
            if(string.IsNullOrWhiteSpace(message))
                throw Extensions.Extensions.GetInvalidParameterNullOrWhiteSpaceException(nameof(message));
            _logger.LogInformation($"Message: {message}");


            QueryChannelsOptions opts = new QueryChannelsOptions();
            opts.WithFilter(new System.Collections.Generic.Dictionary<string, object>{{"id", channelId}});

            var channelsState = await _client.QueryChannels(opts);
            var channelState = channelsState.FirstOrDefault();

            var channel = _client.Channel(channelState.Channel.Type, channelState.Channel.ID);

            var replyMessage = new MessageInput()
            {
                Text = message
            };

            var messageObj = await channel.SendMessage(replyMessage, userId, messageId);
            
            return messageObj.ToInfo();
        }
    }
}