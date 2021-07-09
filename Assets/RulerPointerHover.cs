using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RulerPointerHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public static bool isHovering;
	public delegate void Hover(bool isHovering);
	public static event Hover OnHover;

	private void Start()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
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
