using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireMageTeleport : NetworkBehaviour
{
    public GameObject fireMage;
    public GameObject smoke;
    public float efectTime;
    public OVRGrabbable otherScript;
    public bool setParent;
    public bool test;
    public void Start()
    {
        //Debug.Log(1);
        //Debug.Log(fireMage + "=" + transform.root.gameObject);
        this.enabled = true;
        fireMage = transform.root.gameObject;
        //RpcStartTeleport();
        CmdStartTeleport();
        test = true;
    }
    private void Update()
    {
        //Debug.Log(2);
        if (test == true)
        {
            //Debug.Log(3);
            fireMage = transform.root.gameObject;
            test = false;
        }
        //Debug.Log(fireMage + "=" + transform.root.gameObject);
        if (test == false)
        {
            //Debug.Log(4);
            if (otherScript.isGrabbed == true)
            {
                Debug.Log(5);
                //Debug.Log(transform.root.gameObject);
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
                Debug.Log(8);
                if (oculusRemote.ShootList.Contains(gameObject))
                {
                    Debug.Log(9);
                    return;

                }

                else
                {
                    Debug.Log(10);
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
            else if ((oculusRemote == null && other.gameObject.layer == 9) || (oculusRemote == null && other.gameObject.layer == 10))
            {
                Debug.Log(11);
                return;
            }
            else
            {
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
    //[ClientRpc]
    //void RpcStartTeleport()
    //{
    //    fireMage = transform.root.gameObject;
    //}
    [Command]
    void CmdStartTeleport()
    {
        //Debug.Log(13);
        fireMage = transform.root.gameObject;
        this.enabled = true;
    }
    [Command]
    void CmdUpdateTeleport()
    {
        Debug.Log(14);
        setParent = true;
    }
    [Command]
    void CmdTeleport()
    {
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

