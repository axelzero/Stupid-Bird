using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhatChoodStayWhitsCamera : MonoBehaviour {

    public List<GameObject> whatStayWitchCamera;
    private Vector3 mCameraCoordinats;
	
	void Update ()
    {
          ChangeCordinate();
    }


    void ChangeCordinate()
    {
        for (int i = 0; i <= whatStayWitchCamera.Count-1; i++)
        {
            whatStayWitchCamera[i].transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
            if (i == whatStayWitchCamera.Count)
            {
                i = 0;
            }
        }
    }
}
