using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Utils
{
	public class FSM
	{
		private Dictionary<string, State> states = new Dictionary<string, State>();
		private State curState = null;
		
		public void addState(string stateName, State state) {
			states.Add(stateName, state);
		}
		
		public void translate(string stateName) {
			if(stateName == curState.Name)
			{
				curState.finish();
				curState.start();
			}
			else
			{
				if (curState != null)
				{
					curState.finish();
				}
				curState = states[stateName];
				curState.start();
			}
		}
		
		public void init(string stateName)
		{
			curState = states[stateName];
			translate(stateName);
		}
		
		public void post(string evtString) {
			string stateName = curState.EventAction[evtString]();
			translate(stateName);
		}
	}
}
