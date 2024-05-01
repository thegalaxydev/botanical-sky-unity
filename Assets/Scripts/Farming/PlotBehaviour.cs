using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotBehaviour : MonoBehaviour
{

	private PlantBehaviour _plant;
	public PlantBehaviour Plant { get { return _plant; } }
	
	private Color _originalColor;
	private void Start() {
		_originalColor = GetComponent<Renderer>().material.color;
	}

	public PlantBehaviour PlantSeed(GameObject plant)
	{
		if (_plant != null)
			return null;

		PlantBehaviour plantBehaviour = plant.GetComponent<PlantBehaviour>();
		if (plantBehaviour == null)
			return null;

		GameObject newPlant = Instantiate(plant, transform.position + new Vector3(0, transform.localScale.y / 2, 0), Quaternion.identity);

		_plant = newPlant.GetComponent<PlantBehaviour>();

		_plant.SetStage(0);

		return _plant;
	}

	public void Harvest()
	{
		Debug.Log("Attemptin to Harvest.");
		if (_plant == null)
		{
			Debug.Log("Cannot harvest. No plant.");
			return;
		}

		if (!_plant.CanBeHarvested)
		{
			Debug.Log("Cannot harvest, plant cannot be harvested.");
			return;
		}


		int yield = _plant.Yield;

		GameManager.Instance.AddMoney(yield * _plant.Worth);

		Destroy(_plant.gameObject);
		_plant = null;
	}

	[SerializeField]
	private GameObject _testPlant;

	private void Update() {
		
	}

	public void Highlight()
	{
		GetComponent<Renderer>().material.color = _originalColor * 1.2f;
	}

	public void Select()
	{
		GetComponent<Renderer>().material.color = _originalColor * 1.8f;
	}

	public void Deselect()
	{
		GetComponent<Renderer>().material.color = _originalColor;
	}

	public string SerializePlot()
	{
		int plantID = _plant == null ? -1 : _plant.PlantID;
		int stage = _plant == null ? 0 : _plant.Stage;
		bool canBeHarvested = _plant == null ? false : _plant.CanBeHarvested;

		return $"{plantID.ToString()},{stage}";
	}

	public void DeserializePlot(string data)
	{
		string[] splitData = data.Split(',');

		if (splitData[0] == "-1")
			return;

		GameObject plant = GameManager.Instance.PlantTypes[int.Parse(splitData[0])];
		Debug.Log(plant == null);

		PlantBehaviour newPlant = PlantSeed(plant);
		newPlant.SetStage(int.Parse(splitData[1]));
	}
}
