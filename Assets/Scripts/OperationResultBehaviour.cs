using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OperationResultBehaviour : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text textOperationName;
    [SerializeField] private TMP_Text textDefDetail;
    [SerializeField] private TMP_Text textDefValue;
    [SerializeField] private TMP_Text textDefResult;
    [SerializeField] private TMP_Text textAtqDetail;
    [SerializeField] private TMP_Text textAtqValue;
    [SerializeField] private TMP_Text textAtqResult;
    [SerializeField] private TMP_Text textDig;
    [SerializeField] private Slider sliderDig;

    [SerializeField] private GameObject objDef;
    [SerializeField] private GameObject objAtq;
    [SerializeField] private GameObject objDig;

    private RunningOperation _operation;
    private Mole _mole;

    public Action OnReported;
    
    private void Start()
    {
        canvasGroup.alpha = 0;
    }

    public void Display(RunningOperation operation, Mole mole)
    {
        _operation = operation;
        _mole = mole;

        HideStuff();
        canvasGroup.alpha = 1;
        
        textOperationName.text = operation.Name;
        bool defSuccess = true;
        bool atqSuccess = true;
        int digResult = 0;
        int newOperationDig = operation.Dig;
        
        if (mole.Def < operation.DefChallenge.RequiredValue)
        {
            float roll = Random.Range(0f, 1f);
            float successRange = (float) mole.Def / operation.DefChallenge.RequiredValue;
            
            defSuccess = roll < successRange;
        }

        if (defSuccess)
        {
            if (mole.Atq < operation.AtqChallenge.RequiredValue)
            {
                float roll = Random.Range(0f, 1f);
                float successRange = (float) mole.Atq / operation.AtqChallenge.RequiredValue;
                
                atqSuccess = roll < successRange;
            }
        }

        if (defSuccess)
        {
            digResult = atqSuccess 
                ? mole.Dig 
                : Mathf.FloorToInt((float) mole.Atq / operation.AtqChallenge.RequiredValue * mole.Dig); // Embarassed by creature, digged less than expected
        }
        else
        {
            // Burried! We'll have to dig more next time.
            digResult = -Mathf.FloorToInt(0.5f * mole.Dig);
        }
        
        newOperationDig = Mathf.Clamp(_operation.Dig + digResult, 0, operation.DigTotal);
        
        ///////// NOW DISPLAY TO PLAYER

        textDefDetail.text = operation.DefChallenge.Name;
        textDefValue.text = operation.DefChallenge.RequiredValue.ToString();
        textAtqDetail.text = operation.AtqChallenge.Name;
        textAtqValue.text = operation.AtqChallenge.RequiredValue.ToString();
        textDig.text = $"{operation.Dig}/{operation.DigTotal}";
        sliderDig.maxValue = operation.DigTotal;
        sliderDig.value = operation.Dig;

        float timing = 0.3f;

        if (operation.CapsReward > 0 || operation.RootsReward > 0)
        {
            StartCoroutine(RevealDef(timing));
            timing += 0.3f;
            StartCoroutine(RevealDefResult(timing, defSuccess));
            timing += 0.4f;

            if (defSuccess)
            {
                StartCoroutine(RevealAtq(timing));
                timing += 0.3f;
                StartCoroutine(RevealAtqResult(timing, atqSuccess));
                timing += 0.4f;
            }
        }
        
        StartCoroutine(RevealDig(timing));
        timing += 0.2f;
        StartCoroutine(RevealDigAnimation(timing, newOperationDig));
    }
    
    IEnumerator RevealDef(float time)
    {
        yield return new WaitForSeconds(time);
 
        objDef.SetActive(true);
    }
    
    IEnumerator RevealDefResult(float time, bool success)
    {
        yield return new WaitForSeconds(time);

        textDefResult.text = success ? "RESISTED!" : "DEAD!";
        textDefResult.color = success ? new Color32(0xE0, 0x90 , 0x12, 0xFF) : new Color32(0x7A, 0x39, 0x21, 0xFF);
        textDefResult.gameObject.SetActive(true);
    }
    
    IEnumerator RevealAtq(float time)
    {
        yield return new WaitForSeconds(time);
 
        objAtq.SetActive(true);
    }
    
    IEnumerator RevealAtqResult(float time, bool success)
    {
        yield return new WaitForSeconds(time);

        textAtqResult.text = success ? "PASSED!" : "FAILED!";
        textAtqResult.color = success ? new Color32(0xE0, 0x90 , 0x12, 0xFF) : new Color32(0x7A, 0x39, 0x21, 0xFF);
        textAtqResult.gameObject.SetActive(true);
    }
    
    IEnumerator RevealDig(float time)
    {
        yield return new WaitForSeconds(time);
 
        objDig.SetActive(true);
    }
    
    IEnumerator RevealDigAnimation(float time, int newDigValue)
    {
        yield return new WaitForSeconds(time);
        
        LeanTween.value(gameObject, _operation.Dig, newDigValue, 0.8f).setEaseLinear().setOnUpdate((float val) =>
        {
            sliderDig.value = val;
            textDig.text = $"{Mathf.CeilToInt(val)}/{_operation.DigTotal}";
        }).setOnComplete(() =>
        {
            _operation.Dig = newDigValue;
            OnReported?.Invoke();
            StartCoroutine(CloseMe(0.6f));

            if (_operation.Dig == _operation.DigTotal)
            {
                GameManager.Singleton.FinishOperation(_operation);
            }
        });
 
        objDig.SetActive(true);
    }
    
    IEnumerator CloseMe(float time)
    {
        yield return new WaitForSeconds(time);
 
        canvasGroup.alpha = 0;
    }

    private void HideStuff()
    {
        objDef.SetActive(false);
        textDefResult.gameObject.SetActive(false);
        
        objAtq.SetActive(false);
        textAtqResult.gameObject.SetActive(false);
        
        objDig.SetActive(false);
    }
}
