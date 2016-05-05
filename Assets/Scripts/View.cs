using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class View : MonoBehaviour {
	public Text title;
	public Text preface;
	public Text postscript;
    public Text questionText;
    public Text aText;
    public Text bText;
    public Text cText;
    public Text dText;
    public Text resultText;

	public Image resultImage;
	public Text resultBtnText;

    public GameObject begin;
    public GameObject result;
    public GameObject end;
    public GameObject play;

	void Awake() {
		string root = "Game/View/Camera/Canvas/Main/";
		title = GameObject.Find(root + "Begin/Title").GetComponent<Text>();
		preface = GameObject.Find(root  + "Begin/Text").GetComponent<Text>();
		postscript = GameObject.Find(root + "End/Text").GetComponent<Text>();
		questionText = GameObject.Find(root + "Play/Question/Text").GetComponent<Text>();
		aText = GameObject.Find(root + "Play/Answer/A/Text").GetComponent<Text>();
		bText = GameObject.Find(root + "Play/Answer/B/Text").GetComponent<Text>();
		cText = GameObject.Find(root + "Play/Answer/C/Text").GetComponent<Text>();
		dText = GameObject.Find(root +"Play/Answer/D/Text").GetComponent<Text>();
		resultText = GameObject.Find(root + "Result/Text").GetComponent<Text>();

		begin = GameObject.Find("Main/Begin");
		play = GameObject.Find("Main/Play");
		result = GameObject.Find("Main/Result");
		end = GameObject.Find("Main/End");
	}

}
