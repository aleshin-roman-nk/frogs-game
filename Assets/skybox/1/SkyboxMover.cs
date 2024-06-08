using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxMover : MonoBehaviour
{
	public float scrollSpeed = 0.5f;
	private float offset;

	void Update()
	{
		offset += Time.deltaTime * scrollSpeed;
		//if (offset > 1.0f) offset -= 1.0f;

		RenderSettings.skybox.SetFloat("_TexCoord", offset);
	}
}
