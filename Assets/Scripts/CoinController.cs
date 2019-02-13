using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinController : MonoBehaviour {
    //Coin
    private GameObject mCoinClone;
    public GameObject coinPrefab;
    public List<Transform> spawn;
    //
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpawnCoin();
    }

    void SpawnCoin()
    {
        if (Root.Instance.State == Root.Estate.Play)
        {

            if (Root.Instance.curCoinInGame < Root.Instance.howMuchCoinsInGame)
            {
                mCoinClone = Instantiate<GameObject>(coinPrefab);
                mCoinClone.transform.position = spawn[Random.Range(0, spawn.Count)].transform.position;
                Root.Instance.curCoinInGame++;
            }
        }
    }
}
