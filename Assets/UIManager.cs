using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
	public TextMeshProUGUI targetMeasureText;
	public TextMeshProUGUI gameOverText;
	public TextMeshProUGUI scoreText;
	public Transform strikesT;
	SettingsUI settingsUI;
	Image[] strikes;
	LevelsUI levelsUI;

	public Button playButton, settingsButton;
	public CanvasGroup buttonCanvasGroup;
	public float buttonsFadeTime;

	private void Awake()
	{
		levelsUI = GetComponentInChildren<LevelsUI>();
		settingsUI = GetComponentInChildren<SettingsUI>();
		GetStrikes();
		ResetStrikes();
		SetScore(0);
	}

	private void Start()
	{
		SetLevel(0);
		ActivateButtonPanel();
	}

	public void SetTargetMeasure(int targetMeasure)
	{
		string targetMeasureString = MeasureIndexSplitter.ConvertIndex(targetMeasure);
		targetMeasureText.text = targetMeasureString;
	}

	public void SetScore(int score)
	{
		scoreText.text = score.ToString();
	}

	public void ResetStrikes()
	{
		SetStrikes(0);
	}

	public void SetStrikes(int strikesCount)
	{
		for (int i = 0; i < strikes.Length; i++)
		{
			if (i < strikesCount) strikes[i].enabled = true;
			else strikes[i].enabled = false;
		}
	}

	public void SetLevel(int level)
	{
		levelsUI.SetLevel(level);
	}



	void GetStrikes()
	{
		strikes = strikesT.GetComponentsInChildren<Image>();
	}


	public void CloseSettingsMenu()
	{
		settingsUI.OpenSettings(false);
	}



	public void Button_PlayGame()
	{
		GameManager.instance.StartGame();
		CloseSettingsMenu();
	}


	Tween buttonsFadeTween;
	public void ActivateButtonPanel(bool activate = true)
	{
		targetMeasureText.enabled = !activate;
		if (activate) gameOverText.enabled = false;
		playButton.enabled = settingsButton.enabled = activate;
		buttonsFadeTween.Kill();
		buttonsFadeTween = DOTween.To(() => buttonCanvasGroup.alpha, x => buttonCanvasGroup.alpha = x, activate ? 1 : 0, buttonsFadeTime);
	}

	public void GameOver()
	{
		StartCoroutine(GameOverRoutine());
	}

	IEnumerator GameOverRoutine()
	{
		yield return new WaitForSeconds(1);
		targetMeasureText.enabled = false;
		gameOverText.enabled = true;
		Color c = gameOverText.color;
		c.a = 0;
		gameOverText.color = c;
		gameOverText.DOFade(1, 1);
		yield return new WaitForSeconds(3);
		ActivateButtonPanel();
	}


}
