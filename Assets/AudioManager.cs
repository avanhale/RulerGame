using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;
	public AudioSource correctSource;
	public AudioSource incorrectSource;
	public AudioSource measureTickSource;
	public AudioSource levelUpSource;
	public AudioSource clockTickingSource;
	public AudioSource gameOverSource;
	public AudioSource newMeasureSource;

	private void Awake()
	{
		instance = this;
	}

	public void Correct()
	{
		correctSource.Play();
	}

	public void Incorrect()
	{
		incorrectSource.Play();
	}

	public void MeasureTick()
	{
		if (!measureTickSource.isPlaying) measureTickSource.Play();
	}

	public void LevelUp()
	{
		levelUpSource.Play();
	}

	public void ClockTicking()
	{
		clockTickingSource.Play();
	}
	public void StopClockTicking()
	{
		clockTickingSource.Stop();
	}


	public void GameOver()
	{
		gameOverSource.PlayDelayed(1);
	}


	public void NewMeasure()
	{
		newMeasureSource.Play();
	}



}
