# stream-chat-net-cli
The Stream Chat CLI (Command Line Interface)

![Mindmap](https://github.com/rostislav-nikitin/stream-chat-net-cli/blob/main/mind_map.png)

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
### Help
./schat-cli --help
### Token
- Create user token
```console 
./schat-cli userToken create --username={UserName} [--debug]
```
Typical result:
```console
eyJ0eTAiOiJKV1QfLCJhbGc6OiJIUz11NiJ9.eyt1c2VyX2lkIjoiU29tZVVzZXIifQ.gg2Lhd6fsvAtmimuDRQ14tq6iH5cYYm3F7K1sZS4P3w
```

- Create/Update user
```console 
./schat-cli user {create|update} \
	--username={UserName} \
	--role={Admin|Anonymous|Any|AnyAuthenticated|ChannelMember|ChannelModerator|Guest|User} \
	--name={Full_Name}
	[--debug]
```
Typical result:
```console
ID:		 SomeId
Role:		 user
Online:		 False
Last active:	 
Deactivated at: 
Deactivated at: 
Created at:	 1/5/2021 2:32:47 PM
Updated at:	 1/5/2021 2:32:47 PM
```

### Channel type
- Create channel type
```console
./schat-cli channelType create --name="TestChannel" --automod=ai --mutes=True --typingEvents=True [--debug]
```
Typical result:
```console
Created at:		 1/5/2021 1:18:33 PM
Udpated at:		 1/5/2021 1:18:33 PM
Name:			 TestChannel
Typing events:		 True
Read events:		 False
Connect events:		 False
Search:			 1/5/2021 1:18:33 PM
Reactions:		 False
Replies:		 False
Mutes:			 True
Message retention:	 infinite
Max message length:	 5000
Automod:		 disabled
```
- List channel types
```console 
./schat-cli channelType list [--debug]
```
Typical result:
```console
somestream
messaging
```
- Get channel type
```console
./schat-cli channelType get --name={ChannelTypeName} [--debug]
```
Typical result:
```console
Created at:		 1/5/2021 1:18:33 PM
Udpated at:		 1/5/2021 1:18:33 PM
Name:			 messaging
Typing events:		 True
Read events:		 True
Connect events:		 True
Search:			 1/5/2021 1:18:33 PM
Reactions:		 True
Replies:		 True
Mutes:			 True
Message retention:	 infinite
Max message length:	 5000
Automod:		 disabled
```
