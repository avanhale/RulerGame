using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IncorrectPost : MonoBehaviour
{
	public static IncorrectPost instance;
	RectTransform myRT;
	TextMeshProUGUI measureText;

	private void Awake()
	{
		instance = this;
		myRT = GetComponent<RectTransform>();
		measureText = GetComponentInChildren<TextMeshProUGUI>();
	}

	private void Start()
	{
		DeactivateMarker();
	}


	public void ActivateMarker(int measureIndex,float xPos)
	{
		Vector2 pos = myRT.anchoredPosition;
		pos.x = xPos;
		myRT.anchoredPosition = pos;

		UpdateMeasureText(measureIndex);
		gameObject.SetActive(true);
	}

	public void DeactivateMarker()
	{
		gameObject.SetActive(false);
	}


	void UpdateMeasureText(int measureIndex)
	{
		string measureString = MeasureIndexSplitter.ConvertIndex(measureIndex);
		measureText.text = measureString;
	}


}
