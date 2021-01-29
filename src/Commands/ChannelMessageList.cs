using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StreamChat.Cli.Commands.Extensions;

namespace StreamChat.Cli.Commands
{
    [CommandDescriptor("channelMessage", "list", new[]
    {
        "--channelId={ChannelId}",
        "[--limit={Limit(Default=100,Max=100)}]",
        "[--offset={Offset(Default=0)}]"
    })]
    public class ChannelMessageList : CommandBase
    {
        public ChannelMessageList(
            Client client,
            IConfiguration configuration,
            ILogger<ChannelMessageList> logger) : base(client, configuration, logger)
            
        {
        }

        public override async Task<string> Execute()
        {
            var channelId = _configuration.GetValue<string>("channelId", null);
            if(string.IsNullOrWhiteSpace(channelId))
                throw Extensions.Extensions.GetInvalidParameterNullOrWhiteSpaceException(nameof(channelId));
            _logger.LogInformation($"Channel id: {channelId}");

            QueryChannelsOptions opts = new QueryChannelsOptions();
            opts.WithFilter(new Dictionary<string, object>() {{"id", channelId}});
            opts.WithLimit(Limit);
            opts.WithOffset(Offset);
            
            var channels = await _client.QueryChannels(opts);
            var channel = channels?.FirstOrDefault();
            if(channel == null)
                throw new ArgumentException("Channel not exists.");


            StringBuilder result = new StringBuilder();

            channel.Messages.ForEach(message => result.AppendLine(message.ToPreview()));

            return result.ToString();
        }
    }
}