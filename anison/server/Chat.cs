using System;
using System.Collections;
using UnityEngine;
using socket.io;

namespace anison.server {
	
	public class Chat : MonoBehaviour {

		const string URL_CHAT = "http://anison.chat.stzapp.net";

		static Socket socket;

		public static Chat Connect(string roomId) {
			Chat instance = new Chat ();
			string serverUrl = URL_CHAT + "?roomId=" + roomId;
			socket = Socket.Connect (serverUrl);
			return instance;
		}

		public void Message (string message) {
			socket.Emit("message",message);
		}

		public void On(string eventName, Action callback) {
			if (null != socket) {
				socket.On(eventName, callback);
			}
		}

		public void On(string eventName, Action<string> callback) {
			if (null != socket) {
				socket.On(eventName, callback);
			}
		}
	}
}

