using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class netHit : MonoBehaviour
{
	[SerializeField] private GameObject hitEffect;

	[SerializeField] private Animator animator;

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(WaitForAnimationToEnd());
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private IEnumerator WaitForAnimationToEnd()
	{
		// Wait for the Animator to start the animation
		while (!animator.GetCurrentAnimatorStateInfo(0).IsName("hit"))
		{
			yield return null;
		}

		// Wait until the animation has finished
		while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
		{
			yield return null;
		}

		// Instantiate the object after the animation has finished
		Instantiate(hitEffect, transform.position, Quaternion.identity);

		Destroy(gameObject, 0.4f);
	}
}
