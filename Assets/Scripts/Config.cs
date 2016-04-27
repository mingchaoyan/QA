using UnityEngine;
using System.Collections;

public class Question {
	public string question;
	public string[] answer;
	public int correct;
	public string right;
	public string wrong;

	public Question (string q, string[] a, int c, string r, string w)
	{
		question = q;
		answer = a;
		correct = c;
		right = r;
		wrong = w;
	}
};

public class Config {	
	
	public ArrayList questions = new ArrayList();
	public Config() {
		questions.Add(new Question("aa", new string[]{"1", "2", "3", "4"}, 0, "right", "wrong"));
		questions.Add(new Question("bb", new string[]{"11", "22", "33", "44"}, 0, "right", "wrong"));
		
	}
}
