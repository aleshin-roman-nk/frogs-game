using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainRunner
{
	private List<IStep> moveSteps = new List<IStep>();

	private int currentPointer = 0;

	private bool _run = true;

	public void AddStep(IStep step)
	{
		moveSteps.Add(step);
	}

	public void GoTo(string stepName)
	{
		var i = moveSteps.FindIndex(x => !string.IsNullOrEmpty(x.name) && x.name.Equals(stepName));
		if(i >= 0) currentPointer = i;
	}

	public void Stop()
	{
		_run = false;
	}

	public bool Update()
	{
		if(!_run) return false;

		if (currentPointer < moveSteps.Count)
		{
			if (!moveSteps[currentPointer].update(this))
				currentPointer++;

			return true;
		}
		else 
			return false;
	}
}
