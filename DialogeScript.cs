using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogeScript : MonoBehaviour
{
    GameObject mcCam;
    // Start is called before the first frame update
    void Start()
    {
        //sets the mcCam to the game object CamFollowingMCAfterTimeline
        mcCam = GameObject.Find("CamFollowingMCAfterTimeline");

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position.Set(mcCam.transform.position.x, mcCam.transform.position.y, 0);
        //print(mcCam.transform.position.x);
        //print(mcCam.transform.position.x);
        //print(mcCam.transform.position.y);
    }
}
