using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



	private void Awake()
	{
	}


	void Start()
	{
		SetupFarmForPlayer();
	}

	// Update is called once per frame
	void Update()
	{
		
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
		GameObject farm = Instantiate(_farmPrefab, _farmSpawnPosition, Quaternion.identity);

		Vector3 plotScale = _plotPrefab.transform.localScale;

        List<Vector3> plotGrid = GenerateGridPoints(plotScale, _farmSpawnPosition + new Vector3(0, plotScale.y, 0), 0, 3);

		for (int i = 0; i < plotGrid.Count; i++)
		{
			GameObject plot = Instantiate(_plotPrefab, plotGrid[i], Quaternion.identity);
		}
	}
}
