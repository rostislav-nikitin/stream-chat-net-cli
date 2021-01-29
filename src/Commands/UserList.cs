using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StreamChat.Cli.Commands.Extensions;

namespace StreamChat.Cli.Commands
{
    [CommandDescriptor("user", "list", new []
    {
        "[--limit={Limit(Default=100,Max=100)}]",
        "[--offset={Offset(Default=0)}]"
    })]
    public class UserList : CommandBase
    {
        public UserList(
            Client client,
            IConfiguration configuration,
            ILogger<UserList> logger) : base(client, configuration, logger)
        {
        }

        public override async Task<string> Execute()
        {
            StringBuilder result = new StringBuilder();

            QueryUserOptions opts = new QueryUserOptions();
            opts.WithLimit(Limit);
            opts.WithOffset(Offset);

            var users = await _client.Users.Query(opts);
            int idx = 0;
            foreach(var user in users)
            {
                result.AppendLine($"{++idx}\t{user.ToPreview()}");
            }

            return result.ToString();
        }
    }
}