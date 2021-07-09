using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelsUI : MonoBehaviour
{
	public Color[] levelColors;
	public Transform levelsT;
	Image[] levels;
	TextMeshProUGUI levelText;

	public int targetLevel;

	private void Awake()
	{
		levelText = GetComponentInChildren<TextMeshProUGUI>();
		GetLevels();
	}

	[ContextMenu("UpdateLevel")]
	public void UpdateLevel()
	{
		SetLevel(targetLevel);
	}


	public void SetLevel(int level)
	{
		UpdateColors(level);
		levelText.text = level.ToString();
	}

	void UpdateColors(int level)
	{
		for (int i = 0; i < levels.Length; i++)
		{
			Color targetColor = Color.clear;
			int colorIndex = level - 1;
			int modifier = i == 0 ? -1 : i == 1 ? 0 : 1;
			int colorIndexModifier = colorIndex + modifier;
			if (Mathf.Clamp(colorIndexModifier, 0, levelColors.Length-1) == colorIndexModifier) targetColor = levelColors[colorIndexModifier];
			levels[i].color = targetColor;
		}
	}


	void GetLevels()
	{
		levels = levelsT.GetComponentsInChildren<Image>();
	}


}
