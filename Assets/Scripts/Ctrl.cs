using UnityEngine;
using System;
using System.Collections;
using Utils;

public class Ctrl : MonoBehaviour {
	public Model _model;
	public View _view;
	private TimelessQuestion _timelessQuestion;
	private Question[] _questions;
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
            _index = 0;
            _view.begin.SetActive (true);
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
		Question q1 = _questions [_index] as Question;
		
        play.onStart += delegate {
			Question q = _questions [_index] as Question;
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
			_correct = (q1.correct == 0);
            return "result";
        });
        play.addAction ("event_B", delegate {
			_correct = (q1.correct == 1);
            return "result";
        });
        play.addAction ("event_C", delegate {
			_correct = (q1.correct == 2);
            return "result";
        });
        play.addAction ("event_D", delegate {
			_correct = (q1.correct == 3);
            return "result";
        });
      return play;
    }

    private State ResultState () {
        State result = new State ("result");
        result.onStart += delegate {
			_view.play.SetActive(true);
				
			Question q = _questions[_index] as Question;
            if (_correct) {
                ++_score;
                _view.resultText.text = q.rightInfo;
                _view.result.SetActive (true);
            } else {
                _view.resultText.text = q.wrongInfo;
                _view.result.SetActive (true);
            }
        };
		result.onFinish += delegate {
            _view.result.SetActive (false);
			_view.play.SetActive(false);
        };
			
        result.addAction ("event_OK", delegate {
            _index++;
			if (_index >= _questions.Length) {
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
		_timelessQuestion = Json.Parse<TimelessQuestion>(((TextAsset)Resources.Load("Config/timeless_question")).text);
		Question[] allQuestions = Json.Parse<Question[]>(((TextAsset)Resources.Load("Config/questions")).text);
		System.Random rand = new System.Random();
		_questions = new Question[_timelessQuestion.questionLength];
		for(int i = 0; i<_timelessQuestion.questionLength; ++i)
		{
			int randIndex = rand.Next(0, allQuestions.Length);
			Debug.Log(allQuestions[randIndex]);
			_questions[i] = allQuestions[randIndex];
		}
		
		foreach(Question q in _questions) 
		{
			Debug.Log(q.question);
		}
		
		_view.preface.text = _timelessQuestion.preface;
		_view.postscript.text = _timelessQuestion.postscript;
	}

    void Start () {
        _fsm.addState ("begin", BeginState ());
        _fsm.addState ("play", PlayState ());
        _fsm.addState ("result", ResultState ());
        _fsm.addState ("end", EndState ());
		
        _fsm.init ("begin");
    }
}
