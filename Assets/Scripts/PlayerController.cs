using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public enum Estate {Playing, GameOver };
    public Estate State;

    public AudioClip audioClip;
    private AudioSource mAudioSource;

    private BoxCollider2D mBoxColl;

    private Rigidbody2D mRb2d;
    private Vector3 mPosition;
   // private float x;
   // private float y;

    private Vector2 positionHero;

    public float speed;

    private float timerFly;
    void Start()
    {
        mBoxColl = GetComponent<BoxCollider2D>();
        mAudioSource = GetComponent<AudioSource>();

        mRb2d = GetComponent<Rigidbody2D>();
       // x = Input.GetAxis("Horizontal");
      //  y = Input.GetAxis("Vertical");
    }


    void Update()
    {
        positionHero.x = transform.position.x;
        mPosition = transform.position;
        if (Root.Instance.State == Root.Estate.Tutorial)
        {
            mRb2d.isKinematic = true;
        }

        if (mPosition.y <= -5f)
        {
            State = Estate.GameOver;
        }
        if (State == Estate.GameOver)
        {
            Root.Instance.State = Root.Estate.GameOver;
        }
        if (Root.Instance.State == Root.Estate.Play)
        {
            mRb2d.isKinematic = false;
            if (mPosition.y <= 4.5f)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began/*Input.GetKeyDown(KeyCode.Space)*/ && mPosition.y <= 3.6f)
                {
                    timerFly += Time.deltaTime;
                    if(timerFly < 0.5f)
                    {
                        mRb2d.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
                        timerFly = 0;
                    }
                    
                }
            }
            if (mPosition.y >= 4.5f)
            {
                transform.position = new Vector2(positionHero.x, 4.5f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            mBoxColl.enabled = false;
            mAudioSource.clip = audioClip;
            if (PlayerPrefs.GetInt("MusOn") == 0)
            {
                mAudioSource.Play();
            }
            State = Estate.GameOver;
        }

        if (coll.gameObject.tag == "Coin")
        {
            Root.Instance.coin += 1;
            Root.Instance.curCoinInGame--;
            Destroy(coll.gameObject);
        }
    }
}
