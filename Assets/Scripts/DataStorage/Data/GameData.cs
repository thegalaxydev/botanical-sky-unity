using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
	public int _money;
	public string[] _plotData;

	public GameData()
	{
		_money = 0;
		_plotData = new string[9];
	}
}
