using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {
	public static Assembly mainAssembly;
	public Text info;

	void Awake() {
		Debug.Log(Application.persistentDataPath);
		Debug.Log(Application.dataPath);
		Debug.Log(Application.streamingAssetsPath);
		PlayerPrefs.DeleteAll();
	}

	IEnumerator Start () {
		info.text = "正在分离资源...";
		yield return StartCoroutine(ResSeperator.SeperatoToSDCard());

		info.text = "正在加载程序...";
		yield return StartCoroutine(LoadAssembly());

		yield return StartCoroutine(LoadMainScene());

		Type type = mainAssembly.GetType("GameMain");
		Debug.Log(type);
		GameObject gamemainGO = GameObject.Find("Game");
		gamemainGO.AddComponent(type);
	}

	IEnumerator LoadMainScene() {
		DontDestroyOnLoad(this);
		SceneManager.LoadScene("Main");
		yield return new WaitForSeconds(1.0f);
		
	}

	IEnumerator LoadAssembly() {
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
		yield return new WaitForSeconds(1.0f);
	}



	// Update is called once per frame
	void Update () {
	
	}
}
