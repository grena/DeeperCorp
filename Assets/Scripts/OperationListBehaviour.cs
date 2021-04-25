using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class OperationListBehaviour : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject operationButtonPrefab;
    [SerializeField] private CanvasGroup canvasGroup;
    
    void Start()
    {
        ToggleVisibility(false);
        
        GameManager.Singleton.OnOperationLaunched += OnOperationLaunched;
        GameManager.Singleton.OnOperationAdded += OnOperationAdded;
        
        // Fill the list with existing operations (temp solution)
        foreach (RunningOperation runningOperation in GameManager.Singleton.RunningOperations)
        {
            GameObject opButton = Instantiate(operationButtonPrefab, Vector3.zero, Quaternion.identity);
            opButton.transform.SetParent(container, false);
            
            opButton.GetComponent<OperationButtonBehaviour>().Setup(runningOperation);
        }
    }

    private void OnOperationAdded(RunningOperation op)
    {
        GameObject opButton = Instantiate(operationButtonPrefab, Vector3.zero, Quaternion.identity);
        opButton.transform.SetParent(container, false);
            
        opButton.GetComponent<OperationButtonBehaviour>().Setup(op);
    }

    private void OnOperationLaunched(RunningOperation op)
    {
        ToggleVisibility(false);
    }

    public void ToggleVisibility(bool visible)
    {
        canvasGroup.alpha = visible ? 1 : 0;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
    }
}
