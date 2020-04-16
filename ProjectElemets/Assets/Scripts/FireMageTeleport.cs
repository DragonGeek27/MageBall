
using UnityEngine;
using UnityEngine.Networking;

public class FireMageTeleport : NetworkBehaviour
{
    public GameObject fireMage;
    public GameObject smoke;
    public float efectTime;
    public OVRGrabbable otherScript;
    public bool setParent;
    public bool setMage;
    public AudioClip correct;
    public AudioClip inorrect;
    public void Start()
    {
        //sync up obect spawn over network
        this.enabled = true;
        // set fire mage to parent object localy
        fireMage = transform.root.gameObject;
        setMage = true;
        CmdStartTeleport();
    }
    private void Update()
    {
        //set obect to teleport to player
        if (setMage == true)
        {
            //Debug.Log(3);
            fireMage = transform.root.gameObject;
            setMage = false;
        }
        //register you grabbed the ball
        if (setMage == false)
        {
            //Debug.Log(4);
            if (otherScript.isGrabbed == true)
            {
                Debug.Log(5);
                //Debug.Log(transform.root.gameObject);
                // conferm parent object is set
                setParent = true;
                CmdUpdateTeleport();
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log(6);


        GameObject hit = other.gameObject;
        OculusRemote oculusRemote = hit.GetComponent<OculusRemote>();
        if (setParent == true)
        {
            //Debug.Log(7);
            if (oculusRemote != null)
            {
                //if object is in player list dont trigger
                Debug.Log(8);
                //keep teleprt spell from hitting self
                if (oculusRemote.ShootList.Contains(gameObject))
                {
                    Debug.Log(9);
                    return;

                }

                else
                {//teleport local player
                    Debug.Log(10);
                    CmdTeleport();
                    // set the players new position on the cliant side
                    fireMage.GetComponent<CharacterController>().enabled = false;
                    fireMage.transform.position = new Vector3(transform.position.x,
                        transform.position.y + 1,
                        transform.position.z);
                    //this.enabled = false;
                    //NetworkServer.Destroy(gameObject);
                    fireMage.GetComponent<CharacterController>().enabled = true;


                }
            }
            // keep teleprt spell from hitting  other spells
            else if ((oculusRemote == null && other.gameObject.layer == 9) || (oculusRemote == null && other.gameObject.layer == 10))
            {
                Debug.Log(11);
                return;
            }
            else
            {//teleport local player
                Debug.Log(12);
                CmdTeleport();
                fireMage.GetComponent<CharacterController>().enabled = false;

                fireMage.transform.position = new Vector3(transform.position.x,
                    transform.position.y + 1,
                    transform.position.z);
                //this.enabled = false;
                //NetworkServer.Destroy(gameObject);
                fireMage.GetComponent<CharacterController>().enabled = true;

            }
        }
    }

    [Command]
    void CmdStartTeleport()
    {
        //Debug.Log(13);
        // set fire mage to parent object over the network
        fireMage = transform.root.gameObject;
        this.enabled = true;
    }
    [Command]
    void CmdUpdateTeleport()
    {
        Debug.Log(14);
        setParent = true;
    }
    //sync teleport over the network
    [Command]
    void CmdTeleport()
    {
        // teleport player over the network and create a smokescreen effect
        Debug.Log(15);
        //foreach (var fireMage in goList)
        {
            OculusRemote oculusRemote = fireMage.GetComponent<OculusRemote>();
            if (this.enabled)

            {
                Debug.Log(16);
                GameObject smokeScreen = Instantiate(smoke, fireMage.transform.position, Quaternion.identity) as GameObject;
                //smokeScreen.GetComponent<Transform>().localScale = gameObject.GetComponent<Transform>().localScale;
                NetworkServer.Spawn(smokeScreen);
                if (fireMage.GetComponent<OculusRemote>().ShootList.Contains(gameObject))
                {
                    Debug.Log(17);
                    fireMage.GetComponent<CharacterController>().enabled = false;

                    fireMage.transform.position = new Vector3(transform.position.x,
                            transform.position.y + 1,
                            transform.position.z);
                    this.enabled = false;
                    NetworkServer.Destroy(gameObject);
                    Destroy(gameObject);

                    fireMage.GetComponent<CharacterController>().enabled = false;
                }

                else
                {
                    Debug.Log(18);
                    return;
                }
                Destroy(smokeScreen, efectTime);
            }

        }
    }

}


