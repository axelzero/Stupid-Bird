using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public List<Transform> spawn;
    public List<GameObject> prefabs;

    private GameObject mEnemyClone;

	void Update ()
    {
        if(Root.Instance.State == Root.Estate.Play)
        {

            if (Root.Instance.howManyEnemyNow < Root.Instance.maxEnemysInGame)
            {
                mEnemyClone = Instantiate<GameObject>(prefabs[Random.Range(0, prefabs.Count)]);
                mEnemyClone.transform.position = spawn[Random.Range(0, spawn.Count)].transform.position;
                //Root.Instance.mCurEnemys++;
                Root.Instance.howManyEnemyNow++;
            }
        }
    }
}
