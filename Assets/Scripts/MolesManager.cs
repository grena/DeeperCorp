using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolesManager : MonoBehaviour
{
    [SerializeField] private GameObject molePrefab;
    [SerializeField] private GameObject container;
    [SerializeField] private Transform transformSpawn;

    public void RecruitMole()
    {
        GameManager.Singleton.Stock.Caps -= 5;

        GameObject moleObj = Instantiate(molePrefab, transformSpawn.position, Quaternion.identity);
        moleObj.transform.SetParent(container.transform, false);
    }
}
