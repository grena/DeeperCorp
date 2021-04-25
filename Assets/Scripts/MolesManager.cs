using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class MolesManager : MonoBehaviour
{
    [SerializeField] private GameObject molePrefab;
    [SerializeField] private GameObject container;
    [SerializeField] private Transform transformSpawn;
    [SerializeField] private Button recruitButton;
    [SerializeField] private List<Sprite> moleSprites;

    private List<MoleBehaviour> _moleBehaviours = new List<MoleBehaviour>();
    private MoleCreator _moleCreator = new MoleCreator();

    private void Start()
    {
        _moleCreator.sprites = moleSprites;
    }

    public void RecruitMole()
    {
        GameManager.Singleton.Stock.Caps -= 5;

        Mole mole = _moleCreator.Create();

        GameObject moleObj = Instantiate(molePrefab, transformSpawn.position, Quaternion.identity);
        moleObj.transform.SetParent(container.transform, false);
        
        MoleBehaviour moleBehaviour = moleObj.GetComponent<MoleBehaviour>();
        moleBehaviour.Setup(mole);
        moleBehaviour.linePosition = _moleBehaviours.Count + 1;

        _moleBehaviours.Add(moleBehaviour);

        recruitButton.interactable = _moleBehaviours.Count < 5;
    }
}
