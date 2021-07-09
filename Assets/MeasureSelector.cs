using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MeasureSelector : MonoBehaviour
{
	public RectTransform rulerRT, measureRT, cursorRT;
	public TextMeshProUGUI measureText;
	public float sixteenthDistance;
	public int numInches;

	public Vector3 mousePosition;
	public Vector2 rulerPos;
	public int currentMeasureIndex;

	const int NUM_SIXTEENTHS_PER_INCH = 16;

	public int inchIndex;
	public int microMeasureIndex;



	private void Update()
	{
		mousePosition = Input.mousePosition;

		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(measureRT, mousePosition, null, out rulerPos))
		{
			

			UpdateMeasureIndex();

			ConvertMeasureIndex();

			UpdateMeasureText();
		}

	}


	void UpdateMeasureIndex()
	{
		int maxNumMeasureIndexes = NUM_SIXTEENTHS_PER_INCH * numInches;
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
		currentMeasureIndex = targetMeasureIndex;

		Vector2 measurePos = measureRT.sizeDelta;
		measurePos.x = sixteenthDistance * currentMeasureIndex;
		measureRT.sizeDelta = measurePos;
	}

	void ConvertMeasureIndex()
	{
		inchIndex = Mathf.FloorToInt(currentMeasureIndex / NUM_SIXTEENTHS_PER_INCH);
		microMeasureIndex = currentMeasureIndex % NUM_SIXTEENTHS_PER_INCH;
	}


	void UpdateMeasureText()
	{
		string measureString = "";
		string inchString = inchIndex.ToString();
		string microString = MicroMeasureIndex_Fraction(microMeasureIndex);
		if (inchIndex == 0) measureString = microString;
		else if (microMeasureIndex == 0) measureString = inchString;
		else measureString = string.Format("{0} - {1}", inchString, microString);
		measureText.text = measureString;
	}



	string MicroMeasureIndex_Fraction(int measureIndex)
	{
		switch (measureIndex)
		{
			case 0: return string.Empty;
			case 1: return "1/16";
			case 2: return "1/8";
			case 3: return "3/16";
			case 4: return "1/4";
			case 5: return "5/16";
			case 6: return "3/8";
			case 7: return "7/16";
			case 8: return "1/2";
			case 9: return "9/16";
			case 10: return "5/8";
			case 11: return "11/16";
			case 12: return "3/4";
			case 13: return "13/16";
			case 14: return "7/8";
			case 15: return "15/16";
			default: return string.Empty;
		}

	}


}
