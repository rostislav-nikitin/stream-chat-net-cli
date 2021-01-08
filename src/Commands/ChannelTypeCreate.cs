namespace StreamChat.Cli.Commands
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using StreamChat.Cli.Commands.Extensions;
    using StreamChat;
    using StreamChat.Cli;

    public class ChannelTypeCreate : ICommand
    {
        private readonly Client _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ChannelTypeCreate> _logger;

        public ChannelTypeCreate(
            Client client,
            IConfiguration configuration, 
            ILogger<ChannelTypeCreate> logger)
        {
            _client = client;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<string> Execute()
        {
            const char ListSeparator = ' ';

            var name = _configuration.GetValue<string>("name", Guid.NewGuid().ToString());
            _logger.LogInformation($"Name: {name}");

            var automod = _configuration.GetValue<string>("automod", Autmod.Disabled).ToLowerInvariant();
            if(automod == "ai")
                automod = automod.ToUpperInvariant();

            _logger.LogInformation($"Automod: {automod}");

            var mutes = _configuration.GetValue<bool>("mutes", default);
            _logger.LogInformation($"Mutes: {mutes}");

            var connectEvents = _configuration.GetValue<bool>("connectEvents", default);
            _logger.LogInformation($"ConnectEvents: {connectEvents}");

            var maxMessageLength = _configuration.GetValue<int?>("maxMessageLength", default);
            _logger.LogInformation($"MaxMessageLength: {maxMessageLength}");

            var messageRetention = _configuration.GetValue<string>("messageRetention", default);
            _logger.LogInformation($"MessageRetention: {messageRetention}");

            var reactions = _configuration.GetValue<bool>("reactions", default);
            _logger.LogInformation($"Reactions: {reactions}");

            var readEvents = _configuration.GetValue<bool>("readEvents", default);
            _logger.LogInformation($"ReadEvents: {readEvents}");

            var replies = _configuration.GetValue<bool>("replies", default);
            _logger.LogInformation($"Replies: {replies}");

            var search = _configuration.GetValue<bool>("search", default);
            _logger.LogInformation($"Search: {search}");

            var typingEvents = _configuration.GetValue<bool>("typingEvents", default);
            _logger.LogInformation($"TypingEvents: {typingEvents}");

            const char DoubleQuotes = '\"';
            const char SingleQuote = '\'';

            var commands = _configuration.GetValue<string>("commands", null)?
                .Trim(DoubleQuotes)
                .Trim(SingleQuote)
                .Split(ListSeparator).ToList();

            _logger.LogInformation($"Commands: {commands}");
                
            var channelTypeInput = new ChannelTypeInput()
            {
                Name = name,
                Automod = automod,
                Commands = commands,
                Mutes = mutes,
                ConnectEvents = connectEvents,
                MaxMessageLength = maxMessageLength,
                MessageRetention = messageRetention,
                Reactions = reactions,
                ReadEvents = readEvents,
                Replies = replies,
                Search = search,
                TypingEvents = typingEvents
            };

            var chanelTypeOutput = await _client.CreateChannelType(channelTypeInput);

            return chanelTypeOutput?.ToInfo();
        }

        
    }
}