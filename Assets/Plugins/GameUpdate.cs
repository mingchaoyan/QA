using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Zi;
using UnityEngine.UI;
using FullSerializer;
using JsonTools;

public class GameUpdate {
	public class ClientVersion {
		public int Major;
		public int Minor;
		public int Patch;
		public int Release;

		public ClientVersion(string clientVersion) {
			if (!string.IsNullOrEmpty(clientVersion)) {
				var strs = clientVersion.Split('.');
				if(strs.Length == 4) {
					int temp;
					if(int.TryParse(strs[0], out temp)) {
						Major = temp;
					}
					if(int.TryParse(strs[1], out temp)) {
						Minor = temp;
					}
					if(int.TryParse(strs[2], out temp)) {
						Patch = temp;
					}
					if(int.TryParse(strs[3], out temp)) {
						Release = temp;
					}
				}
			}
		}
	}
	public static ClientVersion clientVersion;
	public class FileInfo {
		public string name;
		public int size;
		public FileInfo(string n, int s) {
			name = n;
			size = s;
		}
	}
	public static List<FileInfo> files = new List<FileInfo>();

	public static IEnumerator CheckUpdate() {
		Text info = GameStart.info;
		string centerAddress = JsonHelpers.GetString(GameStart.appConfig, "centerAddress", "");
		info.text += "\ncenterAddress: " + centerAddress;
		string apiKey = JsonHelpers.GetString(GameStart.appConfig, "apiKey", "");
		info.text += "\napiKey: " + apiKey;
		string clientVersionVal = JsonHelpers.GetString(GameStart.appConfig, "clientVersion", "");
		clientVersion = new ClientVersion(clientVersionVal);
		info.text += "\n+Major: " + clientVersion.Major;
		info.text += "\n+Minor: " + clientVersion.Minor;
		info.text += "\n+Patch: " + clientVersion.Patch;
		info.text += "\n+Release: " + clientVersion.Release;
		var fsDic = fsData.CreateDictionary();
		var request = fsDic.AsDictionary;
		request["major"] = new fsData(clientVersion.Major);
		request["minor"] = new fsData(clientVersion.Minor);
		request["patch"] = new fsData(clientVersion.Patch);
		request["release"] = new fsData(clientVersion.Release);
		var param = Json.Generate(fsDic);
		WebHelpers.httpResCallbackHandler += HandleCheckUpdate;
		WebHelpers.CreatHttpRequest(centerAddress + "/version", apiKey, param, "POST");

		yield return new WaitForSeconds(1.0f);
	}

	public static void HandleCheckUpdate(bool isSuccess, HttpStatusCode httpStatusCode, string response) {
		Text info = GameStart.info;
		WebHelpers.httpResCallbackHandler -= HandleCheckUpdate;

		if(!isSuccess) {
			info.text = "\n热更错误！";
		}
		#if UNITY_EDITOR
		Debug.Log("response:" + response);
		#endif

		var updateInfo = Json.Parse(response);
		var updateInfoDic = updateInfo.AsDictionary;
		var clientVersion = updateInfoDic["latest_version"].AsDictionary;
		var major = (int) clientVersion["major"].AsInt64;
		var minor = (int) clientVersion["minor"].AsInt64;
		var patch = (int) clientVersion["patch"].AsInt64;
		var release = (int) clientVersion["release"].AsInt64;
		//info.text += "\n+++++++++++++++++++++++++";
		info.text += "\n-Major: " + major;
		info.text += "\n-Minor: " + minor;
		info.text += "\n-Patch: " + patch;
		info.text += "\n-Release: " + release;
		var updateList = updateInfoDic.ContainsKey("update_list") 
			? updateInfoDic["update_list"].AsList
			: new List<fsData>();
		foreach(var file in updateList) {
			var filename = file.AsDictionary["name"].AsString;
			var sizeVal = file.AsDictionary["size"];
			int filesize;
			if(sizeVal.IsString) {
				filesize = int.Parse(sizeVal.AsString);
			} else {
				filesize = (int)sizeVal.AsInt64;
			}
			FileInfo fileInfo = new FileInfo(filename, filesize);
			files.Add(fileInfo);
		}
	}
}
