# stream-chat-net-cli
The Stream Chat CLI (Command Line Interface)

## Requirements
.NET Core 3.1

## Clone
```console
git clone https://github.com/rostislav-nikitin/stream-chat-net-cli.git
```

## Build
### Update app settings with API_KEY / API_SECRET
#### Replace API_KEY / API_SECRET in appsettings.json
```JSON
{
	"ConnectionStrings":
	{
		"StreamChat": "API_KEY, API_SECRET"
	}
}
```
#### Add appsettings.Development.json
* Add appsettings.Development.json as below with your API_KEY / API_SECRET
```JSON
{
	"ConnectionStrings":
	{
		"StreamChat": "API_KEY, API_SECRET"
	}
}
```
* Uncomment line below inside a StreamChat.Cli.cspoj. The `appsettings.Development.json` inside a .gitignore and will not commited to the repo.
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
