using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyBoxloopMover : MonoBehaviour
{
	public float scrollSpeed = 0.1f;
	private float offset;

	void Update()
	{
		// Increment the offset based on time and scroll speed
		offset += Time.deltaTime * scrollSpeed;

		// Loop the offset value
		if (offset > 1.0f)
		{
			offset -= 1.0f;
		}

		// Set the _Rotation property of the skybox material
		RenderSettings.skybox.SetFloat("_Rotation", offset * 360f);  // Multiply by 360 to rotate a full circle over one loop
	}
}
