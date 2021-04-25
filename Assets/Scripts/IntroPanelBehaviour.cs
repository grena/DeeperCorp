using UnityEngine;

public class IntroPanelBehaviour : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        GameManager.Singleton.OnGameStarts += OnGameStarts;
        ToggleVisibility(true);
    }

    private void OnGameStarts()
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
