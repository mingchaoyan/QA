using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ResSeperator  {
	public static bool IsSeperated {
		get {
			//if(Application.platform == RuntimePlatform.OSXEditor)
			//	return true;
			if(PlayerPrefs.HasKey("IsSeperated")) {
				return PlayerPrefs.GetInt("IsSeperated") == 1 ? true : false;
			} else {
				return false;	
			}
		}
		set {
			PlayerPrefs.SetInt("IsSeperated", value ? 1: 0);			
			PlayerPrefs.Save();
		}		
	}

	public static IEnumerator SeperatoToSDCard() {
		if (!IsSeperated) {
			ArrayList fileList = new ArrayList();
			fileList.Add ("config");
			fileList.Add ("config.manifest");
			fileList.Add ("StreamingAssets");
			fileList.Add ("StreamingAssets.manifest");
			string fromPath = Application.streamingAssetsPath;
			string toPath = Application.persistentDataPath;
			foreach(string fileName in fileList) {
				string url =  "file://" + fromPath + "/" + fileName;
				using(WWW www = new WWW(url)) {
					yield return www;
					if (www.error != null)
						throw new Exception("WWW download had an error:" + www.error);
					if (www.isDone) {
						FileTools.CreateFileAndDirectory(toPath + "/" + fileName, www.bytes);
					}	
				}
			}
			yield return new WaitForSeconds(1.0f);
			IsSeperated = true;
		}

	}
}
