using UnityEngine;
using System.Collections;

public class CoinsForRoot : MonoBehaviour {
    public static CoinsForRoot Instance;
    [HideInInspector]
    public int coinForRootMenu;

    void Awake()
    {
        Instance = this;
    }

    void Start ()
    {
	
	}
	
	void Update ()
    {
        coinForRootMenu += PlayerPrefs.GetInt("CoinInRound");
        PlayerPrefs.SetInt("CoinForRootMenu", + coinForRootMenu);
	}
}
