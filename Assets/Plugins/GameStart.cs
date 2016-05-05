using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class GameStart : MonoBehaviour {
	public static Assembly mainAssembly;

	// Use this for initialization
	void Start () {
		StartCoroutine(LoadMain());
	}

	IEnumerator LoadMain() {
		string dllName = "assembly-csharp";
		string url = "";
		if (Application.platform == RuntimePlatform.Android) {
			url = "file://" + Application.persistentDataPath + "/" + dllName;
		} else if (Application.platform == RuntimePlatform.IPhonePlayer) {
			url = "jar:file://" + Application.dataPath + "!/assets/config";
		} else {
			url = "file://" +  Application.persistentDataPath + "/" + dllName;
		}
		using(WWW www = new WWW(url)) {
			yield return www;
			if(www.error != null)
				throw new Exception("WWW download had an error:" + www.error);
			if (www.isDone) {
				AssetBundle ab = www.assetBundle;
				TextAsset txt = ab.LoadAsset("Assembly-CSharp", typeof(TextAsset)) as TextAsset;
				try {
					mainAssembly = System.Reflection.Assembly.Load(txt.bytes);
					Type type = mainAssembly.GetType("Test");
					gameObject.AddComponent(type);
					www.assetBundle.Unload(false);
				} catch(Exception ex) {
					Debug.Log(ex);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
