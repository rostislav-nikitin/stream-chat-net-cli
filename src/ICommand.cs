namespace StreamChat.Cli
{
	using System.Threading.Tasks;

	public interface ICommand
	{
		Task<string> Execute();
	}
}
