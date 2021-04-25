using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelBehaviour : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button buttonDigUpgrade;
    [SerializeField] private TMP_Text textDigUpgradeAmount;
    [SerializeField] private TMP_Text textDigUpgradeCost;
    [SerializeField] private TMP_Text textDigUpgradeTotal;
    [SerializeField] private Button buttonAtqUpgrade;
    [SerializeField] private TMP_Text textAtqUpgradeAmount;
    [SerializeField] private TMP_Text textAtqUpgradeCost;
    [SerializeField] private TMP_Text textAtqUpgradeTotal;
    [SerializeField] private Button buttonDefUpgrade;
    [SerializeField] private TMP_Text textDefUpgradeAmount;
    [SerializeField] private TMP_Text textDefUpgradeCost;
    [SerializeField] private TMP_Text textDefUpgradeTotal;
    [SerializeField] private Button buttonMasterUpgrade;
    [SerializeField] private TMP_Text textMasterUpgradeAmount;
    [SerializeField] private TMP_Text textMasterUpgradeCost;
    [SerializeField] private TMP_Text textMasterUpgradeTotal;
    [SerializeField] private Button buttonClose;


    public int digCost = 10;
    public int digAmount = 5;
    
    public int atqCost = 15;
    public int atqAmount = 5;
    
    public int defCost = 20;
    public int defAmount = 5;
    
    public int masterCost = 50;
    public int masterAmount = 1;

    private void Awake()
    {
        ToggleVisibility(false);
        RefreshPrices();
    }

    public void RefreshPrices()
    {
        textDigUpgradeAmount.text = $"+{digAmount}%";
        textDigUpgradeCost.text = digCost.ToString();
        
        textAtqUpgradeAmount.text = $"+{atqAmount}%";
        textAtqUpgradeCost.text = atqCost.ToString();
        
        textDefUpgradeAmount.text = $"+{defAmount}%";
        textDefUpgradeCost.text = defCost.ToString();
        
        textMasterUpgradeAmount.text = $"+{masterAmount}%";
        textMasterUpgradeCost.text = masterCost.ToString();
    }

    public void ToggleVisibility(bool visible)
    {
        canvasGroup.alpha = visible ? 1 : 0;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
    }

    public void UpgradeDig()
    {
        if (GameManager.Singleton.Stock.Caps < digCost) return;
        
        GameManager.Singleton.UpgradeDigTotal += digAmount;
        GameManager.Singleton.Stock.Caps -= digCost;

        textDigUpgradeTotal.text = $"total +{GameManager.Singleton.UpgradeDigTotal}%";
    }

    public void UpgradeAtq()
    {
        if (GameManager.Singleton.Stock.Caps < atqCost) return;
        
        GameManager.Singleton.UpgradeAtqTotal += atqAmount;
        GameManager.Singleton.Stock.Caps -= atqCost;

        textAtqUpgradeTotal.text = $"total +{GameManager.Singleton.UpgradeAtqTotal}%";
    }
    
    public void UpgradeDef()
    {
        if (GameManager.Singleton.Stock.Caps < defCost) return;
        
        GameManager.Singleton.UpgradeDefTotal += defAmount;
        GameManager.Singleton.Stock.Caps -= defCost;

        textDefUpgradeTotal.text = $"total +{GameManager.Singleton.UpgradeDefTotal}%";
    }
    
    public void UpgradeMaster()
    {
        if (GameManager.Singleton.Stock.Caps < masterCost) return;
        
        GameManager.Singleton.UpgradeMasterTotal += masterAmount;
        GameManager.Singleton.Stock.Caps -= masterCost;

        textMasterUpgradeTotal.text = $"total +{GameManager.Singleton.UpgradeMasterTotal}%";
    }
}
