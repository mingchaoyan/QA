using UnityEngine;
using System.Collections;

public class Question {
	public string question;
	public string[] answers;
	public int correct;
	public string rightInfo;
	public string wrongInfo;

	public Question (string q, string[] a, int c, string r, string w)
	{
		question = q;
		answers = a;
		correct = c;
		rightInfo = r;
		wrongInfo = w;
	}
};

