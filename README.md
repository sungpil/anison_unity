# anison_unity
anison unity client

# Example
```c#
    using anison.server;
    
    ....
    
    void Awake () {
	Room.Instance.GetUserStatus ("28", (RoomResult result) => {
		if(result.isError) {
			Debug.Log("error");
		} else {
			Debug.Log(result.userStatus.status);
			if(result.userStatus.status == "playing") {
				testChatServer(result.userStatus.roomId);
			}
		}
	});
    }

    private void testChatServer(string roomId) {
        Chat chatServer = Chat.Connect (roomId);
        chatServer.On ("connect", () => {
            Debug.Log("Connect");
            chatServer.Message("Helloooo~!");
        });
        chatServer.On ("message", OnMessage);
    }

    private void OnMessage(string message) {
        Debug.Log("Message="+message);
    }
```
