using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlaySer : MonoBehaviour {

    private const string first10Coins= "CgkInu7ap-ETEAIQAQ";
    private const string first100Coins = "CgkInu7ap-ETEAIQAg";
    private const string firstBird = "CgkInu7ap-ETEAIQAw";
    private const string secondBird = "CgkInu7ap-ETEAIQBA";
    private const string thirdBird = "CgkInu7ap-ETEAIQBQ";


    void Start ()
    {
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => {
            if (success)
            {
                Debug.Log("Enter");
            }
            else
            {
                Debug.Log("Not enter");
            }
        });
	}

    private void GetAchievement(string id)
    {
        Social.ReportProgress(id, 100.0f, (bool success) =>
        {
            if (success)
            {
                Debug.Log("Полученно достижение: " + id);
            }
        });
    }
}
