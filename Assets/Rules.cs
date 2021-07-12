using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rules
{

	public enum GameType {
		Wholes		= 1,
		Halves		= 2,
		Quarters	= 3,
		Eighths		= 4,
		Sixteenths	= 5,
	};

	public static int NUM_SIXTEENTHS_PER_INCH = 16;
	public static int NUM_EIGHTHS_PER_INCH = 8;
	public static int NUM_QUARTERS_PER_INCH = 4;
	public static int NUM_HALVES_PER_INCH = 2;
	public static int NUM_WHOLES_PER_INCH = 1;

	public static float measurementDistance_Sixteenth = 0.171875f;

	public static int NumberMarksPerInch(GameType gameType)
	{
		switch (gameType)
		{
			case GameType.Wholes: return NUM_WHOLES_PER_INCH;
			case GameType.Halves: return NUM_HALVES_PER_INCH;
			case GameType.Quarters: return NUM_QUARTERS_PER_INCH;
			case GameType.Eighths: return NUM_EIGHTHS_PER_INCH;
			case GameType.Sixteenths: return NUM_SIXTEENTHS_PER_INCH;
			default: return NUM_WHOLES_PER_INCH;
		}
	}

	public static int MaxNumberOfMarks(GameType gameType, int rulerLength)
	{
		return NumberMarksPerInch(gameType) * rulerLength;
	}



	public static float MeasurementDistance(GameType gameType)
	{
		switch (gameType)
		{
			case GameType.Wholes: return measurementDistance_Sixteenth * NUM_SIXTEENTHS_PER_INCH;
			case GameType.Halves: return measurementDistance_Sixteenth * NUM_EIGHTHS_PER_INCH;
			case GameType.Quarters: return measurementDistance_Sixteenth * NUM_QUARTERS_PER_INCH;
			case GameType.Eighths: return measurementDistance_Sixteenth * NUM_HALVES_PER_INCH;
			case GameType.Sixteenths: return measurementDistance_Sixteenth * NUM_WHOLES_PER_INCH;
			default: return measurementDistance_Sixteenth;
		}

	}



	public static GameType GetGameType(int index)
	{
		return (GameType)index;
	}


}
