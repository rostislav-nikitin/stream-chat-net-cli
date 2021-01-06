# stream-chat-net-cli
The Stream Chat CLI (Command Line Interface)

![Mindmap](https://github.com/rostislav-nikitin/stream-chat-net-cli/blob/master/mind_map.png?raw=true)

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

#run some test command
make test
```

### Windows
Use a Visual Studio / Visual Studio Code

## Currently supported commands
### Token
- Create user token
```console 
./schat-cli userToken create --username={UserName}
```
Typical result:
```console
eyJ0eTAiOiJKV1QfLCJhbGc6OiJIUz11NiJ9.eyt1c2VyX2lkIjoiU29tZVVzZXIifQ.gg2Lhd6fsvAtmimuDRQ14tq6iH5cYYm3F7K1sZS4P3w
```

### Channel type
- List channel types
```console 
./schat-cli channelType list
```
Typical result:
```console
somestream
messaging
```
- Get channel type
```console
./schat-cli channelType get --name={ChannelTypeName}
```
Typical result:
```console
Channel type: somestream
	Created at: 1/4/2021 1:18:33 PM
	Udpated at: 1/4/2021 1:18:33 PM
	Name: somestream
	Typing events: True
	Read events: True
	Connect events: True
	Search: 1/4/2021 1:18:33 PM
	Reactions: True
	Replies: True
	Mutes: True
	Message retention: infinite
	Max message length: 5000
	Automod: disabled
```
