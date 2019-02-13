using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInstatiate : MonoBehaviour
{
    public List<GameObject> playerPrefabs;
    private GameObject mPlayer;
    private int mWhatIsHero;

    void Awake()
    {
        mWhatIsHero = PlayerPrefs.GetInt("SchooseHeroNumber");
    }

    void Start ()
    {
        mPlayer = Instantiate(playerPrefabs[mWhatIsHero]);
        mPlayer.transform.parent = gameObject.transform;
        mPlayer.transform.position = gameObject.transform.position;
    }
}
