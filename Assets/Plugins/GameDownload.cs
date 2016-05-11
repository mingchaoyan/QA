using UnityEngine;
using System.Collections;

public class GameDownload {
	public static IEnumerator DownloadFiles() {
		var totalBytes = GetContentBytes();
		var downloadedBytes = 0;
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
