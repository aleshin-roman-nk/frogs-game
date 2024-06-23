using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LinearMoveXZStep : MotionStepBase
{
	public float speed = 1.0f;

	private bool isMoving = false;

	public LinearMoveXZStep(string name): base(name)
	{

	}

	public LinearMoveXZStep()
	{

	}

	public override bool update(ChainRunner prg)
	{
		if (!isMoving)
		{
			isMoving = true;
			OnStartMotion?.Invoke(this);
		}

		// Move the object towards the target position if it's moving
		if (isMoving)
		{
			// Calculate direction and move the object
			Vector3 direction = (targetPoint - movableObject.position).normalized;
			direction.y = 0; // Keep movement in the XZ plane

			movableObject.position = Vector3.MoveTowards(movableObject.position, new Vector3(targetPoint.x, movableObject.position.y, targetPoint.z), speed * Time.deltaTime);

			// Make the object look towards the target
			if (direction != Vector3.zero) // Prevent errors when direction is zero
			{
				movableObject.rotation = Quaternion.LookRotation(direction);
			}
			var dist = Vector2.Distance(new Vector2(movableObject.position.x, movableObject.position.z), new Vector2(targetPoint.x, targetPoint.z));

			// Check if the object has reached the target position
			//if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
			if (dist < 0.1f)
			{
				isMoving = false;
				OnEndMotion?.Invoke(this);
				return false;
			}
		}

		return true;
	}

	public void Stop()
	{
		throw new NotImplementedException();
	}
}
