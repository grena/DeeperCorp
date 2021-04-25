using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _singleton;
    public static GameManager Singleton => _singleton;

    public RunningOperation HoveredOperation
    {
        get => _hoveredOperation;
        set
        {
            _hoveredOperation = value;
            OnOperationHovered?.Invoke(value);
        }
    }

    public Stock Stock;
    public RunningOperationCreator RunningOperationCreator;
    public List<RunningOperation> RunningOperations = new List<RunningOperation>();
    public RunningOperation _hoveredOperation;

    public Action<RunningOperation> OnOperationHovered;
    
    private void Awake()
    {
        if (_singleton != null && _singleton != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _singleton = this;
        }
        
        Initialize();
    }
    
    private void Initialize()
    {
        RunningOperationCreator = new RunningOperationCreator();

        for (int i = 0; i < 3; i++)
        {
            RunningOperations.Add(RunningOperationCreator.Create(12));
        }
        
        Stock = new Stock()
        {
            Caps = 100,
            Roots = 100,
        };
    }
}
