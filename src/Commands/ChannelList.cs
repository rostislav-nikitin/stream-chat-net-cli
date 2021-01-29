using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StreamChat.Cli.Commands.Extensions;

namespace StreamChat.Cli.Commands
{
    [CommandDescriptor("channel", "list", new[]
    {
        "[--limit={Limit(Default=100,Max=100)}]",
        "[--offset={Offset(Default=0)}]"
    })]
    public class ChannelList : CommandBase
    {

        public ChannelList(
            Client client,
            IConfiguration configuration,
            ILogger<ChannelList> logger) : base(client, configuration, logger)
        {
        }
        public override async Task<string> Execute()
        {
            StringBuilder result = new StringBuilder();

            QueryChannelsOptions opts = new QueryChannelsOptions();
            opts.WithLimit(Limit);
            opts.WithOffset(Offset);

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