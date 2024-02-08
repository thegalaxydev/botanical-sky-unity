using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	// singleton instance
	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject();
				_instance = gameObject.AddComponent<GameManager>();
			}
			return _instance;
		}
	}

	[SerializeField, Tooltip("The world position at which the farm should spawn. This shouldn't need to change.")]
	private Vector3 _farmSpawnPosition = new Vector3(0, 0, 0);

	[SerializeField]
	private GameObject _farmPrefab;

	[SerializeField]
	private GameObject _plotPrefab;

	[SerializeField]
	private LayerMask _plotLayer;

	[SerializeField]
	private GameObject _plotMenu;

	[SerializeField]
	private Text _plotNameText;

	[SerializeField]
	private Text _plantNameText;

	[SerializeField]
	private Text _stageText;

	[SerializeField]
	private GameObject _actionButton;
	[SerializeField]
	private Text _actionText;

	private string action = "Plant";

	private GameObject _selectedPlot;
	private List<GameObject> _plots = new List<GameObject>();
	void Start()
	{
		_plotMenu.SetActive(false);
		SetupFarmForPlayer();

		_actionButton.GetComponent<Button>().onClick.AddListener(PerformAction);
	}

	// Update is called once per frame
	void Update()
	{
		foreach (GameObject plot in _plots)
		{
			if (plot == _selectedPlot)
				continue;

			plot.GetComponent<PlotBehaviour>().Deselect();
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		bool didHit = Physics.Raycast(ray, out hit, Mathf.Infinity, _plotLayer);

		if (!didHit)
			return;

		
		PlotBehaviour plotBehaviour = hit.collider.GetComponent<PlotBehaviour>();
		if (hit.collider.gameObject != _selectedPlot)
			plotBehaviour.Highlight();

		if (Input.GetMouseButtonDown(0))
		{
			SelectPlot(hit.collider.gameObject);
		}

		if (_selectedPlot == null)
			_plotMenu.SetActive(false);
	}


	public void SelectPlot(GameObject plot, bool force = false)
	{
		PlotBehaviour plotBehaviour = plot.GetComponent<PlotBehaviour>();
		if (_selectedPlot == plot && !force)
		{
			plotBehaviour.Deselect();
			_selectedPlot = null;
			return;
		}

		foreach (GameObject p in _plots)
		{
			if (p == plot)
				continue;

			PlantBehaviour plantBehaviour = p.GetComponent<PlotBehaviour>().Plant;

			if (plantBehaviour == null)
				continue;
		}

		_selectedPlot = plot;
		plotBehaviour.Select();

		_plotMenu.SetActive(true);
		_plotNameText.text = _selectedPlot.name;

		_plantNameText.text = "";
		_stageText.text = "";

		_actionText.text = "Plant";
		action = "Plant";
		_actionButton.SetActive(true);
		PlantBehaviour plant = _selectedPlot.GetComponent<PlotBehaviour>().Plant;
		if (plant != null)
		{
			_actionButton.SetActive(false);
			_plantNameText.text = plant.PlantName;
			_stageText.text = "Stage " + plant.Stage.ToString();
			
			if (plant.CanBeHarvested)
			{
				action = "Harvest";
				_actionText.text = "Harvest";
				_actionButton.SetActive(true);
			}
		}
	}

	[SerializeField]
	private GameObject _testPlant;
	private void PerformAction()
	{
		if (!_selectedPlot)
			return;

		PlotBehaviour plotBehaviour = _selectedPlot.GetComponent<PlotBehaviour>();

		if (action == "Plant")
		{
			PlantBehaviour plant = plotBehaviour.PlantSeed(_testPlant);
			SelectPlot(_selectedPlot, true);
			plant.AddOnGrowthAction(() =>
			{
				SelectPlot(_selectedPlot, true);
			});
		}
		else if (action == "Harvest")
		{
			plotBehaviour.Harvest();
			SelectPlot(_selectedPlot, true);
		}
	}

    public static List<Vector3> GenerateGridPoints(Vector3 tileSize, Vector3 centerPosition, int spacer, int gridSize)
    {
		float totalWidth = (tileSize.x * gridSize) + (spacer * (gridSize - 1));
        float totalHeight = (tileSize.z * gridSize) + (spacer * (gridSize - 1));

        float startX = centerPosition.x - (totalWidth / 2) + (tileSize.x / 2);
		float startZ = centerPosition.z - (totalHeight / 2) + (tileSize.z / 2);

		List<Vector3> gridPoints = new List<Vector3>();

		for (int x = 0; x < gridSize; x++)
		{
			for (int z = 0; z < gridSize; z++)
			{
				Vector3 objectPosition = new Vector3(
						startX + x * (tileSize.x + spacer),
						centerPosition.y,
						startZ + z * (tileSize.z + spacer)
					);

				gridPoints.Add(objectPosition);
			}
		}

		return gridPoints;
    }

	public void SetupFarmForPlayer()
	{
		Instantiate(_farmPrefab, _farmSpawnPosition, Quaternion.identity);

		Vector3 plotScale = _plotPrefab.transform.localScale;

        List<Vector3> plotGrid = GenerateGridPoints(plotScale, _farmSpawnPosition + new Vector3(0, plotScale.y, 0), 0, 3);

		for (int i = 0; i < plotGrid.Count; i++)
		{
			GameObject plot = Instantiate(_plotPrefab, plotGrid[i], Quaternion.identity);
			plot.name = (i+1).ToString();
			_plots.Add(plot);

		}
	}
}
