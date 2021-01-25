namespace StreamChat.Cli.Commands
{
	using System;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
    using StreamChat;
    using StreamChat.Cli.Commands.Extensions;

	[CommandDescriptor("user", "create", new [] {
		"[--id={id}]", 
		"[--role={Admin|Anonymous|Any|AnyAuthenticated|ChannelMember|ChannelModerator|Guest|User(default)}]",
		"[--name='{FullName}']"})]
    internal class UserCreate : ICommand
	{
		private readonly Client _client;
		private readonly IConfiguration _configuration;
		private readonly ILogger<Command> _logger;

		public UserCreate(Client client, 
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

			var id = _configuration.GetValue<string>("id", Guid.NewGuid().ToString());
			_logger.LogInformation($"ID: {id}");

			var role = _configuration.GetValue<string>("role", Role.User)?.ToLowerInvariant();
			_logger.LogInformation($"Role: {role.ToLowerInvariant()}");

			var online = _configuration.GetValue<bool>("online");
			_logger.LogInformation($"Online: {online}");

			var user = new User()
			{
				ID = id,
				Role = role,
				Online = online
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
