using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoleBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TMP_Text textDig;
    [SerializeField] private TMP_Text textAtq;
    [SerializeField] private TMP_Text textDef;
    [SerializeField] private List<GameObject> iconsMasterLevel;
    
    public int linePosition = 5;

    private Dictionary<int, Transform> linePositions = new Dictionary<int, Transform>();
    public Mole mole;
    
    void Start()
    {
        canvasGroup.alpha = 0;
        
        linePositions[1] = GameObject.Find("MolesPositions/Position 1").transform;
        linePositions[2] = GameObject.Find("MolesPositions/Position 2").transform;
        linePositions[3] = GameObject.Find("MolesPositions/Position 3").transform;
        linePositions[4] = GameObject.Find("MolesPositions/Position 4").transform;
        linePositions[5] = GameObject.Find("MolesPositions/Position 5").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 0.1f;
        // Move our position a step closer to the target.


        if (Vector3.Distance(transform.position, linePositions[linePosition].position) > 0.001f)
        {
            float step =  speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, linePositions[linePosition].position, step);
        }
    }

    public void Setup(Mole m)
    {
        mole = m;

        spriteRenderer.sprite = mole.Sprite;
        textDig.text = mole.Dig.ToString();
        textAtq.text = mole.Atq.ToString();
        textDef.text = mole.Def.ToString();

        int marked = 3;
        foreach (GameObject iconMaster in iconsMasterLevel)
        {
            if (marked > mole.masterLevel)
            {
                iconMaster.SetActive(false);
                marked--;
            }
        }
    }

    private void OnMouseEnter()
    {
        canvasGroup.alpha = 1;
    }

    private void OnMouseExit()
    {
        canvasGroup.alpha = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        canvasGroup.alpha = 1;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canvasGroup.alpha = 0;
    }
}
