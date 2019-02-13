using UnityEngine;
using System.Collections;

public class Girl : MonoBehaviour {

    public GameObject cameraMain;

    private Vector3 cameraMainTr;
    private float x;
    private float y = -7.3f;

    private AudioSource mAudioSource;

    void Start()
    {
        mAudioSource =gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        cameraMainTr = cameraMain.transform.position;
        x = cameraMainTr.x + 7f;
        transform.position = new Vector3(x, y, 0);


            GirlPrity();

    }

    public void GirlPrity()
    {
            y = -4f;
        if (PlayerPrefs.GetInt("MusOn") == 1)
        {
            mAudioSource.Stop();
        }
    }
}
