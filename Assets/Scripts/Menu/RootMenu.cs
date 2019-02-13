using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class RootMenu : MonoBehaviour {
    public static RootMenu Instance;

    [HideInInspector]
    public CameraMoove camMove;

    [HideInInspector]
    public Enemy mEnemyCS;
    [HideInInspector]
    public EnemyController mEnemyContrCS;
    [HideInInspector]
    public int mScore;

    public int maxEnemys;
    [HideInInspector]
    public int mCurEnemys;

    private List<int> mScoresToUp;
    private AudioSource mAudioSource;
    public GameObject AudioSGO;
    public GameObject mButtonMute;
    public GameObject mButtonPlay;

    public Text bestScore;

    //Chose Level
    public GameObject LevelsUI;
    public GameObject MenuUI;
    //

    //Chose Hero
    public GameObject heroUIbtn;
    public GameObject heros;
    public GameObject leftButtonUI;
    public GameObject rightButtonUI;
    public GameObject spownPointsToOff;
    public GameObject okButtonHero;
    public GameObject buyButtonHero;

    private int mHeroNumber = 0;

    public List<GameObject> lockedImg;

    public GameObject noEthernetConnection;
    //

    //coins
    public Text coinUiText;
    [HideInInspector]
    public int coins;
    private int mCoinFromAllGame;

    public int costOfBird;
    public GameObject notAnoughtMoneyUI;
    public GameObject howMushNeedCoins;
    public int coinsForAds;
    //

    //Google servises
    //
    private const string first10Coins = "CgkInu7ap-ETEAIQAQ";
    private const string first100Coins = "CgkInu7ap-ETEAIQAg";
    private const string first1000Coins = "CgkInu7ap-ETEAIQCA";
    //
    private const string firstBird = "CgkInu7ap-ETEAIQAw";
    private const string secondBird = "CgkInu7ap-ETEAIQBA";
    private const string thirdBird = "CgkInu7ap-ETEAIQBQ";
    //
    private const string flyRound10Birds = "CgkInu7ap-ETEAIQCg";
    private const string flyRound25Birds = "CgkInu7ap-ETEAIQCw";
    private const string flyRound50Birds = "CgkInu7ap-ETEAIQDA";
    //

    private const string leaderBoardReachMan = "CgkInu7ap-ETEAIQBg"; 
    private const string leaderBoardAirMan = "CgkInu7ap-ETEAIQBw";
    //

    private int buyedBirdsNum;
    //

    void Awake()
    {
      //  PlayerPrefs.DeleteAll();
        Time.timeScale = 1;
        Instance = this;
        mEnemyCS = GetComponent<Enemy>();
        mEnemyContrCS = GetComponent<EnemyController>();

        camMove = GetComponent<CameraMoove>();
    }

    void Start()
    {
        //Google servises
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
        //

        CoinInRoot = PlayerPrefs.GetInt("CoinForRootMenu");
        coinUiText = coinUiText.GetComponent<Text>();

        heros.SetActive(false);
        int bestScoreint = PlayerPrefs.GetInt("BestScore");
        bestScore.text = "Best Score: " + bestScoreint.ToString();

        mAudioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("MusOn") == 0)
        {
            mAudioSource.Play();
            mButtonPlay.SetActive(false);
            mButtonMute.SetActive(true);
            AudioSGO.SetActive(true);
        }
        else
        {
            mAudioSource.Stop();
            mButtonMute.SetActive(false);
            mButtonPlay.SetActive(true);
            AudioSGO.SetActive(false);
        }
    }

    void Update()
    {
        IsbuttonNeed(mHeroNumber);
        OnCoinEnter();

        if (heroUIbtn != false)
        {
            if (mHeroNumber == 0)
            {
                okButtonHero.SetActive(true);
                buyButtonHero.SetActive(false);
            }
            else if (mHeroNumber != 0 && PlayerPrefs.GetInt((mHeroNumber) + "Hero") != 1)
            {
                okButtonHero.SetActive(false);
                buyButtonHero.SetActive(true);
            }
            else if (mHeroNumber == 1 && PlayerPrefs.GetInt((mHeroNumber) + "Hero") == 1)
            {
                okButtonHero.SetActive(true);
                buyButtonHero.SetActive(false);
                lockedImg[mHeroNumber - 1].SetActive(false);
            }
            else if (mHeroNumber == 2 && PlayerPrefs.GetInt((mHeroNumber) + "Hero") == 1)
            {
                okButtonHero.SetActive(true);
                buyButtonHero.SetActive(false);
                lockedImg[mHeroNumber - 1].SetActive(false);
            }
            else if (mHeroNumber == 3 && PlayerPrefs.GetInt((mHeroNumber) + "Hero") == 1)
            {
                okButtonHero.SetActive(true);
                buyButtonHero.SetActive(false);
                lockedImg[mHeroNumber -1].SetActive(false);
            }
        }
    }

    void OnCoinEnter()
    {
        coinUiText.text = "x " + coins.ToString();
    }

    public void ChoseLevel()
    {
        MenuUI.SetActive(false);
        LevelsUI.SetActive(true);
    }

    public void ChoseHero()
    {
        heroUIbtn.SetActive(true);
        heros.SetActive(true);
        MenuUI.SetActive(false);
        spownPointsToOff.SetActive(false);
        
    }

    public void btnOk()
    {
        PlayerPrefs.SetInt("SchooseHeroNumber", mHeroNumber);
        Debug.Log(mHeroNumber +" number");
        heroUIbtn.SetActive(false);
        heros.SetActive(false);
        MenuUI.SetActive(true);
        spownPointsToOff.SetActive(true);
    }

    public void ExitGame()
    {
      //  PlayGamesPlatform.Instance.SignOut();
        Application.Quit();
    }

    public void BtnWorld1()
    {
        Application.LoadLevel(1);
    }

    public void BtnWorld2()
    {
        Application.LoadLevel(2);
    }

    public void BtnBack()
    {
        MenuUI.SetActive(true);
        LevelsUI.SetActive(false);
    }

    public void BtnBuy()
    {
        //Купить птичку за coins
        if (CoinInRoot >= costOfBird)
        {
            buyedBirdsNum++;
            if (buyedBirdsNum == 1)
            {
                GetAchievement(firstBird);
            }
            else if (buyedBirdsNum == 2)
            {
                GetAchievement(secondBird);
            }
            else if (buyedBirdsNum == 3)
            {
                GetAchievement(thirdBird);
            }
            PlayerPrefs.SetInt("CoinForRootMenu", CoinInRoot - costOfBird);
            CoinInRoot = PlayerPrefs.GetInt("CoinForRootMenu");
            PlayerPrefs.SetInt(mHeroNumber + "Hero", 1);
            // battton ok ON  buy off
            okButtonHero.SetActive(true);
            buyButtonHero.SetActive(false);
        }
        else
        {
            //Окно где сообщение, что не хватает денег
            notAnoughtMoneyUI.SetActive(true);
            Text text;
           // int myCoins;
           // myCoins = PlayerPrefs.GetInt("CoinForRootMenu");
            text = howMushNeedCoins.GetComponent<Text>();
            text.text = "You need " + (costOfBird - coins) + " coins";
        }
    }
    //Если птица не куплена
    public void IsBuyed()
    {
        for(int i = 0; i <= lockedImg.Count; i++)
        {

        }
    }
    //
    //Для выхода из сообщения о не достатке денег

    public void BtnOkExit()
    {
        notAnoughtMoneyUI.SetActive(false);
        noEthernetConnection.SetActive(false);
    }

    //Для выхода из сообщения о не достатке денег
    
    //Для просмотра рекламы
    public void btnGetMoney()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult});
        }
    }
    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                coins += coinsForAds;
                PlayerPrefs.SetInt("CoinForRootMenu", coins);
                if (coins >= 10)
                {
                    GetAchievement(first10Coins);
                }
                if (coins >= 100)
                {
                    GetAchievement(first100Coins);
                }
                if (coins >= 1000)
                {
                    GetAchievement(first1000Coins);
                }
                Social.ReportScore(PlayerPrefs.GetInt("CoinForRootMenu"), leaderBoardReachMan, (bool success) =>
                {
                    if (success)
                    {
                        Debug.Log("Добавлен в таблицу лидеров");
                    }
                });
                BtnOkExit();
                break;
            case ShowResult.Skipped:
                Debug.Log("Skipped");
                break;
            case ShowResult.Failed:
                //Зделать UI с сообщением, что бы включили инет
                noEthernetConnection.SetActive(true);
                break;
        }
    }
    //Для просмотра рекламы
    public void Mute()
    {
        mAudioSource.Stop();
        mButtonMute.SetActive(false);
        mButtonPlay.SetActive(true);
        PlayerPrefs.SetInt("MusOn", 1);
        AudioSGO.SetActive(false);
    }
    public void SoundOn()
    {
        mAudioSource.Play();
        mButtonPlay.SetActive(false);
        mButtonMute.SetActive(true);
        PlayerPrefs.SetInt("MusOn", 0);
        AudioSGO.SetActive(true);
    }


    public void BtnLeftHero()
    {
        Debug.Log("Left");
        heros.transform.position += new Vector3(15f, 0f, 0f);
        // Если куплен
        //if (buyed)
        //{
            mHeroNumber--;
        //}
    }
    public void BtnRighttHero()
    {
        Debug.Log("Right");
        heros.transform.position += new Vector3(-15f, 0f, 0f);
        // Если куплен
        //if (buyed)
        //{
            mHeroNumber++;
        //}
    }

    void IsbuttonNeed(int mNumber)
    {
        if (mNumber == 0)
        {
            //откл. кнопку вправо
            leftButtonUI.SetActive(false);
        }
        else if (mNumber == 3)
        {
            //откл. кнопку влево
            rightButtonUI.SetActive(false);
        }
        else if (mNumber == 1)
        {
            leftButtonUI.SetActive(true);
        }
        else if (mNumber == 2)
        {
            rightButtonUI.SetActive(true);
        }
    }


    public int CoinInRoot
    {
        get
        {
            return coins;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            coins = value;
        }
    }
    //Google servises
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
    //

    public void BtnOshivki()
    {
        Social.ShowAchievementsUI();
    }
    public void BtnLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
}