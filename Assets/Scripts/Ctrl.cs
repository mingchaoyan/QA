using UnityEngine;
using System.Collections;
using Utils;

public class Ctrl : MonoBehaviour
{
    public Model _model;
    public View _view;
	public ArrayList _questions;
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

    private void result (int select)
    {
		Question q = _questions[_index] as Question;
        if (select == q.correct) {
            _correct = true;
        } else {
            _correct = false;			
        }
    }

    private State showState ()
    {
		State show = new State ("show");
        show.onStart += delegate {
			Question q = _questions [_index] as Question;
            _view._questionText.text = q.question;
            _view._aText.text = q.answer [0];
            _view._bText.text = q.answer [1];
            _view._cText.text = q.answer [2];
            _view._dText.text = q.answer [3];
        };
			
        show.addAction ("event_A", delegate {
            result (0);
            return "result";
        });
        show.addAction ("event_B", delegate {
            result (1);
            return "result";
        });
        show.addAction ("event_C", delegate {
            result (2);
            return "result";
        });
        show.addAction ("event_D", delegate {
            result (3);
            return "result";
        });
			
			
        return show;
    }

		
    private State resultState ()
    {
        State result = new State ("result");
        result.onStart += delegate {
				
			Question q = _questions[_index] as Question;
            if (_correct) {
                ++_score;
                _view._resultText.text = q.right;
                _view._result.SetActive (true);
            } else {
					
                _view._resultText.text = q.wrong;
                _view._result.SetActive (true);
            }
        };
		result.onFinish += delegate {
            _view._result.SetActive (false);
        };
			
        result.addAction ("event_OK", delegate {
            _index++;
			if (_index >= _questions.Count) {
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
		_questions = new Config().questions;
		foreach(Question q in _questions) 
		{
			Debug.Log(q.question);
		}
		
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
