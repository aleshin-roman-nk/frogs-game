using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frogMover : MonoBehaviour
{
	private Animator animator;

	public float speed = 1f; // Speed of movement
	private Vector3 targetPosition; // Target position to move towards
	private bool isMoving = false; // Flag to check if the object should be moving

	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
	}

	bool p = false;
	// Update is called once per frame
	void Update()
	{
		// Example to trigger the jump animation
		if (Input.GetKeyDown(KeyCode.Space))
		{
			animator.SetTrigger("jump");

			p = true;
		}

		// Check if the jump animation has finished and transition back to idle
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("long_jump_root_no_move_motion") &&
			animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f && p)
		{
			p = false;
		}

		moveFrog();
	}

	private void moveFrog()
	{
		// Detect left mouse click
		if (Input.GetMouseButtonDown(0))
		{
			// Perform raycast from mouse position
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				// Check if the raycast hits the plane (assuming the plane has a collider)
				if (hit.collider != null)
				{
					// Set target position and start moving
					targetPosition = hit.point;
					isMoving = true;
					animator.SetTrigger("walk");
				}
			}
		}

		// Move the object towards the target position if it's moving
		if (isMoving)
		{
			// Calculate direction and move the object
			Vector3 direction = (targetPosition - transform.position).normalized;
			direction.y = 0; // Keep movement in the XZ plane

			transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z), speed * Time.deltaTime);

			// Make the object look towards the target
			if (direction != Vector3.zero) // Prevent errors when direction is zero
			{
				//Quaternion lookRotation = Quaternion.LookRotation(direction);
				//transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);

				transform.rotation = Quaternion.LookRotation(direction);
			}

			// Check if the object has reached the target position
			if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
			{
				isMoving = false;
				animator.ResetTrigger("walk");
				
			}
		}
	}

	private void jumpFrog()
	{

	}
}
