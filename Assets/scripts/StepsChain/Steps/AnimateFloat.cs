using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateFloat: StaticStepBase
{
	public float valueA;
	public float valueB;
	public float duration;

	public Action<float> OnAnimationStep;

	private float elapsedTime = 0f;
	private bool isAnimating = false;
	private float currentValue;

	// такое можно и в корутине... внутри метода прям, цикл пройдет до того, как update вернет false
	// и можно дальше показать ui элементы
	// или можно так же объединить в программу
	// ведь программа тоже работает через prg.update(), при завершении всех шагов возвращается false
	// 17-06-2024
	// шаг 1 : анимация проявления
	// шаг 2 : включить текст
	public override bool update(ChainRunner prg)
	{
		if (!isAnimating)
		{
			elapsedTime = 0;
			isAnimating = true;
		}

		if (isAnimating)
		{
			elapsedTime += Time.deltaTime;
			float t = Mathf.Clamp01(elapsedTime / duration);
			currentValue = Mathf.Lerp(valueA, valueB, t);

			OnAnimationStep?.Invoke(currentValue);

			if (t >= 1f)
			{
				isAnimating = false;
				return false;
			}
		}

		return true;
	}
}
