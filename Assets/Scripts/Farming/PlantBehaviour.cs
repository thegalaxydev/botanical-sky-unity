using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlantBehaviour : MonoBehaviour
{
	[SerializeField, Tooltip("Each stage of the plant's life.")]
	private List<GameObject> _stages;

	[SerializeField, Tooltip("The total amount of time (in seconds) to get from stage 0 to the final stage.")]
	private float _growthTime;

	[SerializeField, Tooltip("The total yield of the plant when harvested.")]
	private int _yield;

	[SerializeField, Tooltip("The name of the plant.")]
	private string _plantName = "Plant";

	[SerializeField, Tooltip("The worth of the plant when harvested.")]
	private int _worth;


	[SerializeField, Tooltip("Identifier For Plant")]
	private int _plantID;

	private int _stage = 0;
	private bool _canBeHarvested = false;
	private float _elapsedTime = 0;

	[SerializeField]
	private UnityEvent _onGrowth;

	[SerializeField]
	private UnityEvent _onGrowthTemp;
	public int Stage { get { return _stage; } set { _stage = value; }}
	public bool CanBeHarvested { get { return _canBeHarvested; }}
	public int Yield { get { return _yield; } }
	public string PlantName { get { return _plantName; } }
	public int Worth { get { return _worth; } }
	public int PlantID { get { return _plantID; } }

	public void AddOnGrowthAction(UnityAction action) => _onGrowth.AddListener(action);
	public void AddOnGrowthTempAction(UnityAction action) => _onGrowthTemp.AddListener(action);


	private void Start()
	{
		if (_stages.Count == 0)
			Debug.LogException(new System.Exception("Plant must have at least one stage."));
	}

	private void Update()
	{
		if (_stage >= _stages.Count - 1)
		{
			_canBeHarvested = true;
			return;
		}

		float timeBetweenGrowth = _growthTime / _stages.Count;

		_elapsedTime += Time.deltaTime;

		if (_elapsedTime >= timeBetweenGrowth)
		{
			_elapsedTime = 0;
			Grow();
		}
	}

	public void SetStage(int stage)
	{
		_stage = stage;

		for (int i = 0; i < _stages.Count; i++)
			_stages[i].SetActive(false);

		_stages[stage].SetActive(true);
	}

	private void Grow()
	{
		if (_stage >= _stages.Count - 1)
		{
			_canBeHarvested = true;
			return;
		}

		_stage += 1;

		if (_stage == _stages.Count - 1)
			_canBeHarvested = true;

		for (int i = 0; i < _stages.Count; i++)
			_stages[i].SetActive(false);

		_stages[_stage].SetActive(true);
		Debug.Log(_stage);

		_onGrowth?.Invoke();
		_onGrowthTemp?.Invoke();

		_onGrowthTemp?.RemoveAllListeners();

		Debug.Log("Growing plant. Current stage: " + _stage.ToString() + " / " + (_stages.Count - 1).ToString() + ". Can harvest? " + _canBeHarvested.ToString());
	}
}
