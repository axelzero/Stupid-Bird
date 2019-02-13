using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class Root : MonoBehaviour {
    //Girl
    public GameObject girl;
    private bool oneAtRoundTime = true;
    //

    public static Root Instance;

    public enum Estate { Tutorial, Play, GameOver };

    public Estate State;

    public int levelNumber;
    public GameObject gameOverUI;
    public Text gameoverScore;
    private Text mTextScoreGameOver;

    [HideInInspector]
    public Enemy mEnemyCS;
    [HideInInspector]
    public EnemyController mEnemyContrCS;
    public Text score;
    [HideInInspector]
    public int mScore;
    public Text bestScore;
    private int mBestScore;
    //public int maxEnemys;
  // [HideInInspector]
   // public int mCurEnemys;

    //overvride
    public int maxEnemysInGame = 1;
    public int howManyEnemyNow;
    //

   // private List<int> mScoresToUp;
    private AudioSource mAudioSource;
  //  private PlayerController mPlayerContr;

    //Tutorail
    public GameObject tutorialUi;
    //

    //coins
    public Text coinUiText;
    [HideInInspector]
    public int coin;
    private int mLastCoinRound;

    [HideInInspector]
    public int curCoinInGame;
    public int howMuchCoinsInGame = 1;
    private bool mOneTime;
    //


    //Google servises
    //
    private const string first10Coins = "CgkInu7ap-ETEAIQAQ";
    private const string first100Coins = "CgkInu7ap-ETEAIQAg";
    private const string first1000Coins = "CgkInu7ap-ETEAIQCA";

    private const string flyRound10Birds = "CgkInu7ap-ETEAIQCg";
    private const string flyRound25Birds = "CgkInu7ap-ETEAIQCw";
    private const string flyRound50Birds = "CgkInu7ap-ETEAIQDA";
    //
    private const string firstloose = "CgkInu7ap-ETEAIQCQ";

    private const string leaderBoardReachMan = "CgkInu7ap-ETEAIQBg";
    private const string leaderBoardAirMan = "CgkInu7ap-ETEAIQBw";
    //
    //

    void Awake()
    {
        Time.timeScale = 1;
        Instance = this;
        mEnemyCS = GetComponent<Enemy>();
        mEnemyContrCS = GetComponent<EnemyController>();
    }

	void Start ()
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

        //  mLastCoinRound = PlayerPrefs.GetInt(CoinInRound);
        //  PlayerPrefs.SetInt("BestScore", 0);
        //   mPlayerContr = gameoverScore.GetComponent<PlayerController>();

        coinUiText = coinUiText.GetComponent<Text>();

        mTextScoreGameOver = gameoverScore.GetComponent<Text>();

        mAudioSource = GetComponent<AudioSource>();
        score = score.GetComponent<Text>();
       // mScoresToUp = new List<int>();
        if (PlayerPrefs.GetInt("MusOn") == 0)
        {
            mAudioSource.Play();
        }
        else
        {
            mAudioSource.Stop();
        }
         
    }
	
	void Update ()
    {

        if (State == Estate.Tutorial)
        {
            tutorialUi.SetActive(true);
            if (Input.touchCount > 0)
            {
                State = Estate.Play;
                tutorialUi.SetActive(false);

            }
        }
        if (State == Estate.Play)
        {
            PlayerPrefs.SetInt("BestScoreInRound", mScore);
            if (oneAtRoundTime)
            {
                if (PlayerPrefs.GetInt("BestScoreInRound") > PlayerPrefs.GetInt("BestScore") && PlayerPrefs.GetInt("BestScoreInRound") >10  )
                {
                    girl.SetActive(true);
                    Destroy(girl, 2f);
                    oneAtRoundTime = false;
                }
            }
            score.text = mScore.ToString();
            coinUiText.text = "x " + coin.ToString();

            if (mScore >= 10)
            {
                GetAchievement(flyRound10Birds);
            }
            if (mScore >= 25)
            {
                GetAchievement(flyRound25Birds);
            }
            if (mScore >= 50)
            {
                GetAchievement(flyRound50Birds);
            }
        }
            if (State == Estate.GameOver)
        {
            GameOver();
        }


        if (mScore >= 5)
        {
            maxEnemysInGame = 2;
            //maxEnemys = 2;
        }

        if (mScore >= 15)
        {
            maxEnemysInGame = 3;
            // maxEnemys = 3;
        }
        if (mScore >= 30)
        {
            maxEnemysInGame = 4;
            // maxEnemys = 4;
        }
        if (mScore >= 50)
        {
            maxEnemysInGame = 5;
            // maxEnemys = 5;
        }
        if (mScore >= 70)
        {
            maxEnemysInGame = 6;
            // maxEnemys = 6;
        }
        if (mScore >= 95)
        {
            maxEnemysInGame = 7;
            // maxEnemys = 7;
        }
        if (mScore >= 115)
        {
            maxEnemysInGame = 8;
            //  maxEnemys = 8;
        }
        if (mScore >= 145)
        {
            maxEnemysInGame = 9;
            //   maxEnemys = 9;
        }
        if (mScore >= 180)
        {
            maxEnemysInGame = 10;
            //   maxEnemys = 10;
        }
    }

    void GameOver()
    {

        gameOverUI.SetActive(true);
        mTextScoreGameOver.text ="Your score: "+ mScore.ToString();
        int mScorelocal = PlayerPrefs.GetInt("BestScore");
        bestScore.text = "Best Score: " + mScorelocal;

        if(mOneTime == false)
        {
            GetAchievement(firstloose);
            //Лидер по времени
            Social.ReportScore(mScorelocal, leaderBoardAirMan, (bool success) =>
              {
                  if (success)
                  {
                      Debug.Log("Добавлен в таблицу лидеров");
                  }
              });

            
            if (PlayerPrefs.GetInt("BestScoreInRound") >= 10)
            {
                GetAchievement(first10Coins);
            }

            if (PlayerPrefs.GetInt("BestScoreInRound") >= 100)
            {
                GetAchievement(first100Coins);
            }
            if (PlayerPrefs.GetInt("BestScoreInRound") >= 1000)
            {
                GetAchievement(first1000Coins);
            }

            if (PlayerPrefs.GetInt("BestScoreInRound") > PlayerPrefs.GetInt("BestScore"))
            {
                mBestScore = mScore;
                PlayerPrefs.SetInt("BestScore", mBestScore);
            }

            int fromRootMenu = PlayerPrefs.GetInt("CoinForRootMenu");
            PlayerPrefs.SetInt("CoinForRootMenu", fromRootMenu + coin);
            //Лидер по деньгам
            Social.ReportScore(PlayerPrefs.GetInt("CoinForRootMenu"), leaderBoardReachMan, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Добавлен в таблицу лидеров");
                }
            });
            mOneTime = true;
        }

    }



    public void BtnGoMenu()
    {
        Application.LoadLevel(0);
    }
    public void BtnPlayAgain()
    {
        Application.LoadLevel(levelNumber);
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
}
