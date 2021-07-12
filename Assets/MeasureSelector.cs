using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MeasureSelector : MonoBehaviour
{
	public RectTransform measureRT, cursorRT;
	Image measureImage;
	public TextMeshProUGUI measureText;

	public Vector3 mousePosition;
	public Vector2 rulerPos;
	public int currentMeasureIndex;

	public int maxNumMeasureIndexes;
	public float measurementDistance;

	public int inchIndex;
	public int microMeasureIndex;

	private void Awake()
	{
		measureImage = measureRT.GetComponent<Image>();
	}

	private void OnEnable()
	{
		RulerUI.OnHover += RulerPointerHover_OnHover;
		GameManager.OnGameStarted += GameManager_OnGameStarted;
	}
	private void OnDisable()
	{
		RulerUI.OnHover -= RulerPointerHover_OnHover;
		GameManager.OnGameStarted -= GameManager_OnGameStarted;
	}

	private void RulerPointerHover_OnHover(bool isHovering)
	{
		measureImage.enabled = isHovering;
	}
	private void GameManager_OnGameStarted()
	{
		UpdateMeasurementLengths();
	}

	private void Update()
	{
		mousePosition = Input.mousePosition;

		if (GameManager.canSelectMeasurement)
		{
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(measureRT, mousePosition, null, out rulerPos))
			{
				if (RulerUI.isHovering)
				{
					UpdateMeasureIndex();

					UpdateMeasureText();

					if (Input.GetMouseButtonDown(0))
					{
						SelectMeasurement();
					}
				}
			}
		}
	}

	void SelectMeasurement()
	{
		float currentMeasurementDistance = measurementDistance * currentMeasureIndex;
		GameManager.instance.SelectMeasurement(currentMeasureIndex, currentMeasurementDistance);
	}


	void UpdateMeasureIndex()
	{
		int targetMeasureIndex = -1;
		float targetMeasureDistance = Mathf.Infinity;
		for (int i = 0; i <= maxNumMeasureIndexes; i++)
		{
			float measureAmount = measurementDistance * i;
			float measureDistance = Mathf.Abs(measureAmount - rulerPos.x);
			if (measureDistance < targetMeasureDistance)
			{
				targetMeasureDistance = measureDistance;
				targetMeasureIndex = i;
			}
		}
		if (targetMeasureIndex != currentMeasureIndex) AudioManager.instance.MeasureTick();
		currentMeasureIndex = targetMeasureIndex;

		Vector2 measurePos = measureRT.sizeDelta;
		measurePos.x = measurementDistance * currentMeasureIndex;
		measureRT.sizeDelta = measurePos;
	}

	


	void UpdateMeasureText()
	{
		string measureString = MeasureIndexSplitter.ConvertIndex(currentMeasureIndex);
		SetMeasureText(measureString);
	}



	void SetMeasureText(string measureString)
	{
		measureText.text = measureString;
	}


	void UpdateMeasurementLengths()
	{
		maxNumMeasureIndexes = Rules.MaxNumberOfMarks(GameManager.instance.gameSettings.gameType, GameManager.instance.gameSettings.rulerLength);
		measurementDistance = Rules.MeasurementDistance(GameManager.instance.gameSettings.gameType);
	}



}
