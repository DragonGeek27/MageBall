using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellPull : MonoBehaviour
{

    private Transform spellLocation;
    //public GameObject spellLocationGO;
    public float speed;
    //OVRGrabbable scrips
    public OVRGrabbable otherScript;

    //public GameObject spell;

    // Use this for initialization
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        spellLocation = GameObject.Find("SpellSpawn").transform;//fix later
        //if you grab spell set gravity to true and disabe tracking speed
        if (otherScript.isGrabbed == true)
        {
            speed = 0;
            gameObject.GetComponent<Rigidbody>().useGravity = true;

        }
  

        //spell follow book
        else
        {
            transform.position = Vector3.Lerp(transform.position, spellLocation.position, speed * Time.deltaTime);
            //gameObject.GetComponent<Rigidbody>().useGravity = false;
            //Debug.Log("GF");


        }

    }

}
