using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StreamChat.Cli.Commands.Extensions;

namespace StreamChat.Cli.Commands
{
    [CommandDescriptor("user", "list")]
    public class UserList : ICommand
    {
        private readonly Client _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserList> _logger;

        public UserList(
            Client client,
            IConfiguration configuration,
            ILogger<UserList> logger)
        {
            _client = client;
            _configuration = configuration;
            _logger = logger;    
        }

        public async Task<string> Execute()
        {
            const int LimitDefault = 100;
            StringBuilder result = new StringBuilder();

            QueryUserOptions opts = new QueryUserOptions();
            opts.WithLimit(LimitDefault);
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