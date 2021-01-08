namespace StreamChat.Cli.Commands
{
	using System;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
    using StreamChat;
    using StreamChat.Cli.Commands.Extensions;

    internal class UserUpdate : ICommand
	{
		private readonly Client _client;
		private readonly IConfiguration _configuration;
		private readonly ILogger<Command> _logger;

		public UserUpdate(Client client, 
						IConfiguration configuration,
						ILogger<Command> logger)
		{
			_client = client;
			_configuration = configuration;
			_logger = logger;
		}

		public async Task<string> Execute()
		{
			_logger.LogInformation("Executing");

			var username = _configuration.GetValue<string>("username");
			if(string.IsNullOrWhiteSpace(username))
				username = Guid.NewGuid().ToString();
			_logger.LogInformation($"Username: {username}");

			var role = _configuration.GetValue<string>("role");
			if(string.IsNullOrWhiteSpace(role))
				role = Role.User;
			_logger.LogInformation($"Role: {role.ToLowerInvariant()}");

			var user = new User()
			{
				ID = username,
				Role = role
			};

			var name = _configuration.GetValue<string>("name");
			if(!string.IsNullOrWhiteSpace(name))
			{
				_logger.LogInformation($"Name: {name}");
				user.SetData("name", name);
			}	


			User userFromDB = await _client.Users.Update(user);
			
			_logger.LogInformation("Executed");

			return userFromDB?.ToInfo();
		}
		
	}
}
