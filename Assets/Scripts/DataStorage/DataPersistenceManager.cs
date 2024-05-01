using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
	private GameData _gameData;

	private List<IDataPersisetence> _dataHandlers = new List<IDataPersisetence>();

	[SerializeField]
	private string fileName;
	private FileDataHandler _dataHandler;

	// singleton instance
	private static DataPersistenceManager _instance;
	public static DataPersistenceManager Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject();
				_instance = gameObject.AddComponent<DataPersistenceManager>();
			}
			return _instance;
		}
	}

	private void Start() {
		_dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
		_dataHandlers = FindAllDataPersistanceObjects();
		LoadGame();
	}

	private void OnApplicationQuit() {
		SaveGame();
	}


	public void NewGame()
	{
		_gameData = new GameData();
	}

	public void LoadGame()
	{
		_gameData = _dataHandler.Load();

		if (_gameData == null) {
			NewGame();
		}		

		foreach (IDataPersisetence dataHandler in _dataHandlers) {
			dataHandler.LoadData(_gameData);
		}
	}

	public void SaveGame()
	{
		foreach (IDataPersisetence dataHandler in _dataHandlers) {
			dataHandler.SaveData(ref _gameData);
		}

		_dataHandler.Save(_gameData);
	}

	private List<IDataPersisetence> FindAllDataPersistanceObjects()
	{
		IEnumerable<IDataPersisetence> dataHandlers = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersisetence>();

		return new List<IDataPersisetence>(dataHandlers);
	}
	
}
