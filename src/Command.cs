namespace StreamChat.Cli
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
	using StreamChat;

	internal class Command : ICommand
	{
		ILogger<Command> _logger;
		IConfiguration _configuration;
				
		Client _client;

		string _command;
		string _name;
		//string[] _parameters;

		private readonly IDictionary<string, Func<Task<string>>> _chatClientActions = 
				new Dictionary<string, Func<Task<string>>>();

		public Command(IConfiguration configuration, ILogger<Command> logger)
		{
			const string ConnectionStringKey = "StreamChat";
			const string ConnectionStringSeparator = ",";

			_logger = logger;
			_configuration = configuration;

			// Init actions
			_chatClientActions.Add("token-get", TokenGet);
			_chatClientActions.Add("channelType-list", ChannelTypeList);
			_chatClientActions.Add("channelType-get", ChannelTypeGet);

			// Get api key / secret
			var connectionString = _configuration.GetConnectionString(ConnectionStringKey);
			var connectionStringParts = connectionString.Split(ConnectionStringSeparator);
			string apiKey = connectionStringParts[0].Trim();
			string apiSecret = connectionStringParts[1].Trim();
			_logger.LogInformation($"API key: {apiKey}, API secret: {apiSecret}");

			// Set command & parameters
			_command = $"{_configuration["entity"]}-{_configuration["action"]}";
			_name = _configuration["name"];

			// Init client
			_client = new Client(apiKey, apiSecret);
		}

		public async Task<string> Execute()
		{
			_logger.LogInformation("Execution started");

			if(string.IsNullOrWhiteSpace(_command))
				return "Invalid command. Command is not specified";

			string result;

			if(_chatClientActions.TryGetValue(_command, out Func<Task<string>> func))
			{
				result = await func();
			}
			else
			{
				result = "Invalid command. Specified command not found";
			}

			_logger.LogInformation("Execution finished");

			return result;
		}

		private Task<string> TokenGet()
		{
			var result = _client.CreateUserToken("StereamChatClient");
			
			return Task.FromResult(result);
		}

		private async Task<string> ChannelTypeList()
		{
			const int StringBuilderSizeDefault = 16384;

			StringBuilder result = new StringBuilder(StringBuilderSizeDefault);

			var channelTypes = await _client.ListChannelTypes();
			foreach(var channelType in channelTypes)
			{
				result.AppendLine(channelType.Key);
			}

			return result.ToString();
		}

		private async Task<string> ChannelTypeGet()
		{
			var channelTypes = await _client.ListChannelTypes();
			var result = CreateChannelTypeInfo(channelTypes[_name]);

			return result;
		}

		private string CreateChannelTypeInfo(ChannelTypeInfo channelTypeInfo)
		{
			const int StringBuilderSizeDefault = 1024;

			StringBuilder result = new StringBuilder(StringBuilderSizeDefault);

			result.AppendLine($"Channel type: {_name}");
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
