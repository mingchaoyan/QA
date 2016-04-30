using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Utils
{
	public class State
	{
		private string _name  = "";
		public string Name{
			get{
				return _name;
			}
			set{
				_name = value;
			}
		}
		public State(string name) {
			_name = name;
		}
		public delegate void Action();
		public delegate string EvtAction();
		
		private Dictionary<string, EvtAction> _eventAction = new Dictionary<string, EvtAction>();
		public Dictionary<string, EvtAction> EventAction{
			get{
				return _eventAction;
			}
		}
		public event Action onStart = null;
		public event Action onFinish = null;
		
		public void addAction(string evtString, EvtAction action) {
			_eventAction.Add(evtString, action);
		}
		
		public void start() {
			if(onStart != null)
				onStart();
		}
		
		public void finish() {
			if(onFinish != null)
				onFinish();
		}

	}
}
