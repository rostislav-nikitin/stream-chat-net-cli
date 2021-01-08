namespace StreamChat.Cli.Commands
{
	using System.Threading.Tasks;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
	using StreamChat;

	internal class UserTokenCreate : ICommand
	{
		private readonly Client _client;
		private readonly IConfiguration _configuration;
		private readonly ILogger<Command> _logger;

		public UserTokenCreate(Client client, 
						IConfiguration configuration,
						ILogger<Command> logger)
		{
			_client = client;
			_configuration = configuration;
			_logger = logger;
		}

		public Task<string> Execute()
		{
			_logger.LogInformation("Executing");

			var username = _configuration.GetValue<string>("username");

			_logger.LogInformation($"Username: {username}");

			var result = _client.CreateUserToken(username);

			_logger.LogInformation("Executed");

			return Task.FromResult(result);
		}
	}
}
