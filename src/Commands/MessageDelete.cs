using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace StreamChat.Cli.Commands.Commands
{
    [CommandDescriptor("message", "delete", "--id={MessageId}")]
    public class MessageDelete : ICommand
    {
        private readonly Client _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MessageDelete> _logger;

        public MessageDelete(
            Client client,
            IConfiguration configuration,
            ILogger<MessageDelete> logger)
        {
            _client = client;
            _configuration = configuration;
            _logger = logger;
            
        }
        public async Task<string> Execute()
        {
            var id = _configuration.GetValue<string>("id");
            if(string.IsNullOrWhiteSpace(id))
                throw Extensions.Extensions.GetInvalidParameterNullOrWhiteSpaceException(nameof(id));

            _logger.LogInformation($"Message: {id}");

            var message = await _client.DeleteMessage(id);

            return null;
        }
    }
}