using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class OperationListBehaviour : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject operationButtonPrefab;
    
    void Start()
    {
        // Fill the list with existing operations (temp solution)
        foreach (RunningOperation runningOperation in GameManager.Singleton.RunningOperations)
        {
            GameObject opButton = Instantiate(operationButtonPrefab, Vector3.zero, Quaternion.identity);
            opButton.transform.SetParent(container, false);
            
            opButton.GetComponent<OperationButtonBehaviour>().Setup(runningOperation);
        }
    }
}
