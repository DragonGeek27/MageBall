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

        // set hit to object hit so if hit = player it will not go off if on players list
        GameObject hit = other.gameObject.transform.root.gameObject;
        OculusRemote oculusRemote = hit.GetComponent<OculusRemote>();

        if (oculusRemote != null)
        {

            if (oculusRemote.ShootList.Contains(gameObject))
            {
                return;

            }

            else
            {
                //networkIdentity.localPlayerAuthority = false;

                // run exploshon if hits player without spell on list
                CmdExploshon();
            }
        }
        else if ((oculusRemote == null && other.gameObject.layer == 9) || (oculusRemote == null && other.gameObject.layer == 10))
        {
            return;
        }

        else
        {
            //networkIdentity.localPlayerAuthority = false;\
            
            // play exploshen if coliding with anything that is not player that casts the spell
            CmdExploshon();
        }
    }
    [Command]
    void CmdExploshon()
    {

        if (this.enabled)
        {
            this.enabled = false;
            //This will fire only the first time this object hits a trigger
            //create exploshon or spell efect at the location the spell ball lands
            GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            expl.GetComponent<Transform>().localScale = gameObject.GetComponent<Transform>().localScale;
            //create spell exploshon or efect over the network and destroy spell ball
            NetworkServer.Spawn(expl);
            NetworkServer.Destroy(gameObject);
            NetworkServer.Spawn(expl);

            Destroy(gameObject);
            //destoy spell efect after sso many sec
            Destroy(expl, efectTime);


        }
    }
}

