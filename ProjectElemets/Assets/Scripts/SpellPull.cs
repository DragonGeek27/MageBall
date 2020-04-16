using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class SpellPull : NetworkBehaviour
{

    private Transform spellLocation;
    //public GameObject spellLocationGO;
    public float speed;
    //OVRGrabbable scrips
    public OVRGrabbable ovrGrabbable;
    public Exploshon exploshonScript;
    public bool grabbed = false;


    //public GameObject spell;

    void Start()
    {

        //GetComponent(Exploshon).enabled = false;
        //get OVRGrabbable scrips from spell ball
        ovrGrabbable = gameObject.GetComponent<OVRGrabbable>();
        grabbed = false;
        if (exploshonScript != null)
        {

            exploshonScript.enabled = false;
        }
        else
        {

        }
        speed = 1000;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(grabbed);

        spellLocation = GameObject.Find("SpellSpawn").transform;//fix later
        //if you grab spell set gravity to true and disabe tracking speed
        if (ovrGrabbable.isGrabbed == true)
        {
            Debug.Log("test" + hasAuthority);
            grabbed = true;

            transform.parent = null;
            speed = 0;
            gameObject.GetComponent<Rigidbody>().useGravity = true;

            CmdGrabed();


        }
        else if ((ovrGrabbable.isGrabbed == false) && (grabbed == true))
        {

            CmdGrabed();
            if (exploshonScript != null)
            {
                exploshonScript.enabled = true;


            }
            else
            {

            }
        }
        //if (oculusRemote.ShootList.Contains(gameObject))
        //{
        //    transform.position = Vector3.Lerp(transform.position, spellLocation.position, speed * Time.deltaTime);
        //    gameObject.GetComponent<Rigidbody>().useGravity = false;

        //}


        //spell follow book
        else if ((ovrGrabbable.isGrabbed == false) && (grabbed == false))
        {
            //gameObject.transform.position = transform.parent.gameObject.transform.position;
        }
        else
        {

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "SpellSpawn")
        {
            transform.SetParent(other.gameObject.transform);
        }

    }
    [Command]
    void CmdGrabed()
    {
        if (exploshonScript == null)
        {
            return;
        }
        else
        {
            exploshonScript.enabled = true;
            transform.parent = null;
        }
    }

}
