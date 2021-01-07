namespace StreamChat.Cli.Commands
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
	using StreamChat;

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
			
			var result = UserGet(userFromDB);

			_logger.LogInformation("Executed");

			return result;
		}
		private string UserGet(User user)
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine($"ID:\t\t {user.ID}");
			result.AppendLine($"Role:\t\t {user.Role}");
			result.AppendLine($"Online:\t\t {user.Online}");
			result.AppendLine($"Last active:\t {user.LastActive}");
			result.AppendLine($"Deactivated at: {user.DeactivatedAt}");
			result.AppendLine($"Deactivated at: {user.DeletedAt}");
			result.AppendLine($"Created at:\t {user.CreatedAt}");
			result.AppendLine($"Updated at:\t {user.UpdatedAt}");

			return result.ToString();
		}
	}
}
