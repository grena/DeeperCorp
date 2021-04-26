using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private GameObject buttonSellRoots;

    public Stock Stock;
    public RunningOperationCreator RunningOperationCreator;
    public List<RunningOperation> RunningOperations = new List<RunningOperation>();
    public RunningOperation _hoveredOperation;

    public Action<RunningOperation> OnOperationHovered;
    public Action<RunningOperation> OnOperationLaunched;
    public Action<RunningOperation> OnOperationAdded;
    public Action<RunningOperation> OnOperationFinished;
    public Action<RunningOperation> OnOperationRemoved;
    public Action OnGameOver;
    public Action OnGameStarts;

    public RunningOperation LaunchedOperation;
    public MoleBehaviour LaunchedMole;

    public float UpgradeDigTotal = 0;
    public float UpgradeAtqTotal = 0;
    public float UpgradeDefTotal = 0;
    public float UpgradeMasterTotal = 0;

    public int currentDepth = 0;
    public float nextTickRoots = 0;
    public int consumedFood = 0;
    public int sentMoles = 0;
    public bool isGameOver = false;
    public bool isGameStarted = false;
    
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
        if (!isGameStarted) return;
        if (isGameOver) return;
        
        if (Time.time > nextTickRoots)
        {
            if (Stock.Roots == 0)
            {
                isGameOver = true;
                OnGameOver?.Invoke();
                
                return;
            }
            
            Stock.Roots -= 1;
            consumedFood += 1;

            if (consumedFood % 20 == 0)
            {
                // NEW BABY!
                Stock.Pop += 1;
            }

            nextTickRoots = Time.time + Stock.Pop * 0.04f;
        }
    }

    public void LaunchMole(RunningOperation operation)
    {
        sentMoles++;
        
        MoleBehaviour moleBehaviour =
            GetComponent<MolesManager>().moleBehaviours.First(behaviour => behaviour.linePosition == 1);

        LaunchedMole = moleBehaviour;
        LaunchedOperation = operation;
        
        LaunchedMole.LetsDig();
        LaunchedMole.OnInside += OnLaunchedMoleInside;
        
        OnOperationLaunched?.Invoke(operation);
    }

    public void RelaunchGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void StartGame()
    {
        isGameStarted = true;
        OnGameStarts?.Invoke();
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
            Caps = 150,
            Roots = 120,
            Pop = 25,
        };
        
        Stock.OnStockUpdated += OnStockUpdated;
    }

    private void OnStockUpdated()
    {
        buttonSellRoots.SetActive(Stock.Roots > 30);
    }

    public void SellRoots()
    {
        if (Stock.Roots > 30)
        {
            Stock.Roots -= 30;
            Stock.Caps += 10;
        }
    }

    private void OnOperationReported()
    {
        GetComponent<MolesManager>().EndExpedition();
    }
}
