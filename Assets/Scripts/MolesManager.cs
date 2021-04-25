using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private OperationListBehaviour operationListBehaviour;

    public List<MoleBehaviour> moleBehaviours = new List<MoleBehaviour>();
    private MoleCreator _moleCreator = new MoleCreator();

    private void Start()
    {
        _moleCreator.sprites = moleSprites;
    }

    public void RecruitMole()
    {
        int cost = 5;

        if (GameManager.Singleton.Stock.Caps < cost) return;
        
        GameManager.Singleton.Stock.Caps -= cost;

        Mole mole = _moleCreator.Create();

        GameObject moleObj = Instantiate(molePrefab, transformSpawn.position, Quaternion.identity);
        moleObj.transform.SetParent(container.transform, false);
        
        MoleBehaviour moleBehaviour = moleObj.GetComponent<MoleBehaviour>();
        moleBehaviour.Setup(mole);
        moleBehaviour.linePosition = moleBehaviours.Count + 1;
        moleBehaviour.OnReady += OnMoleReady;

        moleBehaviours.Add(moleBehaviour);

        recruitButton.interactable = moleBehaviours.Count < 5;
    }

    public void EndExpedition()
    {
        // Remove mole behaviour 1
        MoleBehaviour launchedMole = moleBehaviours.First(behaviour => behaviour.linePosition == 1);
        moleBehaviours.Remove(launchedMole);
        Destroy(launchedMole.gameObject);

        // Advance all other ones
        foreach (MoleBehaviour moleBehaviour in moleBehaviours)
        {
            moleBehaviour.linePosition--;
        }
        
        recruitButton.interactable = moleBehaviours.Count < 5;
    }

    private void OnMoleReady()
    {
        operationListBehaviour.ToggleVisibility(true);
    }
}
