//using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class randomListPlayer : MonoBehaviour
{
	[SerializeField] private AudioSource[] sounds;

	private AudioSource[] _playlist;

	int currentIndex = 0;

	AudioSource[] generatePlaylist(AudioSource[] originalSonglist)
	{
		AudioSource[] newArray = new AudioSource[originalSonglist.Length * 5];

		AudioSource lastElement = originalSonglist[0];
		AudioSource secondLastElement = originalSonglist[0];

		for (int i = 0; i < newArray.Length; i++)
		{
			AudioSource element;

			// Randomly select an element from the original array
			do
			{
				element = originalSonglist[Random.Range(0, originalSonglist.Length)];
			}
			// Ensure that the same element is not repeated more than twice in a row
			while (element == lastElement && element == secondLastElement);

			newArray[i] = element;
			secondLastElement = lastElement;
			lastElement = element;
		}

		return newArray;
	}

	private T[] generatePlaylist<T>(T[] originalArray, int maxRepetition, int playListLength)
	{
		if (maxRepetition < 1)
		{
			Debug.LogError("maxRepetition must be at least 1.");
		}

		T[] newArray = new T[originalArray.Length * playListLength];

		if (newArray.Length == 0) return newArray;

		Queue<T> lastElements = new Queue<T>(maxRepetition);

		for (int i = 0; i < newArray.Length; i++)
		{
			T element;

			// Randomly select an element from the original array
			do
			{
				element = originalArray[Random.Range(0, originalArray.Length)];
			}
			// Ensure that the same element is not repeated more than maxRepetition times in a row
			while (lastElements.Contains(element) && lastElements.Count >= maxRepetition);

			newArray[i] = element;

			// Update the queue with the latest element
			if (lastElements.Count == maxRepetition)
			{
				lastElements.Dequeue();
			}
			lastElements.Enqueue(element);
		}

		return newArray;
	}

	private void Start()
	{
		//_playlist = generatePlaylist(sounds);
		_playlist = generatePlaylist(sounds, 1, 15);

		//var str = string.Join(", ", _playlist.Select(x => x.name));
		//Debug.Log(str);
	}

	public void Play()
	{
		if(_playlist.Length == 0) return;

		if(currentIndex >= _playlist.Length) currentIndex = 0;

		_playlist[currentIndex].Play();

		currentIndex++;
	}
}
