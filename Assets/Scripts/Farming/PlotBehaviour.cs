using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotBehaviour : MonoBehaviour
{

    private PlantBehaviour _plant;
    public PlantBehaviour Plant { get { return _plant; } }

    public void PlantSeed(GameObject plant)
    {
        if (_plant != null)
            return;

        PlantBehaviour plantBehaviour = plant.GetComponent<PlantBehaviour>();
        if (plantBehaviour == null)
            return;

        GameObject newPlant = Instantiate(plant, transform.position + new Vector3(0, transform.localScale.y / 2, 0), Quaternion.identity);

        _plant = newPlant.GetComponent<PlantBehaviour>(); ;
    }

    public void Harvest()
    {
        Debug.Log("Attemptin to Harvest.");
        if (_plant == null)
        {
            Debug.Log("Cannot harvest. No plant.");
            return;
        }

        Debug.Log(_plant.CanBeHarvested);
        if (!_plant.CanBeHarvested)
        {
            Debug.Log("Cannot harvest, plant cannot be harvested.");
            return;
        }


        int yield = _plant.Yield;

        Destroy(_plant.gameObject);
        _plant = null;
    }

    [SerializeField]
    private GameObject _testPlant;

    private void Update()
    {
        // debug stuff
        if (Input.GetKeyUp(KeyCode.K))
        {
            PlantSeed(_testPlant);
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            Harvest();
        }
    }


}
