using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace StreamChat.Cli
{
    public abstract class CommandBase : ICommand
    {
        private const int MaxLimit = 100;
        private const int LimitDefault = MaxLimit;
        private const int OffsetDefault = 0;
        protected readonly Client _client;
        protected readonly IConfiguration _configuration;
        protected readonly ILogger _logger;

        public CommandBase(
            Client client, 
            IConfiguration configuration, 
            ILogger logger)
        {
            _client = client;
            _configuration = configuration;
            _logger = logger;
        }

        protected virtual int LimitUserDefault => LimitDefault;

        protected virtual int OffsetUserDefault => OffsetDefault;

        public abstract  Task<string> Execute();


        protected int Limit
        {
            get
            {
                int result = Math.Min(MaxLimit, _configuration.GetValue<int>("limit", LimitUserDefault));
                _logger.LogInformation($"Limit: {result}");

            return result;
            }
        }

        protected int Offset
        {
            get
            {
                int result = _configuration.GetValue<int>("offset", OffsetUserDefault);
                _logger.LogInformation($"Offset: {result}");

                return result;
            }
        }
    }
}