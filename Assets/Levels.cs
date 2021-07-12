using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "ScriptableObjects/Levels", order = 1)]
public class Levels : ScriptableObject
{
	[System.Serializable]
	public class Level
	{
		public int countdownTime;
		public int scoreIncrement;
	}

	public Level[] levels;


	public Level GetLevel(int levelNumber)
	{
		return levels[levelNumber - 1];
	}

}
