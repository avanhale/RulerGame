using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	public TextMeshProUGUI targetMeasureText;
	public TextMeshProUGUI scoreText;
	public Transform strikesT;
	Image[] strikes;
	LevelsUI levelsUI;

	private void Awake()
	{
		levelsUI = GetComponentInChildren<LevelsUI>();
		GetStrikes();
		ResetStrikes();
		SetLevel(0);
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

}
