using System;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField] private OperationResultBehaviour operationResultBehaviour;

    public Stock Stock;
    public RunningOperationCreator RunningOperationCreator;
    public List<RunningOperation> RunningOperations = new List<RunningOperation>();
    public RunningOperation _hoveredOperation;

    public Action<RunningOperation> OnOperationHovered;
    public Action<RunningOperation> OnOperationLaunched;
    public Action<RunningOperation> OnOperationAdded;
    public Action<RunningOperation> OnOperationFinished;
    public Action<RunningOperation> OnOperationRemoved;

    public RunningOperation LaunchedOperation;
    public MoleBehaviour LaunchedMole;

    public float UpgradeDigTotal = 0;
    public float UpgradeAtqTotal = 0;
    public float UpgradeDefTotal = 0;
    public float UpgradeMasterTotal = 0;

    public int currentDepth = 0;

    public float nextTickRoots = 0;
    public int consumedFood = 0;
    
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

    private void Update()
    {
        if (Time.time > nextTickRoots)
        {
            Stock.Roots -= 1;
            consumedFood += 1;

            if (consumedFood % 30 == 0)
            {
                // NEW BABY!
                Stock.Pop += 1;
            }

            nextTickRoots = Time.time + Stock.Pop * 0.04f;
        }
    }

    public void LaunchMole(RunningOperation operation)
    {
        MoleBehaviour moleBehaviour =
            GetComponent<MolesManager>().moleBehaviours.First(behaviour => behaviour.linePosition == 1);

        LaunchedMole = moleBehaviour;
        LaunchedOperation = operation;
        
        LaunchedMole.LetsDig();
        LaunchedMole.OnInside += OnLaunchedMoleInside;
        
        OnOperationLaunched?.Invoke(operation);
    }

    private void OnLaunchedMoleInside()
    {
        operationResultBehaviour.Display(LaunchedOperation, LaunchedMole.mole);
    }

    public void FinishOperation(RunningOperation operation)
    {
        Stock.Caps += operation.CapsReward;
        Stock.Roots += operation.RootsReward;

        if (operation.CapsReward == 0 && operation.RootsReward == 0)
        {
            // GO DEEPER !
            GoDeeper();
        }

        RunningOperations.Remove(operation);
        
        OnOperationFinished?.Invoke(operation);
    }

    public void GoDeeper()
    {
        currentDepth++;

        foreach (RunningOperation runningOperation in RunningOperations)
        {
            OnOperationRemoved?.Invoke(runningOperation);
        }
        
        RunningOperations.Clear();
        
        for (int i = 0; i < 4; i++)
        {
            RunningOperation op = RunningOperationCreator.Create(currentDepth);
            RunningOperations.Add(op);
            OnOperationAdded?.Invoke(op);
        }

        RunningOperation opDeeper = RunningOperationCreator.CreateDeeper(currentDepth + 1);
        RunningOperations.Add(opDeeper);
        OnOperationAdded?.Invoke(opDeeper);
    }

    private void Initialize()
    {
        RunningOperationCreator = new RunningOperationCreator();
        
        RunningOperation opDeeper = RunningOperationCreator.CreateDeeper(currentDepth + 1);
        RunningOperations.Add(opDeeper);
        OnOperationAdded?.Invoke(opDeeper);
        
        operationResultBehaviour.OnReported += OnOperationReported;
        
        Stock = new Stock()
        {
            Caps = 100,
            Roots = 100,
            Pop = 25,
        };
    }

    private void OnOperationReported()
    {
        GetComponent<MolesManager>().EndExpedition();
    }
}
