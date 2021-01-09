using System.Linq;
using System.Reflection;
using System.Text;

namespace StreamChat.Cli
{
    internal class HelpGenerator
    {
        public string Generate()
        {
            StringBuilder result = new StringBuilder();

            result.Append(GenerateHeader());
            result.Append(GenerateCommands());

            return result.ToString();
        }

        private string GenerateHeader()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Usage:");
			result.AppendLine("\tschat-cli --help");
			result.AppendLine("\tschat-cli {entity} {action} [--parameter1=value...--parameterN=value] [--debug]\n");

            return result.ToString();
        }

        private string GenerateCommands()
        {
            StringBuilder result = new StringBuilder();

            Assembly.GetExecutingAssembly().GetTypes()
                .OrderBy(type => type.Name)
                .Select(type => type.GetCustomAttribute<CommandDescriptorAttribute>())
                .Where(ca => ca != null)
                .OrderBy(ca => ca.Entity)
                .ToList()
                .ForEach(ca => 
                    {
                        result.Append($"\t{ca.Entity} {ca.Action}");
                        ca.Parameters?.ToList().ForEach(p => result.Append($"\n\t\t{p}"));
                        result.AppendLine();
                    });


            return result.ToString();
        }
    }
}