using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Join : MonoBehaviour {

    //public OVRGrabbable grabbable;
    public JoinButton joinMatch;
    private bool join;
    // Use this for initialization
    void Start()
    {
        join = true;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (grabbable.isGrabbed == true)
    //    {
    //        if (join == true)
    //        {
    //            joinMatch.JoinMatch();
    //            join = false;
    //        }
    //    }

    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (join == true)
            {
                joinMatch.JoinMatch();
                join = false;
            }
        }
    }
}
