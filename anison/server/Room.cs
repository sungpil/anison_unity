using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace anison.server {
	
	[System.Serializable]
	public class UserStatus {
		
		public string status;
		public string roomId;

		override public string ToString() {
			return "status=" + status + ", roomId=" + roomId;
		}
	}

	public class RoomResult {
		public bool isError { get; private set; }
		public UserStatus userStatus { get; private set; }
		public RoomResult(bool isError, UserStatus userStatus) {
			this.isError = isError;
			this.userStatus = userStatus;
		}
	}

	public class Room : MonoBehaviour {

		const string URL_ROOM = "http://anison.room.stzapp.net";

		static Room mInstance;

		public static Room Instance
		{
			get
			{
				if (mInstance == null){
					GameObject go = new GameObject();
					mInstance = go.AddComponent<Room>();
				}
				return mInstance;
			}
		}

		public void GetUserStatus (string userId, Action<RoomResult> callback) {
			string requestUrl = URL_ROOM + "?id=" + userId;
			StartCoroutine(Reqeust(requestUrl, callback));
		}

		IEnumerator Reqeust(string requestUrl, Action<RoomResult> callback) {
			UnityWebRequest www = UnityWebRequest.Get(requestUrl);
			yield return www.Send();
			RoomResult result;
			if(www.isError) {
				Debug.Log(www.error);
				result = new RoomResult (true, null);
			}

			else {
				UserStatus userStatus = JsonUtility.FromJson<UserStatus> (www.downloadHandler.text);
				result = new RoomResult (false, userStatus);
			}
			if (null != callback) {
				callback (result);
			}
		}
	}
}

