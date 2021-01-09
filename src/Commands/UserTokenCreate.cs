namespace StreamChat.Cli.Commands
{
    using System;
    using System.Threading.Tasks;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
	using StreamChat;

	[CommandDescriptor("userToken", "create", "--userId={UserId}")]
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

			var userId = _configuration.GetValue<string>("userId");
			_logger.LogInformation($"Username: {userId}");
			if(string.IsNullOrWhiteSpace(userId))
				throw new ArgumentException("Invalid parameter: \"userId\" is null or white space.");

			var result = _client.CreateUserToken(userId);

			_logger.LogInformation("Executed");

			return Task.FromResult(result);
		}
	}
}
