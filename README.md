# anison_unity
anison unity client

# Example
```c#
    using anison.server;
    
    ....
    
    void Awake () {
        Room.Instance.GetUserStatus ("27", (UserStatus userStatus) => {
            if(userStatus.status == "waiting") {
                Debug.Log("waiting");
            } else {
                testChatServer(userStatus.roomId);
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
