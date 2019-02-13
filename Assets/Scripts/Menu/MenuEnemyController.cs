using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuEnemyController : MonoBehaviour {

    public List<Transform> spawn;
    public List<GameObject> prefabs;

    private GameObject mEnemyClone;

    void Update()
    {
        if (RootMenu.Instance.mCurEnemys < RootMenu.Instance.maxEnemys)
        {
            mEnemyClone = Instantiate<GameObject>(prefabs[Random.Range(0, prefabs.Count)]);
            mEnemyClone.transform.position = spawn[Random.Range(0, spawn.Count)].transform.position;
            RootMenu.Instance.mCurEnemys++;
        }

    }
}
