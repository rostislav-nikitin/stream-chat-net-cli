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
        "--channelId={ChannelId}"
    })]
    public class ChannelMessageList : ICommand
    {
        public Client _clinet;
        IConfiguration _configuration;
        public ILogger<ChannelMessageList> _logger;

        public ChannelMessageList(
            Client clinet,
            IConfiguration configuration,
            ILogger<ChannelMessageList> logger)
            
        {
            _clinet = clinet;
            _configuration = configuration;
            _logger = logger;

        }

        public async Task<string> Execute()
        {
            const int ChannelsLimitDefault = 100;

            var channelId = _configuration.GetValue<string>("channelId", null);
            if(string.IsNullOrWhiteSpace(channelId))
                throw Extensions.Extensions.GetInvalidParameterNullOrWhiteSpaceException(nameof(channelId));

            QueryChannelsOptions opts = new QueryChannelsOptions();
            opts.WithLimit(ChannelsLimitDefault);
            opts.WithFilter(new Dictionary<string, object>() {{"id", channelId}});
            
            var channels = await _clinet.QueryChannels(opts);
            var channel = channels?.FirstOrDefault();
            if(channel == null)
                throw new ArgumentException("Channel not exists.");


            StringBuilder result = new StringBuilder();

            channel.Messages.ForEach(message => result.AppendLine(message.ToPreview()));

            return result.ToString();
        }
    }
}