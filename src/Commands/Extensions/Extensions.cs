namespace StreamChat.Cli.Commands.Extensions
{
    using System;
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

		public static string ToPreview(this User user)
		{
			return $"ID: {user.ID, -38}\t Role: {user.Role}\t Online: {user.Online}\t Last Active: {user.LastActive}";
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
			result.AppendLine($"Name:\t\t {user.GetData<string>("name")}");

			return result.ToString();
		}
		public static string ToPreview(this ChannelState channelState)
		{
			return $"CID: {channelState.Channel.CID, -72}\t Frozen: {channelState.Channel.Frozen}\t Member Count: {channelState.Channel.MemberCount}\t Created By: {channelState.Channel.CreatedBy?.ID, -20}\t Created At: {channelState.Channel.CreatedAt}";
		}

		public static string ToInfo(this ChannelState channelState)
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine($"ID:\t\t\t {channelState.Channel.ID}");
			result.AppendLine($"Type:\t\t\t {channelState.Channel.Type}");
			result.AppendLine($"CID:\t\t\t {channelState.Channel.CID}");
			result.AppendLine($"Created By:\t\t {channelState.Channel.CreatedBy?.ID}");
			result.AppendLine($"Created At:\t\t {channelState.Channel.CreatedAt}");
			result.AppendLine($"Updated At:\t\t {channelState.Channel.UpdatedAt}");
			result.AppendLine($"Deleted At:\t\t {channelState.Channel.DeletedAt}");
			result.AppendLine($"Last Message At:\t {channelState.Channel.LastMessageAt}");
			result.AppendLine($"Frozen:\t\t\t {channelState.Channel.Frozen}");
			result.AppendLine($"Member Count:\t\t {channelState.Channel.MemberCount}");

			return result.ToString();
		}
		
		public static string ToPreview(this Message message)
		{
			return $"ID: {message.ID, -38} Type: {message.Type, -10} Created at: {message.CreatedAt:yyyy/MM/dd HH:mm:ss}  Reply count: {message.ReplyCount, -3} Text: {message.Text}";
		}

		public static ArgumentException GetInvalidParameterNullOrWhiteSpaceException(string name)
		{
			return new ArgumentException($"Invalid parameter: \"--{name}\" is null of white space.");
		}
        
    }
}