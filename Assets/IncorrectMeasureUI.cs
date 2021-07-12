using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncorrectMeasureUI : MonoBehaviour
{
	RectTransform myRT;

	private void Awake()
	{
		myRT = GetComponent<RectTransform>();
	}



	public void Activate(float xWidth)
	{
		myRT.sizeDelta = new Vector2(xWidth, myRT.sizeDelta.y);
		gameObject.SetActive(true);
	}

	public void Deactivate()
	{
		gameObject.SetActive(false);
	}


}
