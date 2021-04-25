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

    [SerializeField] private GameObject goAtq;
    [SerializeField] private TMP_Text textAtq;
    [SerializeField] private GameObject goDef;
    [SerializeField] private TMP_Text textDef;
    
    [SerializeField] private GameObject goClassicRewards;
    [SerializeField] private GameObject goDeeperReward;
    [SerializeField] private TMP_Text textRewardCaps;
    [SerializeField] private TMP_Text textRewardRoots;

    public RunningOperation operation;

    private void Start()
    {
        GameManager.Singleton.OnOperationHovered += OnOperationHovered;
        canvasGroup.alpha = 0;
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

        if (operation.CapsReward == 0 && operation.RootsReward == 0)
        {
            // DEEPER OPERATION
            goAtq.SetActive(false);
            goDef.SetActive(false);
            goClassicRewards.SetActive(false);
            goDeeperReward.SetActive(true);
        }
        else
        {
            // CLASSIC OPERATION
            goAtq.SetActive(true);
            goDef.SetActive(true);
            goClassicRewards.SetActive(true);
            goDeeperReward.SetActive(false);
        }
        
        canvasGroup.alpha = 1;
    }
}
