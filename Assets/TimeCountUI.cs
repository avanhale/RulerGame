using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCountUI : MonoBehaviour
{
    TextMeshProUGUI timeText;
	Image timeFillImage;

	public Color startColor, endColor;

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
		float timeLerp = (float)time / (float)totalTime;
		timeFillImage.fillAmount = timeLerp;
		LerpColor(timeLerp);
	}


	void LerpColor(float timeLerp)
	{
		//float h = Mathf.Lerp(120, 0, timeLerp);
		//Color newColor = Color.HSVToRGB(h, 100, 100);
		Color lerpColor = Color.Lerp(endColor, startColor, timeLerp);
		Color.RGBToHSV(lerpColor, out var h, out var s, out var v);
		s = v = 1;
		Color newColor = Color.HSVToRGB(h, s, v);
		timeFillImage.color = newColor;
	}
}
