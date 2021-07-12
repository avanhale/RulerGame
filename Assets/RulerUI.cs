using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RulerUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public static RulerUI instance;
	public static bool isHovering;
	public delegate void Hover(bool isHovering);
	public static event Hover OnHover;

	RectTransform myRT;
	public Transform inchBlocksT;
	GameObject[] inchBlocks;

	private void Awake()
	{
		instance = this;
		myRT = GetComponent<RectTransform>();
		GetInchBlocks();
	}

	private void Start()
	{
		RefreshCanvas();
	}

	void GetInchBlocks()
	{
		List<GameObject> inchBlocksList = new List<GameObject>();
		for (int i = 0; i < inchBlocksT.childCount; i++) inchBlocksList.Add(inchBlocksT.GetChild(i).gameObject);
		inchBlocks = inchBlocksList.ToArray();
	}

	public void SetInchBlocks(int numberOfBlocks)
	{
		for (int i = 0; i < inchBlocks.Length; i++)
		{
			if (i < numberOfBlocks) inchBlocks[i].SetActive(true);
			else inchBlocks[i].SetActive(false);
		}
		RefreshCanvas();
	}


	void RefreshCanvas()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(myRT);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isHovering = true;
		OnHover?.Invoke(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isHovering = false;
		OnHover?.Invoke(false);
	}
}
