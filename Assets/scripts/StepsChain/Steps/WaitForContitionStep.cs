using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForContitionStep : StaticStepBase
{
	public Action Do { get; set; }

	public Func<bool> While { get; set; }

	public WaitForContitionStep(string name): base(name)
	{

	}

	public WaitForContitionStep()
	{

	}

	public override bool update(ChainRunner prg)
	{
		if(While == null)
		{
			return false;
		}

		if (!While.Invoke())
		{
			return false;
		}

		Do?.Invoke();
		return true;
	}
}
