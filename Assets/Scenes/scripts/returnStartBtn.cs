using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.SceneManagement;

public class returnStartBtn : MonoBehaviour
{
	TapGesture _tapGesture;

	// Start is called before the first frame update
	void Start()
    {
		_tapGesture = GetComponent<TapGesture>();

		_tapGesture.Tapped += _tapGesture_Tapped;
	}

	private void _tapGesture_Tapped(object sender, System.EventArgs e)
	{
		SceneManager.LoadScene("start");
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
