using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public AudioManager audioManager;
	public UIManager uiManager;
	public TextMeshProUGUI targetMeasureText;
	public TimeCountUI timeCountUI;
	public int startTimeCount;

	public float timeCount;

	public int currentNumberInchBlocks;
	public int targetMeasureIndex;

	public int numberStrikes;
	public int maxNumberStrikes;

	public static bool canSelectMeasurement;

	public int currentScore;
	public int scoreIncrement;

	public int targetLevelScore;
	public int currentLevelScore;

	public int currentLevel;

	private void Awake()
	{
		instance = this;
	}

	public void StartGame()
	{
		StartNewMeasurement();
		SetStrikes(0);
		SetScore(0);
	}

	void StartNewMeasurement()
	{
		StartTimeRoutine();
		canSelectMeasurement = true;
	}

	void StartTimeRoutine()
	{
		StopTimeRoutine();
		timeCountRoutine = StartCoroutine(TimeCountRoutine());
	}

	void StopTimeRoutine()
	{
		if (timeCountRoutine != null) StopCoroutine(timeCountRoutine);
	}

	Coroutine timeCountRoutine;
	IEnumerator TimeCountRoutine()
	{
		timeCount = startTimeCount;
		RandomizeTargetMeasure();
		while (timeCount > 0)
		{
			yield return null;
			timeCount -= Time.deltaTime;
			timeCountUI.UpdateTime();
		}
		TimeRanOut();
	}

	void TimeRanOut()
	{
		Incorrect();
	}

	public void SelectMeasurement(int measurementIndex)
	{
		if (measurementIndex == targetMeasureIndex)
		{
			Correct();
		}
		else
		{
			Incorrect();
		}
	}

	void Correct()
	{
		audioManager.Correct();
		StartNewMeasurement();
		SetScore(currentScore + scoreIncrement);
		TryIncreaseLevel(scoreIncrement);
	}


	void Incorrect()
	{
		audioManager.Incorrect();
		SetStrikes(numberStrikes + 1);
		if (numberStrikes < maxNumberStrikes)
		{
			StartNewMeasurement();
		}
		else
		{
			EndGame();
		}
	}


	void TryIncreaseLevel(int scoreIncrement)
	{
		currentLevelScore += scoreIncrement;
		if (currentLevelScore >= targetLevelScore)
		{
			SetLevel(currentLevel + 1);
			currentLevelScore = 0;
		}
	}

	void SetLevel(int level)
	{
		currentLevel = level;
		uiManager.SetLevel(currentLevel);
	}

	void EndGame()
	{
		canSelectMeasurement = false;
		StopTimeRoutine();
	}

	void SetStrikes(int strikes)
	{
		numberStrikes = strikes;
		uiManager.SetStrikes(numberStrikes);
	}

	void SetScore(int score)
	{
		currentScore = score;
		uiManager.SetScore(currentScore);
	}


	void RandomizeTargetMeasure()
	{
		targetMeasureIndex = GetRandomMeasureIndex();
		UpdateTargetMeasureText();
	}


	int GetRandomMeasureIndex()
	{
		return Random.Range(0, currentNumberInchBlocks * Rules.NUM_SIXTEENTHS_PER_INCH);
	}


	void UpdateTargetMeasureText()
	{
		uiManager.SetTargetMeasure(targetMeasureIndex);
	}

}
