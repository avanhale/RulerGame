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

	public int targetMeasureIndex;

	public int numberStrikes;
	public int maxNumberStrikes;

	public static bool canSelectMeasurement;

	public int currentScore;
	public int scoreIncrement;

	public int targetLevelScore;
	public int currentLevelScore;

	public int currentLevel;


	[System.Serializable]
	public class GameSettings
	{
		public Rules.GameType gameType;
		public int rulerLength;

		public void SetGameType(Rules.GameType _gameType)
		{
			gameType = _gameType;
		}

		public void SetRulerLength(int _rulerLength)
		{
			rulerLength = _rulerLength;
			RulerUI.instance.SetInchBlocks(rulerLength);
		}
	}

	public GameSettings gameSettings;
	public Levels levels;

	public float levelBreakTime;
	public float incorrectBreakTime;



	public delegate void GameStarted();
	public static event GameStarted OnGameStarted;

	private void Awake()
	{
		instance = this;
	}

	public void StartGame()
	{
		StartNewMeasurement();
		SetStrikes(0);
		SetScore(0);
		SetLevel(1);
		OnGameStarted?.Invoke();
	}

	void StartNewMeasurement()
	{
		StartTimeRoutine();
		canSelectMeasurement = true;
		IncorrectPost.instance.DeactivateMarker();
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
		Incorrect(0, 0);
	}

	public void SelectMeasurement(int measurementIndex, float currentMeasurementDistance)
	{
		if (measurementIndex == targetMeasureIndex)
		{
			Correct();
		}
		else
		{
			Incorrect(measurementIndex, currentMeasurementDistance);
		}
	}

	void Correct()
	{
		audioManager.Correct();
		SetScore(currentScore + scoreIncrement);
		bool increasedLevel = TryIncreaseLevel(scoreIncrement);
		if (increasedLevel)
		{
			StopTimeRoutine();
			Invoke("StartNewMeasurement", levelBreakTime);
		}
		else
		{
			StartNewMeasurement();
		}
	}


	void Incorrect(int measurementIndex, float measurementDistance)
	{
		audioManager.Incorrect();
		IncorrectPost.instance.ActivateMarker(measurementIndex, measurementDistance);
		SetStrikes(numberStrikes + 1);
		if (numberStrikes < maxNumberStrikes)
		{
			StopTimeRoutine();
			canSelectMeasurement = false;
			Invoke("StartNewMeasurement", incorrectBreakTime);
		}
		else
		{
			EndGame();
		}
	}


	bool TryIncreaseLevel(int scoreIncrement)
	{
		currentLevelScore += scoreIncrement;
		if (currentLevelScore >= targetLevelScore)
		{
			SetLevel(currentLevel + 1);
			currentLevelScore = 0;
			return true;
		}
		return false;
	}

	void SetLevel(int level)
	{
		currentLevel = level;
		uiManager.SetLevel(currentLevel);
		audioManager.LevelUp();

		Levels.Level newLevel = levels.GetLevel(level);
		startTimeCount = newLevel.countdownTime;
		scoreIncrement = newLevel.scoreIncrement;
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
		int randomMeasureIndex = 0;
		for (int i = 0; i < 5; i++)
		{
			randomMeasureIndex = Random.Range(1, Rules.MaxNumberOfMarks(gameSettings.gameType, gameSettings.rulerLength)+1);
			if (randomMeasureIndex != targetMeasureIndex) break;
		}
		return randomMeasureIndex;
	}


	void UpdateTargetMeasureText()
	{
		uiManager.SetTargetMeasure(targetMeasureIndex);
	}

}
