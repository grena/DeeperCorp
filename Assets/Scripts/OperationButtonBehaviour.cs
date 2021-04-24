using Core;
using TMPro;
using UnityEngine;

public class OperationButtonBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text textName;
    [SerializeField] private TMP_Text textDepth;

    public RunningOperation operation;

    public void Setup(RunningOperation op)
    {
        operation = op;

        textName.text = operation.Name;
        textDepth.text = $"{operation.Depth} meters";
    }
}
