namespace StreamChat.Cli.Commands
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
	using StreamChat;

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

			var channelTypes = await _client.ListChannelTypes();
			var result = CreateChannelTypeInfo(channelTypes[name]);

			_logger.LogInformation("Executed");

			return result;
		}

		private string CreateChannelTypeInfo(ChannelTypeInfo channelTypeInfo)
		{
			const int StringBuilderSizeDefault = 1024;

			StringBuilder result = new StringBuilder(StringBuilderSizeDefault);

			result.AppendLine($"Created at:\t\t {channelTypeInfo.CreatedAt}");
			result.AppendLine($"Udpated at:\t\t {channelTypeInfo.UpdatedAt}");
			result.AppendLine($"Name:\t\t\t {channelTypeInfo.Name}");
			result.AppendLine($"Typing events:\t\t {channelTypeInfo.TypingEvents}");
			result.AppendLine($"Read events:\t\t {channelTypeInfo.ReadEvents}");
			result.AppendLine($"Connect events:\t\t {channelTypeInfo.ConnectEvents}");
			result.AppendLine($"Search:\t\t\t {channelTypeInfo.CreatedAt}");
			result.AppendLine($"Reactions:\t\t {channelTypeInfo.Reactions}");
			result.AppendLine($"Replies:\t\t {channelTypeInfo.Replies}");
			result.AppendLine($"Mutes:\t\t\t {channelTypeInfo.Mutes}");
			result.AppendLine($"Message retention:\t {channelTypeInfo.MessageRetention}");
			result.AppendLine($"Max message length:\t {channelTypeInfo.MaxMessageLength}");
			result.AppendLine($"Automod:\t\t {channelTypeInfo.Automod}");

			return result.ToString();
		}
	}
}
