using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MotionStepBase: IStep
{
	public string name {  get; private set; }

	public MotionStepBase()
	{
		
	}

	public MotionStepBase(string name)
	{
		this.name = name;
	}
	public Transform movableObject { get; set; }
	public Vector3 targetPoint { get; set; }
	public Action<MotionStepBase> OnStartMotion { get; set; }
	public Action<MotionStepBase> OnEndMotion { get; set; }
	public abstract bool update(ChainRunner prg);
}
