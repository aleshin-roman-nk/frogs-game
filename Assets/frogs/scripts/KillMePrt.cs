using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMePrt : MonoBehaviour
{
	public float Delay = 1f;

	private IEnumerator Start()
	{
		if (Delay != 0) yield return new WaitForSeconds(Delay);
		Destroy(gameObject);
	}
}
