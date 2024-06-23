using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RestObjectManager : MonoBehaviour
{
	[SerializeField] FrogRestPoint[] restPoints;

	// how many frogs allowed to play, the rest are waiting
	public int maxParticipantsCount = 5;
	private int participantsCount = 0;

	public void Start()
	{
		if (restPoints.Length < maxParticipantsCount) maxParticipantsCount = restPoints.Length;
	}

	//public FrogRestPoint provideRestObjectExcept(string objectName)
	public FrogRestPoint provideRestObjectExcept(int objectName)
	{
		//if (participantsCount >= maxParticipantsCount) return null;

		//var rests = restPoints.Where(p => !p.isBusy && !p.name.Equals(objectName)).ToArray();
		var rests = restPoints.Where(p => !p.isBusy && p.GetInstanceID() != objectName).ToArray();

		if (rests.Length == 0) return null;
		if (rests.Length == 1) { rests[0].isBusy = true; return rests[0]; }

		var res = rests[UnityEngine.Random.Range(0, rests.Length)];
		res.isBusy = true;

		return res;
	}

	public FrogRestPoint enterCity()
	{
		if (participantsCount >= maxParticipantsCount) return null;

		participantsCount++;

		var rests = restPoints.Where(p => !p.isBusy).ToArray();

		if (rests.Length == 0) return null;
		if (rests.Length == 1) { rests[0].isBusy = true; return rests[0]; }

		var res = rests[UnityEngine.Random.Range(0, rests.Length)];
		res.isBusy = true;

		return res;
	}

	public void leaveCity()
	{
		participantsCount--;
	}
}
