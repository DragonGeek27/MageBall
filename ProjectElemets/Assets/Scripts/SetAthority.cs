using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetAthority : NetworkBehaviour
{
    public OVRGrabbable ovrGrab;
    public OculusRemote player;
    public Transform objRoot;
    public GameObject playerWithAthority;
    public bool athoritySet = false;
    public GameObject floatColider;
    public string test;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        test = gameObject.GetComponent<NetworkIdentity>().ToString();
        Debug.Log("IDs: " + test);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("aaaaaaa2");
        if (other.gameObject.layer == 9)
        {
            Debug.Log("aaaaaaa1");
            if (athoritySet == false)
            {
                playerWithAthority = other.gameObject.transform.root.gameObject;
                Debug.Log("aaaaaaa");
                if (ovrGrab.isGrabbed == true)
                {
                    floatColider.GetComponent<CapsuleCollider>().gameObject.SetActive(false);

                    Debug.Log("aaaaaaa3");
                objRoot = playerWithAthority.transform.root;
                player = objRoot.gameObject.GetComponent<OculusRemote>();
                player.ShootList.Add(gameObject);
                gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(playerWithAthority.GetComponent<NetworkIdentity>().connectionToClient);
                gameObject.GetComponent<NetworkIdentity>().RemoveClientAuthority(gameObject.GetComponent<NetworkIdentity>().connectionToClient);
                gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(playerWithAthority.GetComponent<NetworkIdentity>().connectionToClient);
                
                CmdSetAthority();
                    athoritySet = true;
                }
            }
        }
    }
    [Command]
    void CmdSetAthority() {
        floatColider.GetComponent<CapsuleCollider>().gameObject.SetActive(false);
        Debug.Log("qweqweqw");
        objRoot = playerWithAthority.transform.root;
        player = objRoot.gameObject.GetComponent<OculusRemote>();
        player.ShootList.Add(gameObject);
        gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(playerWithAthority.GetComponent<NetworkIdentity>().connectionToClient);
        gameObject.GetComponent<NetworkIdentity>().RemoveClientAuthority(gameObject.GetComponent<NetworkIdentity>().connectionToClient);
        gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(playerWithAthority.GetComponent<NetworkIdentity>().connectionToClient);
        athoritySet = true;
    }
}
