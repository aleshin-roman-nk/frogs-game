using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowButtonsToClick : MonoBehaviour
{
	public bool allowed {  get; private set; }

	private float ellapsedTime = 0.0f;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		ellapsedTime += Time.deltaTime;

		if (ellapsedTime >= 5)
		{
			allowed = true;
			ellapsedTime = 0.0f;
		}
	}
}
