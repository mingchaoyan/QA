using UnityEngine;
using System.Collections;
using GDGeek;

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
        StateWithEventMap begin = new StateWithEventMap ();
        begin.onStart += delegate {
            _index = 0;
            _view._begin.SetActive (true);
        };
        begin.onOver += delegate {
            _view._begin.SetActive (false);
        };
			
        begin.addAction ("OK", delegate {
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
        StateWithEventMap show = new StateWithEventMap ();
        show.onStart += delegate {
			Question q = _questions [_index] as Question;
            _view._questionText.text = q.question;
            _view._aText.text = q.answer [0];
            _view._bText.text = q.answer [1];
            _view._cText.text = q.answer [2];
            _view._dText.text = q.answer [3];
        };
			
        show.addAction ("A", delegate {
            result (0);
            return "result";
        });
        show.addAction ("B", delegate {
            result (1);
            return "result";
        });
        show.addAction ("C", delegate {
            result (2);
            return "result";
        });
        show.addAction ("D", delegate {
            result (3);
            return "result";
        });
			
			
        return show;
    }

		
    private State resultState ()
    {
        StateWithEventMap result = new StateWithEventMap ();
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
        result.onOver += delegate {
            _view._result.SetActive (false);
        };
			
        result.addAction ("OK", delegate {
            _index++;
			if (_index >= _questions.Count) {
                return "over";
            } else {
                return "show";
            }
				
        });
        return result;
    }

    private State overState ()
    {
        StateWithEventMap over = new StateWithEventMap ();
			
        over.onStart += delegate {
            _view._scoreText.text = _score.ToString ();
            _view._end.SetActive (true);
        };
        over.onOver += delegate {
				
            this._index = 0;
            _score = 0;
            _view._end.SetActive (false);
        };
			
        over.addAction ("OK", delegate {
            return "begin";
        });
        return over;
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
        _fsm.addState ("over", overState ());
        _fsm.init ("begin");
    }
}
