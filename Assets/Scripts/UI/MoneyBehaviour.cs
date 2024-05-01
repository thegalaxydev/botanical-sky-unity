using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyBehaviour : MonoBehaviour
{    
	private Text _outputText;
	void Start()
	{
		_outputText = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update()
	{
		_outputText.text = $"${GameManager.Instance.Money}";
	}
}
