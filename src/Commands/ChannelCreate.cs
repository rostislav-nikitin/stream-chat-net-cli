using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StreamChat.Cli;
using StreamChat.Cli.Commands.Extensions;

namespace StreamChat.Cli.Commands
{
    [CommandDescriptor("channel", "create", "[--id={ChannelID}] --type={ChannelType} --creator={UserId} [--users=\"{UserId1 UserId2...UserIdN}\"]")]
    public class ChannelCreate : ICommand
    {
        private readonly Client _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ChannelCreate> _logger;

        public ChannelCreate(
            Client client,
            IConfiguration configuration,
            ILogger<ChannelCreate> logger)
        {
            _client = client;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> Execute()
        {
            const char ListSeparator = ' ';

            var id = _configuration.GetValue<string>("id", Guid.NewGuid().ToString());

            var type = _configuration.GetValue<string>("type");
            if(string.IsNullOrWhiteSpace(type))
                throw Extensions.Extensions.GetInvalidParameterNullOrWhiteSpaceException(nameof(type));
            _logger.LogInformation($"type: {type}");

            var creator = _configuration.GetValue<string>("creator");
            if(string.IsNullOrWhiteSpace(creator))
                throw Extensions.Extensions.GetInvalidParameterNullOrWhiteSpaceException(nameof(creator));
            _logger.LogInformation($"creator: {creator}");

            var channel = _client.Channel(type, id);

            var users = _configuration.GetValue<string>("users", string.Empty)
                .Split(ListSeparator, System.StringSplitOptions.RemoveEmptyEntries);

            var channelState = await channel.Create(creator, users);

            return channelState.ToInfo();

                        
        }
    }
}