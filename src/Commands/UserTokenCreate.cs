namespace StreamChat.Cli.Commands
{
    using System;
    using System.Threading.Tasks;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
	using StreamChat;

	[CommandDescriptor("userToken", "create", "--user={UserId}")]
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

			var user = _configuration.GetValue<string>("user");
			_logger.LogInformation($"UserId: {user}");
			if(string.IsNullOrWhiteSpace(user))
				throw Extensions.Extensions.GetInvalidParameterNullOrWhiteSpaceException(nameof(user));

			var result = _client.CreateUserToken(user);

			_logger.LogInformation("Executed");

			return Task.FromResult(result);
		}
	}
}
