using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MeasureSelector : MonoBehaviour
{
	public GameManager gameManager;
	public RectTransform measureRT, cursorRT;
	Image measureImage;
	public TextMeshProUGUI measureText;
	public float sixteenthDistance;
	public int numInches;

	public Vector3 mousePosition;
	public Vector2 rulerPos;
	public int currentMeasureIndex;


	public int inchIndex;
	public int microMeasureIndex;

	private void Awake()
	{
		measureImage = measureRT.GetComponent<Image>();
	}

	private void OnEnable()
	{
		RulerPointerHover.OnHover += RulerPointerHover_OnHover;
	}
	private void OnDisable()
	{
		RulerPointerHover.OnHover -= RulerPointerHover_OnHover;
	}

	private void RulerPointerHover_OnHover(bool isHovering)
	{
		measureImage.enabled = isHovering;
	}

	private void Update()
	{
		mousePosition = Input.mousePosition;

		if (GameManager.canSelectMeasurement)
		{
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(measureRT, mousePosition, null, out rulerPos))
			{
				if (RulerPointerHover.isHovering)
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
		gameManager.SelectMeasurement(currentMeasureIndex);
	}


	void UpdateMeasureIndex()
	{
		int maxNumMeasureIndexes = Rules.NUM_SIXTEENTHS_PER_INCH * numInches;
		int targetMeasureIndex = -1;
		float targetMeasureDistance = Mathf.Infinity;
		for (int i = 0; i <= maxNumMeasureIndexes; i++)
		{
			float measureAmount = sixteenthDistance * i;
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
		measurePos.x = sixteenthDistance * currentMeasureIndex;
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



	


}
