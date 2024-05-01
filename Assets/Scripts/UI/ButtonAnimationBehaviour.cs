using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonAnimationBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{   
	[SerializeField]
	private Text _buttonText;

	[SerializeField]
	private Vector3 _upOffset = new Vector3(0, 4, 0);

	[SerializeField]
	private Vector3 _downOffset = new Vector3(0, 0, 0);


	public void OnPointerDown(PointerEventData eventData)
	{
		_buttonText.rectTransform.localPosition = _downOffset;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_buttonText.rectTransform.localPosition = _upOffset;
	}
}
