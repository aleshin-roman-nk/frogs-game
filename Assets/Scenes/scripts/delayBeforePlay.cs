using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class delayBeforePlay : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI countdownText;
	[SerializeField] int countdownTime = 5;

	private float seconds;

	private bool _youCanGo = false;

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(StartCountdown());
	}

	private IEnumerator StartCountdown()
	{
		float remainingTime = countdownTime;

		while (remainingTime > 0)
		{
			countdownText.text = remainingTime.ToString("0");
			yield return new WaitForSeconds(1f);
			remainingTime--;
		}

		countdownText.text = "Go!";
		yield return new WaitForSeconds(1f);
		countdownText.gameObject.SetActive(false);

		// Add any code here that should run after the countdown ends
		_youCanGo = true;
	}

	public bool youCanGo()
	{
		return _youCanGo;
	}
}
