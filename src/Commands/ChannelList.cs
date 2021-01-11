using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StreamChat.Cli.Commands.Extensions;

namespace StreamChat.Cli.Commands
{
    [CommandDescriptor("channel", "list")]
    public class ChannelList : ICommand
    {
        private readonly Client _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ChannelList> _logger;

        public ChannelList(
            Client client,
            IConfiguration configuration,
            ILogger<ChannelList> logger)
        {
            _client = client;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<string> Execute()
        {
            const int ChannelsLimit = 100;

            StringBuilder result = new StringBuilder();

            QueryChannelsOptions opts = new QueryChannelsOptions();
            opts.WithLimit(ChannelsLimit);

            var channels = await _client.QueryChannels(opts);
            int idx = 0;
            foreach(var channel in channels)
            {
                result.AppendLine($"{++idx}\t {channel.ToPreview()}");
            }

            return result.ToString();
        }
    }
}