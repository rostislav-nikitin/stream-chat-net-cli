using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace StreamChat.Cli.Commands
{
    [CommandDescriptor("channelType", "delete", "--name={ChannelTypeName}")]
    public class ChannelTypeDelete : ICommand
    {
        private readonly Client _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ChannelTypeDelete> _logger;

        public ChannelTypeDelete(
            Client client,
            IConfiguration configuration, 
            ILogger<ChannelTypeDelete> logger)
        {
            _client = client;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> Execute()
        {
            var name = _configuration.GetValue<string>("name");
            _logger.LogInformation($"Name: {name}");
            if(string.IsNullOrWhiteSpace(name))
                throw Extensions.Extensions.GetInvalidParameterNullOrWhiteSpaceException(nameof(name));

            await _client.DeleteChannelType(name);

            return string.Empty;
        }
    }
}