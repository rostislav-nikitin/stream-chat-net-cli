namespace StreamChat.Cli.Commands.Extensions
{
	using System.Text;
	
    public static class Extensions
    {
        public static string ToInfo(this ChannelType channelType)
		{
			const int StringBuilderSizeDefault = 1024;

			StringBuilder result = new StringBuilder(StringBuilderSizeDefault);

			result.AppendLine($"Created at:\t\t {channelType.CreatedAt}");
			result.AppendLine($"Udpated at:\t\t {channelType.UpdatedAt}");
			result.AppendLine($"Name:\t\t\t {channelType.Name}");
			result.AppendLine($"Typing events:\t\t {channelType.TypingEvents}");
			result.AppendLine($"Read events:\t\t {channelType.ReadEvents}");
			result.AppendLine($"Connect events:\t\t {channelType.ConnectEvents}");
			result.AppendLine($"Search:\t\t\t {channelType.CreatedAt}");
			result.AppendLine($"Reactions:\t\t {channelType.Reactions}");
			result.AppendLine($"Replies:\t\t {channelType.Replies}");
			result.AppendLine($"Mutes:\t\t\t {channelType.Mutes}");
			result.AppendLine($"Message retention:\t {channelType.MessageRetention}");
			result.AppendLine($"Max message length:\t {channelType.MaxMessageLength}");
			result.AppendLine($"Automod:\t\t {channelType.Automod}");

			return result.ToString();
		}

		public static string ToInfo(this User user)
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