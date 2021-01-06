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
Use this scenario if you will *NOT* commit your changes. Otherwise the appsettings.json width you sensitive data will be commited into the repo.
```JSON
{
	"ConnectionStrings":
	{
		"StreamChat": "API_KEY, API_SECRET"
	}
}
```
#### Scenario 2. Create an appsettings.Development.json
Use this scenarion if you will commit your changes. The `appsettings.Development.json` inside a .gitignore. And it will not be commited into the repo.
* Create an appsettings.Development.json as below with your API_KEY / API_SECRET
```JSON
{
	"ConnectionStrings":
	{
		"StreamChat": "API_KEY, API_SECRET"
	}
}
```
* Uncomment a line below inside a StreamChat.Cli.cspoj.
```JSON
<None Include="appsettings.Development.json" CopyToOutputDirectory="Always" />
```
### Linux
```console
cd ./src

#build
make build

# run some test command
make test
```

### Windows
Use a Visual Studio / Visual Studio Code

## Currently supported commands
- Create user token
```console schat-cli userToken create --username={UserName}```
