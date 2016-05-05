using UnityEngine;
using System;
using System.Collections;
using Utils;
using UnityEngine.UI;

public class Ctrl : MonoBehaviour {
	public Model _model;
	public View _view;
	private QA _qa;
	private Question[] _usedQuestions;
	private	Question[] _allQuestions;
    private FSM _fsm = new FSM ();
    private int _score = 0;
    private int _index = 0;
    private bool _correct = false;

    public void FsmPost (string msg) {
        _fsm.post (msg);		
    }

    private State BeginState () {
		State begin = new State ("begin");
        begin.onStart += delegate {
			_view.begin.SetActive (true);
			StartCoroutine(Begin());
        };
        begin.onFinish += delegate {
            _view.begin.SetActive (false);
        };
			
		begin.addAction ("event_BeginBtn", delegate {
            return "play";
        });
        return begin;
    }

	private IEnumerator Begin() {
		_view.preface.text = "资源分离中...";
		yield return StartCoroutine(ResSeperator.SeperatoToSDCard());
		yield return StartCoroutine(LoadConfig());	
	}

    private State PlayState () {
		State play = new State ("play");

        play.onStart += delegate {
			Question q = _usedQuestions [_index] as Question;
            _view.questionText.text = q.question;
            _view.aText.text = q.answers [0];
            _view.bText.text = q.answers [1];
            _view.cText.text = q.answers [2];
            _view.dText.text = q.answers [3];
			_view.play.SetActive(true);
        };
		
		play.onFinish += delegate {
			_view.play.SetActive(false);
		};
			
        play.addAction ("event_A", delegate {
			Question q = _usedQuestions [_index] as Question;
			_correct = (q.correct == 0);
            return "result";
        });
        play.addAction ("event_B", delegate {
			Question q = _usedQuestions [_index] as Question;
			_correct = (q.correct == 1);
            return "result";
        });
        play.addAction ("event_C", delegate {
			Question q = _usedQuestions [_index] as Question;
			_correct = (q.correct == 2);
            return "result";
        });
        play.addAction ("event_D", delegate {
			Question q = _usedQuestions [_index] as Question;
			_correct = (q.correct == 3);
            return "result";
        });
      return play;
    }

    private State ResultState () {
        State result = new State ("result");
        result.onStart += delegate {
			_view.play.SetActive(true);
				
			Question q = _usedQuestions[_index] as Question;
            if (_correct) {
                ++_score;
                _view.resultText.text = q.rightInfo;
                _view.result.SetActive (true);
				_view.resultText.color = Color.green;
				_view.resultBtnText.color = Color.green;
            } else {
                _view.resultText.text = q.wrongInfo;
                _view.result.SetActive (true);
				_view.resultText.color = Color.red;
				_view.resultBtnText.color = Color.red;
            }
        };
		result.onFinish += delegate {
            _view.result.SetActive (false);
			_view.play.SetActive(false);
        };
			
        result.addAction ("event_OK", delegate {
            _index++;
			if (_index >= _usedQuestions.Length) {
                return "end";
            } else {
                return "play";
            }
				
        });
        return result;
    }

    private State EndState () {
        State end = new State ("end");
			
        end.onStart += delegate {
            _view.end.SetActive (true);
        };
        end.onFinish += delegate {
            this._index = 0;
            _score = 0;
            _view.end.SetActive (false);
        };
			
        end.addAction ("event_RestartBtn", delegate {
            return "begin";
        });
        return end;
    }
	
	void Awake() {
		PlayerPrefs.DeleteAll();
	}

	IEnumerator LoadConfig(){
		if (ResSeperator.IsSeperated) {
			_view.preface.text = "加载配表中...";
			string url = "";
			if (Application.platform == RuntimePlatform.Android) {
				url = "file://" + Application.persistentDataPath + "/config";
			} else if (Application.platform == RuntimePlatform.IPhonePlayer) {
				url = "jar:file://" + Application.dataPath + "!/assets/config";
			} else {
				url = "file://" +  Application.streamingAssetsPath + "/config";
			}
			using(WWW www = new WWW(url)) {
				yield return www;
				if (www.error != null)
					throw new Exception("WWW download had an error:" + www.error);
				if (www.isDone) {
					AssetBundle ab = www.assetBundle;
					TextAsset taQa = ab.LoadAsset<TextAsset>("qa.json");
					_qa = Json.Parse<QA>(taQa.text);
					TextAsset taAllQuestions = ab.LoadAsset<TextAsset>("questions");
					_allQuestions = Json.Parse<Question[]> (taAllQuestions.text);
				}
			}
			yield return new WaitForSeconds(1.0f);
			HandleConfig();
		}
	}

	void HandleConfig() {
		System.Random rand = new System.Random();
		_usedQuestions = new Question[_qa.usedQuestionLength];
		for(int i = 0; i<_qa.usedQuestionLength; ++i)
		{
			int randIndex = rand.Next(0, _allQuestions.Length);
			Debug.Log(_allQuestions[randIndex]);
			_usedQuestions[i] = _allQuestions[randIndex];
		}
		
		foreach(Question q in _usedQuestions) 
		{
			Debug.Log(q.question);
		}
        _index = 0;
		_view.title.text = _qa.title;	
		_view.preface.text = _qa.preface;
		_view.postscript.text = _qa.postscript;

		_view.infoText.text = "";
	}

	void Start() {
        _fsm.addState ("begin", BeginState ());
        _fsm.addState ("play", PlayState ());
        _fsm.addState ("result", ResultState ());
        _fsm.addState ("end", EndState ());
		
        _fsm.init ("begin");
	}

}
