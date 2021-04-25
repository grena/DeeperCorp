using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverBehaviour : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text textDeepestExpedition;
    [SerializeField] private TMP_Text textSentMoles;
    [SerializeField] private TMP_Text textPopulationReached;
    [SerializeField] private TMP_Text textRootsConsumed;

    private void Start()
    {
        ToggleVisibility(false);
        
        GameManager.Singleton.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        Display();
    }

    public void Display()
    {
        textDeepestExpedition.text = $"{GameManager.Singleton.currentDepth} meters";
        textSentMoles.text = $"{GameManager.Singleton.sentMoles}";
        textPopulationReached.text = $"{GameManager.Singleton.Stock.Pop}";
        textRootsConsumed.text = $"{GameManager.Singleton.consumedFood}";

        ToggleVisibility(true);
    }
    
    public void ToggleVisibility(bool visible)
    {
        canvasGroup.alpha = visible ? 1 : 0;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
    }
}
