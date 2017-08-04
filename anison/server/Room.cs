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

		public void GetUserStatus (string userId, Action<UserStatus> callback) {
			string requestUrl = URL_ROOM + "?id=" + userId;
			StartCoroutine(Reqeust(requestUrl, callback));
		}

		IEnumerator Reqeust(string requestUrl, Action<UserStatus> callback) {
			UnityWebRequest www = UnityWebRequest.Get(requestUrl);
			yield return www.Send();

			if(www.isError) {
				Debug.Log(www.error);
			}

			else {
				Debug.Log(www.downloadHandler.text);
				UserStatus userStatus = JsonUtility.FromJson<UserStatus> (www.downloadHandler.text);
				if (null != callback) {
					callback (userStatus);
				}
			}
		}
	}
}

