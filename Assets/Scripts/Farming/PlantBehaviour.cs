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

    private int _stage = 0;
    private bool _canBeHarvested = false;

    private UnityEvent _onGrowth;
    private UnityEvent _onGrowthTemp;

    public int Stage { get { return _stage; } }
    public bool CanBeHarvested { get { return _canBeHarvested; } }

    public void AddOnGrowthAction(UnityAction action) => _onGrowth.AddListener(action);
    public void AddOnGrowthTempAction(UnityAction action) => _onGrowthTemp.AddListener(action);


    private void Start()
    {
        if (_stages.Count == 0)
            Debug.LogException(new System.Exception("Plant must have at least one stage."));

        for (int i = 0; i < _stages.Count; i++)
            _stages[i].SetActive(false);

        _stages[0].SetActive(true);
    }

    private void Update()
    {
        
    }

    private void Grow()
    {

    }
}
