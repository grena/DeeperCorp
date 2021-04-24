using System;
using TMPro;
using UnityEngine;

public class ResourcesPanelBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text textCapsCount;
    [SerializeField] private TMP_Text textRootsCount;
    
    private void Start()
    {
        GameManager.Singleton.Stock.OnStockUpdated += OnStockUpdated;
        
        RefreshStocks();
    }

    private void OnStockUpdated()
    {
        RefreshStocks();
    }

    private void RefreshStocks()
    {
        textCapsCount.text = GameManager.Singleton.Stock.Caps.ToString();
        textRootsCount.text = GameManager.Singleton.Stock.Roots.ToString();
    }
}
