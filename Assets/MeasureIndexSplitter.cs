using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureIndexSplitter : MonoBehaviour
{



	public static string ConvertIndex(int measureIndex)
	{
		string measureString = string.Empty;
		ConvertMeasureIndex(measureIndex, out var inchIndex, out var remainderIndex);
		string inchString = inchIndex.ToString();
		string remainderString = RemainderIndex_Fraction(remainderIndex);
		if (inchIndex == 0 && remainderIndex == 0) measureString = inchString;
		else if (inchIndex == 0) measureString = remainderString;
		else if (remainderIndex == 0) measureString = inchString;
		else measureString = string.Format("{0} - {1}", inchString, remainderString);
		measureString += "\"";
		return measureString;
	}

	public static void ConvertMeasureIndex(int measureIndex, out int inchIndex, out int remainderIndex)
	{
		inchIndex = Mathf.FloorToInt(measureIndex / Rules.NUM_SIXTEENTHS_PER_INCH);
		remainderIndex = measureIndex % Rules.NUM_SIXTEENTHS_PER_INCH;
	}


	static string RemainderIndex_Fraction(int measureIndex)
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
