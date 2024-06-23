using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeLevelBtn : MonoBehaviour
{
	TapGesture _tapGesture;

	[SerializeField] public AllowButtonsToClick allowerToClick;

	// Start is called before the first frame update
	void Start()
	{
		_tapGesture = GetComponent<TapGesture>();

		_tapGesture.Tapped += _tapGesture_Tapped;
	}

	private void _tapGesture_Tapped(object sender, System.EventArgs e)
	{
		if (allowerToClick.allowed)
		{
			if (name.Equals("lvl1")) SceneManager.LoadScene("Level1");
			else if (name.Equals("lvl2")) SceneManager.LoadScene("Level2");
			else if (name.Equals("lvl3")) SceneManager.LoadScene("Level3");
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
