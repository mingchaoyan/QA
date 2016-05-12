using UnityEngine;
using System.Collections;
using System;
using Zi;
using UnityEngine.UI;

public class GameDownload {
	public static IEnumerator DownloadFiles() {
		Text info = GameStart.info;
		var totalBytes = GetContentBytes();
		var downloadedBytes = 0;
		string downloadUrl = JsonHelpers.GetString(GameStart.appConfig, "downloadUrl", "");
		string toPath = Application.persistentDataPath;
		foreach(var file in GameUpdate.files) {
			var url = downloadUrl + GameUpdate.latestClientVersion.Release + "/" + file.name;
			info.text += "\n 开始" + url;
			Debug.Log(url);
			using(WWW www = new WWW(url)) {
				yield return www;
				if (www.error != null)
					throw new Exception("WWW download had an error:" + www.error);
				if (www.isDone) {
					FileTools.CreateFileForce(toPath + "/" + file.name, www.bytes);
					info.text += " -> 完成";
				}	
			}
		}

		yield return new WaitForSeconds(1.0f);
	}

	static int GetContentBytes() {
		var size = 0;
		foreach(var file in GameUpdate.files) {
			size += file.size;
		}
		return size;
	}
}
