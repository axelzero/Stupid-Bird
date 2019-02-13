using UnityEngine;
using System.Collections;

public class MenuEnemys : MonoBehaviour {

    private Rigidbody2D mRb2d;
    private Vector3 mPosition;

    public float speed;
    void Start()
    {
        mRb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        mPosition = transform.position;
        mRb2d.AddForce(Vector2.left * speed, ForceMode2D.Force);

        if (mPosition.x <= -10f)
        {
            RootMenu.Instance.mCurEnemys--;
            Destroy(gameObject);
        }
    }
}
