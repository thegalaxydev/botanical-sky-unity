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

}
