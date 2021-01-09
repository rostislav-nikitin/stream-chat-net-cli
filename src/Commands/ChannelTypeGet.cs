namespace StreamChat.Cli.Commands
{
	using System;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
    using StreamChat.Cli.Commands.Extensions;
    using StreamChat;

	[CommandDescriptor("channelType", "get", "--name={ChannelTypeName}")]
	internal class ChannelTypeGet: ICommand
	{
		private readonly Client _client;
		private readonly IConfiguration _configuration;
		private readonly ILogger<Command> _logger;

		public ChannelTypeGet(Client client, 
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

			var name = _configuration.GetValue<string>("name");
			_logger.LogInformation($"Name: {name}");
			if(string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Invalid parameter: \"name\" is null of white space.");

			var channelTypes = await _client.ListChannelTypes();
			var result = channelTypes[name]?.ToInfo();

			_logger.LogInformation("Executed");

			return result;
		}

		
	}
}
