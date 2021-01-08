using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StreamChat.Cli.Commands;

namespace StreamChat.Cli
{
    class Program
    {
		public delegate ICommand CommandResolver(string key);

		private static string[] _args;

		private static readonly IDictionary<string, string> _mappings = new Dictionary<string, string>()
		{
			{"usercreate", "userupdate"}
		};

		private static IConfiguration _configuration;
		private static ILogger<Program> _logger;

		private static string _apiKey;
		private static string _apiSecret;


        static async Task Main(string[] args)
        {
			_args = args;

			if(Array.IndexOf(_args, "--help") > -1
				|| _args.Length < 2)
			{
				Console.WriteLine(GetHelp());
				return;
			}

			var serviceCollection = new ServiceCollection();
			Configure(serviceCollection);
			ConfigureServices(serviceCollection);
			var serviceProvider = serviceCollection.BuildServiceProvider();

			// Log API key / secret
			_logger = serviceProvider.GetService<ILogger<Program>>();
			_logger.LogInformation($"API key: {_apiKey}, API secret: {_apiSecret}");

			string cmd = CreateCommand(args[0], args[1]);
			var command = serviceProvider
					.GetServices<ICommand>()
					.FirstOrDefault(s => s.GetType().Name.Equals(cmd, StringComparison.InvariantCultureIgnoreCase));

			if(command != null)
			{
				 Console.WriteLine(await command.Execute());
			}
			else
			{
				Console.WriteLine(GetHelp());
			}
        }

        private static string CreateCommand(string command, string action)
        {
             string result = $"{command}{action}";
			 
			 if(_mappings.ContainsKey(result))
			 	result = _mappings[result];

			return result;
        }

		private static string GetHelp()
        {
            StringBuilder result = new StringBuilder();

			result.AppendLine("Usage:");
			result.AppendLine("\tschat-cli --help");
			result.AppendLine("\tschat-cli {entity} {action} [--parameter1=value...--parameterN=value] [--debug]\n");
			result.AppendLine("{entity} {action} [--parameter1=value...--parameterN=value]");
			result.AppendLine("\tuserToken create\n\t\t --username={username}");
			result.AppendLine("\tuser {create|update}\n\t\t--username={username}\n\t\t--role={Admin|Anonymous|Any|AnyAuthenticated|ChannelMember|ChannelModerator|Guest|User}\n\t\t--name={FullName}");
			result.AppendLine("\tchannelType create\n\t\t --name={ChannelTypeName}\n\t\t --automod={AI|Disabled(default)|Simple}\n\t\t --mutes={True|False}\n\t\t --connectEvents={True|False}\n\t\t --maxMessageLength={MaxMessageLength}\n\t\t --messageRetention={MessageRetention}\n\t\t --reactions={True|False}\n\t\t --readEvents={True|False}\n\t\t --replies={True|False}\n\t\t --search={True|False}\n\t\t --typingEvents={True|False}\n\t\t --commands=\"{Command1 Command2 ... CommandN}\"");
			result.AppendLine("\tchannelType list");
			result.AppendLine("\tchannelType get\n\t\t --name={ChannelTypeName}");

			return result.ToString();
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
			if(Array.IndexOf(_args, "--debug") > -1)
			{
				services.AddLogging(builder =>
					builder.AddConsole()
				);
			}
			else
			{
				services.AddLogging();
			}

			services.AddTransient<ICommand, UserTokenCreate>();
			services.AddTransient<ICommand, UserUpdate>();
			services.AddTransient<ICommand, ChannelTypeList>();
			services.AddTransient<ICommand, ChannelTypeGet>();
			services.AddTransient<ICommand, ChannelTypeCreate>();

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

