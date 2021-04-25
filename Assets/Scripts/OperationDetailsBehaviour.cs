using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OperationDetailsBehaviour : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text textDescription;
    [SerializeField] private TMP_Text textDig;
    [SerializeField] private Slider sliderDig;
    [SerializeField] private TMP_Text textAtq;
    [SerializeField] private TMP_Text textDef;
    [SerializeField] private TMP_Text textRewardCaps;
    [SerializeField] private TMP_Text textRewardRoots;

    public RunningOperation operation;

    private void Start()
    {
        GameManager.Singleton.OnOperationHovered += OnOperationHovered;
    }

    private void OnOperationHovered(RunningOperation op)
    {
        Display(op);
    }

    public void Display(RunningOperation op)
    {
        operation = op;
        
        Refresh();
    }

    private void Refresh()
    {
        if (operation is null)
        {
            canvasGroup.alpha = 0;
            return;
        }
        
        textDescription.text = operation.Description;
        textDig.text = $"{operation.Dig.ToString()}/{operation.DigTotal.ToString()}";
        sliderDig.maxValue = operation.DigTotal;
        sliderDig.value = operation.Dig;
        textAtq.text = $"{operation.AtqChallenge.RequiredValue.ToString()} ({operation.AtqChallenge.Name})";
        textDef.text = $"{operation.DefChallenge.RequiredValue.ToString()} ({operation.DefChallenge.Name})";
        textRewardCaps.text = operation.CapsReward.ToString();
        textRewardRoots.text = operation.RootsReward.ToString();
        canvasGroup.alpha = 1;
    }
}
