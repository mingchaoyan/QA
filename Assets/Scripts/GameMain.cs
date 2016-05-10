using UnityEngine;
using System.Collections;
using System;
using JsonTools;
using UnityEngine.UI;
using System.Collections.Generic;
using FullSerializer;

public class GameMain : MonoBehaviour {
	public static QA qa;
	public static Question[] allQuestions;
	public Text info;
	public GameObject loadPanelGO;
	public GameObject mainPanelGO;

	void Awake() {
		loadPanelGO = GameObject.Find("Game/View/Camera/Canvas/Load");
		mainPanelGO = GameObject.Find("Game/View/Camera/Canvas/Main");
		info = GameObject.Find("Game/View/Camera/Canvas/Load/Text").GetComponent<Text>();
	}

	// Use this for initialization
	IEnumerator Start () {
		yield return StartCoroutine(LoadConfig());		
		loadPanelGO.SetActive(false);
		mainPanelGO.SetActive(true);
		GameObject ctrlGO = GameObject.Find("Game/Ctrl");
		ctrlGO.AddComponent<Ctrl>();
	}

	IEnumerator LoadConfig(){
		if (ResSeperator.IsSeperated) {
			string url = "";
			if (Application.platform == RuntimePlatform.Android) {
				url = "file://" + Application.persistentDataPath + "/config";
			} else if (Application.platform == RuntimePlatform.IPhonePlayer) {
				url = "jar:file://" + Application.dataPath + "!/assets/config";
			} else {
				url = "file://" +  Application.persistentDataPath + "/config";
			}
			using(WWW www = new WWW(url)) {
				yield return www;
				if (www.error != null)
					throw new Exception("WWW download had an error:" + www.error);
				if (www.isDone) {
					AssetBundle ab = www.assetBundle;
					TextAsset taQa = ab.LoadAsset<TextAsset>("qa.json");
					qa = Json.Parse<QA>(taQa.text);
					TextAsset taAllQuestions = ab.LoadAsset<TextAsset>("questions");
					allQuestions = Json.Parse<Question[]> (taAllQuestions.text);
				}
			}
			yield return new WaitForSeconds(1.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
