using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxBtnHover;
    [SerializeField] private AudioSource sfxBtnClick;
    [SerializeField] private AudioSource sfxFillDig;
    [SerializeField] private AudioSource sfxBlipResult;
    [SerializeField] private AudioSource sfxBlipResultValueFail;
    [SerializeField] private AudioSource sfxBlipResultValueSuccess;
    [SerializeField] private AudioSource sfxSuccessOperation;

    public enum Sounds
    {
        SfxBtnHover,
        SfxBtnClick,
        SfxFillDig,
        SfxBlipResult,
        SfxBlipResultValueFail,
        SfxBlipResultValueSuccess,
        SfxSuccessOperation,
    }
    
    private static SoundManager _instance;

    public static SoundManager singleton => _instance;

    private void Awake() 
    { 
        if (_instance != null && _instance != this) 
        { 
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    } 

    public void Play(Sounds sound)
    {
        switch (sound)
        {
            case Sounds.SfxBtnHover:
                sfxBtnHover.PlayOneShot(sfxBtnHover.clip);
                break;
            case Sounds.SfxBtnClick:
                sfxBtnClick.PlayOneShot(sfxBtnClick.clip);
                break;
            case Sounds.SfxFillDig:
                sfxBtnClick.PlayOneShot(sfxFillDig.clip);
                break;
            case Sounds.SfxBlipResult:
                sfxBtnClick.PlayOneShot(sfxBlipResult.clip);
                break;
            case Sounds.SfxBlipResultValueFail:
                sfxBtnClick.PlayOneShot(sfxBlipResultValueFail.clip);
                break;
            case Sounds.SfxBlipResultValueSuccess:
                sfxBtnClick.PlayOneShot(sfxBlipResultValueSuccess.clip);
                break;
            case Sounds.SfxSuccessOperation:
                sfxBtnClick.PlayOneShot(sfxSuccessOperation.clip);
                break;
        }
    }
}
