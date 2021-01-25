using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StreamChat.Cli.Commands.Extensions;

namespace StreamChat.Cli.Commands
{
    [CommandDescriptor("channelMessage", "send", new[]
    {
        "--channelId={ChannelId}",
        "--userId={UserId}",
        "--message='{Message text}'"
    })]
    public class ChannelMessageSend : ICommand
    {
        private readonly Client _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ChannelMessageSend> _logger;

        public ChannelMessageSend(
            Client client,
            IConfiguration configuration,
            ILogger<ChannelMessageSend> logger)
        {
            _client = client;
            _configuration= configuration;
            _logger = logger;
        }

        public async Task<string> Execute()
        {
            var channelId = _configuration.GetValue<string>("channelId");
            if(string.IsNullOrWhiteSpace(channelId))
                throw Extensions.Extensions.GetInvalidParameterNullOrWhiteSpaceException(nameof(channelId));
            _logger.LogInformation($"Channel id: {channelId}");

            var userId = _configuration.GetValue<string>("userId");
            if(string.IsNullOrWhiteSpace(userId))
                throw Extensions.Extensions.GetInvalidParameterNullOrWhiteSpaceException(nameof(userId));
            _logger.LogInformation($"Channel id: {userId}");

            var message = _configuration.GetValue<string>("message");
            if(string.IsNullOrWhiteSpace(message))
                throw Extensions.Extensions.GetInvalidParameterNullOrWhiteSpaceException(nameof(message));
            _logger.LogInformation($"Message: {message}");

            QueryChannelsOptions opts = new QueryChannelsOptions();
            opts.WithFilter(new System.Collections.Generic.Dictionary<string, object>(){{"id", channelId}});
            var channelStates = await _client.QueryChannels(opts);

            if(!channelStates.Any())
                throw new ApplicationException($"Channel with id: \"{channelId}\" was not found.");

            var channelState = channelStates.First();

            var channel = _client.Channel(channelState.Channel.Type, channelState.Channel.ID);
            var result = await channel.SendMessage(new MessageInput() { Text = message }, userId);

            return result.ToInfo();
            
        }
    }
}