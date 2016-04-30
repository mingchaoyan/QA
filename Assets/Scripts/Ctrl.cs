using UnityEngine;
using System;
using System.Collections;
using Utils;

public class Ctrl : MonoBehaviour
{
    public Model _model;
    public View _view;
	public TimeLessQuestion timelessQuestion;
	public  Question[] _questions;
    private FSM _fsm = new FSM ();
    private int _score = 0;
    private int _index = 0;
    private bool _correct = false;

    public void fsmPost (string msg)
    {
        _fsm.post (msg);		
    }

    private State beginState ()
    {
		State begin = new State ("begin");
        begin.onStart += delegate {
            _index = 0;
            _view._begin.SetActive (true);
        };
        begin.onFinish += delegate {
            _view._begin.SetActive (false);
        };
			
		begin.addAction ("event_BeginBtn", delegate {
            return "show";
        });
        return begin;
    }

    private State showState ()
    {
		State show = new State ("show");
		Question q1 = _questions [_index] as Question;
		
        show.onStart += delegate {
			Question q = _questions [_index] as Question;
            _view._questionText.text = q.question;
            _view._aText.text = q.answers [0];
            _view._bText.text = q.answers [1];
            _view._cText.text = q.answers [2];
            _view._dText.text = q.answers [3];
			_view._play.SetActive(true);
        };
		
		show.onFinish += delegate
		{
			_view._play.SetActive(false);
		};
			
        show.addAction ("event_A", delegate {
			_correct = (q1.correct == 0);
            return "result";
        });
        show.addAction ("event_B", delegate {
			_correct = (q1.correct == 1);
            return "result";
        });
        show.addAction ("event_C", delegate {
			_correct = (q1.correct == 2);
            return "result";
        });
        show.addAction ("event_D", delegate {
			_correct = (q1.correct == 3);
            return "result";
        });
			
			
        return show;
    }

		
    private State resultState ()
    {
        State result = new State ("result");
        result.onStart += delegate {
			_view._play.SetActive(true);
				
			Question q = _questions[_index] as Question;
            if (_correct) {
                ++_score;
                _view._resultText.text = q.rightInfo;
                _view._result.SetActive (true);
            } else {
                _view._resultText.text = q.wrongInfo;
                _view._result.SetActive (true);
            }
        };
		result.onFinish += delegate {
            _view._result.SetActive (false);
			_view._play.SetActive(false);
        };
			
        result.addAction ("event_OK", delegate {
            _index++;
			if (_index >= _questions.Length) {
                return "end";
            } else {
                return "show";
            }
				
        });
        return result;
    }

    private State endState ()
    {
        State end = new State ("end");
			
        end.onStart += delegate {
            _view._scoreText.text = _score.ToString ();
            _view._end.SetActive (true);
        };
        end.onFinish += delegate {
				
            this._index = 0;
            _score = 0;
            _view._end.SetActive (false);
        };
			
        end.addAction ("event_RestartBtn", delegate {
            return "begin";
        });
        return end;
    }
	
	void Awake()
	{
		timelessQuestion = Json.Parse<TimeLessQuestion>(((TextAsset)Resources.Load("Config/timeless_question")).text);
		Question[] allQuestions = Json.Parse<Question[]>(((TextAsset)Resources.Load("Config/questions")).text);
		System.Random rand = new System.Random();
		_questions = new Question[timelessQuestion.questionLength];
		for(int i = 0; i<timelessQuestion.questionLength; ++i)
		{
			int randIndex = rand.Next(0, allQuestions.Length);
			Debug.Log(allQuestions[randIndex]);
			_questions[i] = allQuestions[randIndex];
		}
		
		foreach(Question q in _questions) 
		{
			Debug.Log(q.question);
		}
		
		_view._preface.text = timelessQuestion.preface;
		_view._postscript.text = timelessQuestion.postscript;
	}

    void Start ()
    {
        _fsm.addState ("begin", beginState ());
        _fsm.addState ("show", showState ());
        _fsm.addState ("result", resultState ());
        _fsm.addState ("end", endState ());
		
        _fsm.init ("begin");
    }
}
