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
			var result = CreateChannelTypeInfo(name, channelTypes[name]);

			_logger.LogInformation("Executed");

			return result;
		}

		private string CreateChannelTypeInfo(string name, ChannelTypeInfo channelTypeInfo)
		{
			const int StringBuilderSizeDefault = 1024;

			StringBuilder result = new StringBuilder(StringBuilderSizeDefault);

			result.AppendLine($"Channel type: {name}");
			result.AppendLine($"\tCreated at: {channelTypeInfo.CreatedAt}");
			result.AppendLine($"\tUdpated at: {channelTypeInfo.UpdatedAt}");
			result.AppendLine($"\tName: {channelTypeInfo.Name}");
			result.AppendLine($"\tTyping events: {channelTypeInfo.TypingEvents}");
			result.AppendLine($"\tRead events: {channelTypeInfo.ReadEvents}");
			result.AppendLine($"\tConnect events: {channelTypeInfo.ConnectEvents}");
			result.AppendLine($"\tSearch: {channelTypeInfo.CreatedAt}");
			result.AppendLine($"\tReactions: {channelTypeInfo.Reactions}");
			result.AppendLine($"\tReplies: {channelTypeInfo.Replies}");
			result.AppendLine($"\tMutes: {channelTypeInfo.Mutes}");
			result.AppendLine($"\tMessage retention: {channelTypeInfo.MessageRetention}");
			result.AppendLine($"\tMax message length: {channelTypeInfo.MaxMessageLength}");
			result.AppendLine($"\tAutomod: {channelTypeInfo.Automod}");

			return result.ToString();
		}
	}
}
