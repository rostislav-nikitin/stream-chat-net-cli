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
#### Scenario 1. Replace API_KEY / API_SECRET in the appsettings.json or appsettings.Development.json
Use this scenario if you will **NOT** commit your changes. Otherwise the appsettings.json width you sensitive data will be commited into the repo.
```JSON
{
	"ConnectionStrings":
	{
		"StreamChat": "API_KEY, API_SECRET"
	}
}
```
#### Scenario 2. Use secret manager
Use this scenario if you will commit your changes into the public repository. In this case SreamChat API_KEY / API_SECRET will be **NOT** commited into the repo.
For this init your user secrets and add the ConnectionStrings:StreamChat key:
```console
make set-connection-string "API_KEY, API_SECRET"
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
### User token
- User token create
```console 
./schat-cli userToken create --userId={UserId} [--debug]
```
Example:
```console
./schat-cli userToken create --userId=TestUser
```
Result:
```console
eyJ0eTAiOiJKV1QfLCJhbGc6OiJIUz11NiJ9.eyt1c2VyX2lkIjoiU29tZVVzZXIifQ.gg2Lhd6fsvAtmimuDRQ14tq6iH5cYYm3F7K1sZS4P3w
```
### User
- User create
```console 
./schat-cli user create \
	[--id={Id}] \
	[--role={Admin|Anonymous|Any|AnyAuthenticated|ChannelMember|ChannelModerator|Guest|User(default)}] \
	[--name="{FullName}"] \
	[--debug]
```
Example:
```console
./schat-cli user create --id=TestUser --role=User --name="Test User"
``` 
Result:
```console
ID:		 SomeId
Role:		 user
Online:		 False
Last active:	 
Deactivated at: 
Deactivated at: 
Created at:	 1/5/2021 2:32:47 PM
Updated at:	 1/5/2021 2:32:47 PM
Name:		 Test User
```

- User update
```console 
./schat-cli user update \
	--id={Id} \
	[--role={Admin|Anonymous|Any|AnyAuthenticated|ChannelMember|ChannelModerator|Guest|User(default)}] \
	[--name="{FullName}"] \
	[--debug]
```
Example:
```console
./schat-cli user update --id=TestUser --role=User --name="Test User"
``` 
Result:
```console
ID:		 SomeId
Role:		 user
Online:		 False
Last active:	 
Deactivated at: 
Deactivated at: 
Created at:	 1/5/2021 2:32:47 PM
Updated at:	 1/5/2021 2:32:47 PM
Name:		 Test User
```

### Channel type
- Channel type create
```console
./schat-cli channelType create \
	 [--name={ChannelTypeName}] \
	 [--automod={AI|Disabled(default)|Simple}] \
	 [--mutes={True|False}] \
	 [--connectEvents={True|False}] \
	 [--maxMessageLength={MaxMessageLength}] \
	 [--messageRetention={MessageRetention}] \
	 [--reactions={True|False}] \
	 [--readEvents={True|False}] \
	 [--replies={True|False}] \
	 [--search={True|False}] \
	 [--typingEvents={True|False}] \
	 [--commands="{Command1 Command2 ... CommandN}"] \
	 [--debug]
```
Example:
```console
./schat-cli channelType create --name="TestChannel" \
 	--automod=ai --mutes=True --typingEvents=True
```
Result:
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
- Channel type get
```console
./schat-cli channelType get --name={ChannelTypeName} [--debug]
```
Example:
```console
./schat-cli channelType get --name=messaging
```
Result:
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
- Channel type list
```console 
./schat-cli channelType list [--debug]
```
Example:
```console
./schat-cli channelType list
```
Result:
```console
somestream
messaging
```
