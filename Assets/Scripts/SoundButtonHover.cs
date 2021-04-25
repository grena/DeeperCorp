using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SoundButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.singleton.Play(SoundManager.Sounds.SfxBtnClick);
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!GetComponent<Button>().interactable) return;
        
        SoundManager.singleton.Play(SoundManager.Sounds.SfxBtnHover);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // if (!GetComponent<Button>().interactable) return;
        //
        // SoundManager.singleton.Play(SoundManager.Sounds.SfxBtnClick);
    }
}
