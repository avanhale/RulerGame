using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCountUI : MonoBehaviour
{
    TextMeshProUGUI timeText;
	Image timeFillImage;

	private void Awake()
	{
        timeText = GetComponentInChildren<TextMeshProUGUI>();
		timeFillImage = GetComponentInChildren<Image>();
	}

	public void UpdateTime()
	{
		float time = GameManager.instance.timeCount;
		int totalTime = GameManager.instance.startTimeCount;

		timeText.text = Mathf.CeilToInt(time).ToString();
		timeFillImage.fillAmount = (float)((float)time / (float)totalTime);
	}
}
