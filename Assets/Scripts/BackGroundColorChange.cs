using UnityEngine;
using System.Collections;

public class BackGroundColorChange : MonoBehaviour {

    public Camera cameraGO;

    private Animator anim;
    private float mTimerDay;
    public float dayTime;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {


            TimerDay += Time.deltaTime;

            if (TimerDay > (dayTime/2))
            {
                Day();
            }

            if ((TimerDay > 0f) &&TimerDay < (dayTime / 2))
            {
                Night();
            }
    }


    public void Day()
    {
            anim.SetInteger("DayOrNight", 1);
    }

    public void Night()
    {
            anim.SetInteger("DayOrNight", 0);
    }

    public float TimerDay
    {
        get
        {
            return mTimerDay;
        }
        set
        {
            if (value > dayTime*1.5f)
            {
                value = 0;
            }
            mTimerDay = value;
        }
    }
}
