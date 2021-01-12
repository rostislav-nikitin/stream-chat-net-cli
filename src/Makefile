build:
	dotnet build
set-connection-string:
	dotnet user-secrets init
	dotnet user-secrets set "ConnectionStrings:StreamChat" "$(KEY)"
test:
	export ASPNETCORE_ENVIRONMENT=Development
	dotnet ./bin/Debug/netcoreapp3.1/StreamChat.Cli.dll userToken create --user=TestUser --debug
	#dotnet ./bin/Debug/netcoreapp3.1/StreamChat.Cli.dll channelType list --debug
	#dotnet ./bin/Debug/netcoreapp3.1/StreamChat.Cli.dll channelType get --name=messaging --debug
