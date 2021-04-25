using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class MoleBehaviour : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TMP_Text textDig;
    [SerializeField] private TMP_Text textAtq;
    [SerializeField] private TMP_Text textDef;
    [SerializeField] private List<GameObject> iconsMasterLevel;

    private bool _ready;
    private bool _inside;
    
    public int linePosition = 5;

    private Dictionary<int, Transform> linePositions = new Dictionary<int, Transform>();
    private Transform posBord;
    private Transform posInside;
    
    public Mole mole;
    public Action OnReady;
    public Action OnInside;

    private float _speed;

    public string finalMove = "";
    
    public bool Ready
    {
        get => _ready;
        set
        {
            _ready = value;
            
            if (_ready)
            {
                ToggleStats(true);
                OnReady?.Invoke();
            }
        }
    }

    public bool Inside
    {
        get => _inside;
        set
        {
            _inside = value;
            
            if (_inside)
            {
                OnInside?.Invoke();
            }
        }
    }

    void Start()
    {
        _speed = Random.Range(0.5f, 0.9f);
        canvasGroup.alpha = 0;
        
        posBord = GameObject.Find("MolesPositions/PositionBord").transform;
        posInside = GameObject.Find("MolesPositions/PositionInside").transform;
        
        linePositions[1] = GameObject.Find("MolesPositions/Position 1").transform;
        linePositions[2] = GameObject.Find("MolesPositions/Position 2").transform;
        linePositions[3] = GameObject.Find("MolesPositions/Position 3").transform;
        linePositions[4] = GameObject.Find("MolesPositions/Position 4").transform;
        linePositions[5] = GameObject.Find("MolesPositions/Position 5").transform;
    }

    // Update is called once per frame
    void Update()
    {
        switch (finalMove)
        {
            case "bord":
            {
                if (Vector3.Distance(transform.position, posBord.position) > 0.001f)
                {
                    float step = _speed * Time.deltaTime; // calculate distance to move
                    transform.position = Vector3.MoveTowards(transform.position, posBord.position, step);
                }
                else
                {
                    finalMove = "inside";
                }

                return;
            }
            case "inside":
            {
                if (Vector3.Distance(transform.position, posInside.position) > 0.001f)
                {
                    float step = _speed * Time.deltaTime; // calculate distance to move
                    transform.position = Vector3.MoveTowards(transform.position, posInside.position, step);
                }
                else
                {
                    if (!Inside) Inside = true;
                }

                return;
            }
        }

        if (Vector3.Distance(transform.position, linePositions[linePosition].position) > 0.001f)
        {
            float step = _speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, linePositions[linePosition].position, step);
        }
        else
        {
            if (linePosition == 1 && !Ready)
            {
                Ready = true;    
            }
        }
    }

    public void LetsDig()
    {
        ToggleStats(false);
        finalMove = "bord";
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
        if (Ready) return;
        
        ToggleStats(true);
    }

    private void OnMouseExit()
    {
        if (Ready) return;
        
        ToggleStats(false);
    }

    private void ToggleStats(bool visible)
    {
        canvasGroup.alpha = visible ? 1 : 0;
    }
}
