using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

using StreamChat;

namespace StreamChat.Cli
{
    class Program
    {
		private static string[] _args;

        static async Task Main(string[] args)
        {
			_args = args;

			var serviceCollection = new ServiceCollection();
			Configure(serviceCollection);
			ConfigureServices(serviceCollection);
			var serviceProvider = serviceCollection.BuildServiceProvider();

			var command = serviceProvider.GetService<ICommand>();
			var result = await command.Execute();

			Console.WriteLine(result);
        }

		private static void Configure(IServiceCollection services)
		{
			var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
			
			ConfigurationBuilder configBuilder = new ConfigurationBuilder();

			configBuilder.AddJsonFile("appsettings.json", true, true);
			configBuilder.AddJsonFile($"appsettings.{environmentName}.json", true, true);
			configBuilder.AddCommandLine(_args);

			var conf = configBuilder.Build();
 
			services.AddSingleton<IConfiguration>(conf);
		}

		private static void ConfigureServices(IServiceCollection services)
		{
			services.AddLogging(builder =>
				builder.AddConsole()
			);

			services.AddTransient<ICommand, Command>();
		}
    }
}
