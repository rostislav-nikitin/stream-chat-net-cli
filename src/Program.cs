using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

using StreamChat;

using StreamChat.Cli.Commands;

namespace StreamChat.Cli
{
    class Program
    {
		public delegate ICommand CommandResolver(string key);

		private static string[] _args;

		private static IConfiguration _configuration;
		private static ILogger<Program> _logger;

		private static string _apiKey;
		private static string _apiSecret;


        static async Task Main(string[] args)
        {
			_args = args;
			if(_args.Length < 2)
				throw new ArgumentException("Invalid arguments\nUse:\n\tschatcli {entity} {action} [parameter 1 ... parameter n]");

			string cmd = $"{args[0]}{args[1]}";
			Console.WriteLine(cmd);

			var serviceCollection = new ServiceCollection();
			Configure(serviceCollection);
			ConfigureServices(serviceCollection);
			var serviceProvider = serviceCollection.BuildServiceProvider();

			// Log API key / secret
			_logger = serviceProvider.GetService<ILogger<Program>>();
			_logger.LogInformation($"API key: {_apiKey}, API secret: {_apiSecret}");

			var command = serviceProvider
					.GetServices<ICommand>()
					.First(s => s.GetType().Name.Equals(cmd, StringComparison.InvariantCultureIgnoreCase));

			var result = await command.Execute();

			Console.WriteLine(result);
        }

		private static void Configure(IServiceCollection services)
		{
			var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
			
			ConfigurationBuilder configBuilder = new ConfigurationBuilder();

			configBuilder.AddJsonFile("appsettings.json", true, true);
			configBuilder.AddJsonFile($"appsettings.{environmentName}.json", true, true);

			_configuration = configBuilder.Build();

			ConfigurationBuilder commandLineConfigBuilder = new ConfigurationBuilder();
			commandLineConfigBuilder.AddCommandLine(_args);
			var commandLineConfig = commandLineConfigBuilder.Build();
 
			services.AddSingleton<IConfiguration>(commandLineConfig);
		}

		private static void ConfigureServices(IServiceCollection services)
		{
			services.AddLogging(builder =>
				builder.AddConsole()
			);

			services.AddTransient<ICommand, UserTokenCreate>();
			services.AddTransient<ICommand, ChannelTypeList>();
			services.AddTransient<ICommand, ChannelTypeGet>();
			services.AddSingleton(CreateStreamChatClient(services));
		}

		private static Client CreateStreamChatClient(IServiceCollection services)
		{
			const string ConnectionStringKey = "StreamChat";
			const string ConnectionStringSeparator = ",";

			// Get api key / secret
			var connectionString = _configuration.GetConnectionString(ConnectionStringKey);
			var connectionStringParts = connectionString.Split(ConnectionStringSeparator);
			_apiKey = connectionStringParts[0].Trim();
			_apiSecret = connectionStringParts[1].Trim();

			// Init client
			var result = new Client(_apiKey, _apiSecret);

			return result;
		}
    }
}
