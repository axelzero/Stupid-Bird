using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    private Rigidbody2D mRb2d;
    private Vector3 mPosition;
    private Vector3 mPositionDel;
    private int mScore;

    public float speed;
    void Start()
    {
        mRb2d = GetComponent<Rigidbody2D>();
        mPositionDel.x = transform.position.x- 20f;
    }

    void Update()
    {
        mPosition = transform.position;
        mRb2d.AddForce(Vector2.left * speed, ForceMode2D.Force);
        if (Root.Instance.State == Root.Estate.Play)
        {
            if (mPositionDel.x >= mPosition.x)
            {
                Root.Instance.howManyEnemyNow--;
                Root.Instance.mScore += 1;
                Destroy(gameObject);
            }
        }
        if (Root.Instance.State == Root.Estate.GameOver)
        {
            if (mPositionDel.x >= mPosition.x)
            {
                Root.Instance.howManyEnemyNow--;
              //  Root.Instance.mCurEnemys--;
                Destroy(gameObject);
            }
        }
        

    }
}