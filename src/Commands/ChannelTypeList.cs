namespace StreamChat.Cli.Commands
{
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Logging;
	using StreamChat;

	internal class ChannelTypeList : ICommand
	{
		private readonly Client _client;
		private readonly ILogger<Command> _logger;

		public ChannelTypeList(Client client, ILogger<Command> logger)
		{
			_client = client;
			_logger = logger;
		}

		public async Task<string> Execute()
		{
			_logger.LogInformation("Executing");

			const int StringBuilderSizeDefault = 16384;

			StringBuilder result = new StringBuilder(StringBuilderSizeDefault);

			var channelTypes = await _client.ListChannelTypes();
			foreach(var channelType in channelTypes)
			{
				result.AppendLine(channelType.Key);
			}
			_logger.LogInformation("Executed");

			return result.ToString();
		}
	}
}
