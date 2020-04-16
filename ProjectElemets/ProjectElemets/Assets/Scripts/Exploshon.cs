using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class Exploshon : NetworkBehaviour
{

    public GameObject explosion;
    public float efectTime;
    //public NetworkIdentity networkIdentity;
    public void Start()
    {

        this.enabled = true;
    }
    public void OnTriggerEnter(Collider other)
    {

        //Debug.Log(other.gameObject);
        //Debug.Log("Server" + gameObject.GetComponent<NetworkBehaviour>().isServer);
        //Debug.Log("Client" + gameObject.GetComponent<NetworkBehaviour>().isClient);
        //Debug.Log("Authority" + gameObject.GetComponent<NetworkBehaviour>().hasAuthority);
        //Debug.Log("layer" + other.gameObject.layer);
        GameObject hit = other.gameObject;
        OculusRemote oculusRemote = hit.GetComponent<OculusRemote>();

        if (oculusRemote != null)
        {

            if (oculusRemote.ShootList.Contains(gameObject))
            {
                return;

            }

            else
            {
                Debug.Log("1");
                //networkIdentity.localPlayerAuthority = false;
                CmdExploshon();
            }
        }
        else if ((oculusRemote == null && other.gameObject.layer == 9) || (oculusRemote == null && other.gameObject.layer == 10))
        {
            return;
        }

        else
        {
            Debug.Log("1.5");
            //networkIdentity.localPlayerAuthority = false;
            CmdExploshon();
        }
    }
    [Command]
    void CmdExploshon()
    {
        Debug.Log("2");
        //    RpcExploshon();
        //}
        //[ClientRpc]
        //void RpcExploshon()
        //{
        if (this.enabled)
        {
            this.enabled = false;
            //This will fire only the first time this object hits a trigger

            GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;

            expl.GetComponent<Transform>().localScale = gameObject.GetComponent<Transform>().localScale;
            NetworkServer.Spawn(expl);
            NetworkServer.Destroy(gameObject);
            NetworkServer.Spawn(expl);

            Destroy(gameObject);
            Destroy(expl, efectTime);


        }
    }
}

