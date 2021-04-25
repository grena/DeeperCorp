using Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OperationButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.Singleton.HoveredOperation = operation;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Singleton.HoveredOperation = null;
    }
}
