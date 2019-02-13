using UnityEngine;
using System.Collections;

public class MenuEnemy : MonoBehaviour
{


    private Rigidbody2D mRb2d;
    private Vector3 mPosition;
    private Vector3 mPositionDel;

    public float speed;
    void Start()
    {
        mRb2d = GetComponent<Rigidbody2D>();
        mPositionDel.x = transform.position.x - 20f;
    }

    void Update()
    {
        mPosition = transform.position;
        mRb2d.AddForce(Vector2.left * speed, ForceMode2D.Force);
        if (mPositionDel.x >= mPosition.x)
        {
            RootMenu.Instance.mCurEnemys--;
            Destroy(gameObject);
        }
    }
}