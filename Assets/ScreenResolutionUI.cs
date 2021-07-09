using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenResolutionUI : MonoBehaviour
{
	TextMeshProUGUI resolutionText;
	public bool curRez;
	public bool physical;

	private void Awake()
	{
		resolutionText = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		resolutionText.text = string.Format("{0}\nx\n{1}", Screen.width, Screen.height);
		if (curRez) resolutionText.text = string.Format("{0}\nx\n{1}", Screen.currentResolution.width, Screen.currentResolution.height);
		if (physical) resolutionText.text = string.Format("{0}\nx\n{1}", Screen.width/Screen.dpi, Screen.currentResolution.height/Screen.dpi);
	}

}
