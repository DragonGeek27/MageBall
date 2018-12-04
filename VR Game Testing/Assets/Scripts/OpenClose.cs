using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClose : MonoBehaviour
{
    public Animator doorAnimator;
    public OVRGrabbable otherScript;
    public bool door;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        door = doorAnimator.GetBool("Grabbed");
        if (otherScript.isGrabbed == true)
        {
            OpenDoor();

        }
    }
    void OpenDoor()
    {
        //Debug.Log("lllllllllllllllllllllllllllllllllllll");
        if (door == true)
        {
            doorAnimator.SetBool("Grabbed", false);
        }

        else if (door == false)
        {
            doorAnimator.SetBool("Grabbed", true);
        }

    }
}
