using UnityEngine;
using System;
using System.Collections;
using Utils;
using UnityEngine.UI;

public class Ctrl : MonoBehaviour {
	public Model _model;
	public View _view;
	private Question[] _usedQuestions;
    private FSM _fsm = new FSM ();
    private int _score = 0;
    private int _index = 0;
    private bool _correct = false;

    public void FsmPost (string msg) {
        _fsm.post (msg);		
    }

    private State BeginState () {
		State begin = new State ("begin");
		Button btn = _view.begin.GetComponentInChildren<Button>();
		btn.onClick.AddListener(delegate () {
			FsmPost("event_BeginBtn");	
		});
        begin.onStart += delegate {
			HandleConfig();
			_view.begin.SetActive (true);
			_view.play.SetActive(false);
			_view.result.SetActive(false);
			_view.end.SetActive(false);
				
        };
        begin.onFinish += delegate {
            _view.begin.SetActive (false);
        };
			
		begin.addAction ("event_BeginBtn", delegate {
            return "play";
        });
        return begin;
    }



    private State PlayState () {
		State play = new State ("play");
		string root = "Game/View/Camera/Canvas/Main/Play/Answer/";
		GameObject.Find(root + "A").GetComponent<Button>().onClick.AddListener(delegate {
			FsmPost("event_A");
		});
		GameObject.Find(root + "B").GetComponent<Button>().onClick.AddListener(delegate {
			FsmPost("event_B");
		});
		GameObject.Find(root + "C").GetComponent<Button>().onClick.AddListener(delegate {
			FsmPost("event_C");
		});
		GameObject.Find(root + "D").GetComponent<Button>().onClick.AddListener(delegate {
			FsmPost("event_D");
		});

        play.onStart += delegate {
			Question q = _usedQuestions [_index] as Question;
            _view.questionText.text = q.question;
            _view.aText.text = q.answers [0];
            _view.bText.text = q.answers [1];
            _view.cText.text = q.answers [2];
            _view.dText.text = q.answers [3];
			_view.play.SetActive(true);
			_view.begin.SetActive(false);
			_view.end.SetActive(false);
			_view.result.SetActive(false);
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
		Button btn = _view.result.GetComponentInChildren<Button>();
		btn.onClick.AddListener(delegate {
			FsmPost("event_OK");
		});
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
		Button btn = _view.end.GetComponentInChildren<Button>();
		btn.onClick.AddListener(delegate {
			FsmPost("event_RestartBtn");
		});
			
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

	void HandleConfig() {
		System.Random rand = new System.Random();
		_usedQuestions = new Question[GameMain.qa.usedQuestionLength];
		for(int i = 0; i<GameMain.qa.usedQuestionLength; ++i)
		{
			int randIndex = rand.Next(0, GameMain.allQuestions.Length);
			Debug.Log(GameMain.allQuestions[randIndex]);
			_usedQuestions[i] = GameMain.allQuestions[randIndex];
		}
		
		foreach(Question q in _usedQuestions) 
		{
			Debug.Log(q.question);
		}
        _index = 0;
		_view.title.text = GameMain.qa.title;	
		_view.preface.text = GameMain.qa.preface;
		_view.postscript.text = GameMain.qa.postscript;

	}

	void Awake() {
		GameObject modelGO = GameObject.Find("Game/Model");
		GameObject viewGO = GameObject.Find("Game/View");
		modelGO.AddComponent<Model>();
		viewGO.AddComponent<View>();
		_model = modelGO.GetComponent<Model>();
		_view =  viewGO.GetComponent<View>();
	}

	void Start() {
        _fsm.addState ("begin", BeginState ());
        _fsm.addState ("play", PlayState ());
        _fsm.addState ("result", ResultState ());
        _fsm.addState ("end", EndState ());
		
        _fsm.init ("begin");
	}
}
