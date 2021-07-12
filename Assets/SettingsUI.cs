using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class SettingsUI : MonoBehaviour
{

	public RectTransform settingsPanelRT;
	public float panelOpenHeight;
	public float panelOpenTime;
	public bool isOpen;

	public TMP_Dropdown measureDropdown, lengthDropdown;

	private void Awake()
	{
		settingsPanelRT.sizeDelta = new Vector2(settingsPanelRT.sizeDelta.x, 0);
	}

	private void Start()
	{
		FillAllSettings();
	}

	public void ToggleSettings()
	{
		isOpen = !isOpen;
		OpenSettings(isOpen);
	}

	Tween openTween;
	public void OpenSettings(bool open = true)
	{
		openTween.Kill();
		Vector2 endHeight = new Vector2(settingsPanelRT.sizeDelta.x, open ? panelOpenHeight : 0);
		openTween = settingsPanelRT.DOSizeDelta(endHeight, panelOpenTime);
		isOpen = open;
	}




	public void Dropdown_Measure(int index)
	{
		Rules.GameType gameType = Rules.GetGameType(index + 1);
		GameManager.instance.gameSettings.SetGameType(gameType);
		measureDropdown.SetValueWithoutNotify(index);
		PlayerPrefsRG.GameType(index);
	}

	public void Dropdown_Length(int index)
	{
		int rulerLength = index + 1;
		GameManager.instance.gameSettings.SetRulerLength(rulerLength);
		lengthDropdown.SetValueWithoutNotify(index);
		PlayerPrefsRG.RulerLength(index);
	}




	void FillAllSettings()
	{
		// Game Type
		int gameType = 0;
		if (PlayerPrefsRG.GameType(ref gameType))
		{
			Dropdown_Measure(gameType);
		}

		// Ruler Length
		int rulerLength = 0;
		if (PlayerPrefsRG.RulerLength(ref rulerLength))
		{
			Dropdown_Length(rulerLength);
		}
	}




}
