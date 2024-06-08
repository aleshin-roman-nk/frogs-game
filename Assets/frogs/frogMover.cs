using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frogMover : MonoBehaviour
{
	private Animator animator;

	public Transform newPosPoint;

	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
		animator.SetInteger("AnimationID", 13);
	}

	bool p = false;
	// Update is called once per frame
	void Update()
	{
		// Example to trigger the jump animation
		if (Input.GetKeyDown(KeyCode.Space))
		{
			animator.SetInteger("AnimationID", 15);

			p = true;
		}

		// Check if the jump animation has finished and transition back to idle
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("long_jump_root_motion") &&
			animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && p)
		{
			animator.SetInteger("AnimationID", 13); // Reset the parameter to transition to idle

			transform.position = newPosPoint.position;

			p = false;
		}
	}

	//void OnAnimatorMove()
	//{
	//	if (animator)
	//	{
	//		// Apply the root motion to the position and rotation of the GameObject
	//		transform.position += animator.deltaPosition;
	//		transform.rotation *= animator.deltaRotation;
	//	}
	//}
}
