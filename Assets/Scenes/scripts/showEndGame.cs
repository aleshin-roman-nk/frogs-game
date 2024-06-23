using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class showEndGame : MonoBehaviour
{
	[SerializeField] private Image Image;

	[SerializeField] private GameObject[] objectsToAppearOnEnd;
	[SerializeField] private GameObject[] objectsToDisappearOnEnd;
	[SerializeField] private TextMeshProUGUI result;

	private ChainRunner chainRunner;

	// Start is called before the first frame update
	void Start()
	{
		chainRunner = new ChainRunner();

		chainRunner.AddStep(new OperationStep
		{
			Do = () =>
			{
				hideObjectsToDisappear();
				Image.gameObject.SetActive(true);
			}
		});

		chainRunner.AddStep(new AnimateFloat
		{
			duration = 0.8f,
			valueA = 0.0f,
			valueB = 0.9f,
			OnAnimationStep = (x) =>
			{
				Color newColor = Image.color;
				newColor.a = x;
				Image.color = newColor;
			}
		});

		chainRunner.AddStep(new OperationStep
		{
			Do = () =>
			{
				result.text = $"{caughtFrogs}/{totalFrogs}";
				unhideObjectsToAppear();
			}
		});
	}

	bool go = false;

	// Update is called once per frame
	void Update()
	{
		if(go)
			chainRunner.Update();
	}

	private int caughtFrogs = 0;
	private int totalFrogs = 0;

	public void endGame(int caughtfrogs, int totalfrogs)
	{
		caughtFrogs = caughtfrogs;
		totalFrogs = totalfrogs;
		go = true;
	}

	private void unhideObjectsToAppear()
	{
		foreach (var item in objectsToAppearOnEnd)
		{
			item.SetActive(true);
		}
	}

	private void hideObjectsToDisappear()
	{
		foreach (var item in objectsToDisappearOnEnd)
		{
			item.SetActive(false);
		}
	}
}
