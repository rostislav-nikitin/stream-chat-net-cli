# stream-chat-net-cli
The Stream Chat CLI (Command Line Interface)

## Requirements
.NET Core 3.1

## Clone
```console
git clone https://github.com/rostislav-nikitin/stream-chat-net-cli.git
```

## Build
### Set app settings with your custom API_KEY / API_SECRET
#### Scenario 1. Replace API_KEY / API_SECRET in the appsettings.json
Use this scenario if you will *NOT* commit your changes. Otherwise appsettings.json width you sensitive data will be commited into the repo.
```JSON
{
	"ConnectionStrings":
	{
		"StreamChat": "API_KEY, API_SECRET"
	}
}
```
#### Scenario 2. Create an appsettings.Development.json
Use this scenarion if you will commit your changes. The `appsettings.Development.json` inside a .gitignore and will be not commited into the repo.
* Add appsettings.Development.json as below with your API_KEY / API_SECRET
```JSON
{
	"ConnectionStrings":
	{
		"StreamChat": "API_KEY, API_SECRET"
	}
}
```
* Uncomment line below inside a StreamChat.Cli.cspoj.
```JSON
<None Include="appsettings.Development.json" CopyToOutputDirectory="Always" />
```

```console
cd ./src
make build
```

## Run
```console
make run
```
