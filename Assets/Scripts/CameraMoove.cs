using UnityEngine;
using System.Collections;

public class CameraMoove : MonoBehaviour {

    //public GameObject mainCamera;

	void Start ()
    {

    }
	

	void Update ()
    {
        transform.position += new Vector3(1,0,0) * Time.deltaTime;
	}
}
