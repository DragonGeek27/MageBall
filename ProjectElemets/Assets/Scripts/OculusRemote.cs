using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class OculusRemote : NetworkBehaviour
{
    public GameObject spellShoot;
    public GameObject spellOne;
    public GameObject spellTwo;
    public GameObject spellThree;
    public GameObject spellSpawn;

    public List<GameObject> ShootList;

    public float spellShootCoolDown = 0.5f;
    private float spellShootTimer = 0;

    public float spellOneCoolDown = 7;
    private float spellOneTimer = 0;

    public float spellTwoCoolDown = 7;
    private float spellTwoTimer = 0;

    public float spellThreeCoolDown = 7;
    private float spellThreeTimer = 0;

    public float spellShotSpeed = 7;

    public RectTransform coolDownBar0;
    public RectTransform coolDownBar1;
    public RectTransform coolDownBar2;
    public RectTransform coolDownBar3;

    private void Awake()
    {

    }
    void Start()
    {
        //set cooldown bars to 0
        coolDownBar2.sizeDelta = new Vector2(coolDownBar2.sizeDelta.y, 0);
    }
    void Update()
    {


        //oculus controler controls 
        // spell 1 cooldown
        if (Time.time > spellShootTimer)
        {
            // cast spell 1 
            //if (Input.GetKeyDown(KeyCode.F))
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) //basic shoot spell
            {
                spellShootTimer = Time.time + spellShootCoolDown;
                CmdSpellShoot();
            }
        }
        //____________________________________
        if (Time.time > spellOneTimer)
        {
            //if (Input.GetKeyDown(KeyCode.Alpha1))
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
            {
                spellOneTimer = Time.time + spellOneCoolDown;
                CmdSpellOne();

            }
        }
        //____________________________________
        if (Time.time > spellTwoTimer)
        {
            //if (Input.GetKeyDown(KeyCode.Alpha2))
            if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                spellTwoTimer = Time.time + spellTwoCoolDown;
                CmdSpellTwo();

            }
        }
        //____________________________________
        if (Time.time > spellThreeTimer)
        {
            //if (Input.GetKeyDown(KeyCode.Alpha3))
            if (OVRInput.GetDown(OVRInput.Button.Four))
            {
                spellThreeTimer = Time.time + spellThreeCoolDown;
                CmdSpellThree();
            }
        }
        //Spell Cooldown timer icons
        float Cooldown0 = spellShootTimer - Time.time;
        if (Cooldown0 <= 0)
        {
            Cooldown0 = 0;
        }

        coolDownBar0.sizeDelta = new Vector2(0.2f, Cooldown0 * .5f);

        float Cooldown1 = spellOneTimer - Time.time;
        if (Cooldown1 <= 0)
        {
            Cooldown1 = 0;
        }
        coolDownBar1.sizeDelta = new Vector2(0.2f, Cooldown1 * .028f);

        float Cooldown2 = spellTwoTimer - Time.time;
        if (Cooldown2 <= 0)
        {
            Cooldown2 = 0;
        }
        coolDownBar2.sizeDelta = new Vector2(0.2f, Cooldown2 * .028f);

        float Cooldown3 = spellThreeTimer - Time.time;
        if (Cooldown3 <= 0)
        {
            Cooldown3 = 0;
        }
        coolDownBar3.sizeDelta = new Vector2(0.2f, Cooldown3 * .028f);
        //Debug.Log(Time.time);

        for (var i = ShootList.Count - 1; i > -1; i--)
        {
            if (ShootList[i] == null)
                ShootList.RemoveAt(i);
        }


    }
    //spawn in or shoot spells on server.
    [Command]
    void CmdSpellShoot()
    {
        var newSpell = Instantiate(spellShoot, spellSpawn.transform.position, spellSpawn.transform.rotation);
        NetworkServer.Spawn(newSpell);
        ShootList.Add(newSpell);
        //newSpell.transform.SetParent(spellSpawn.transform);
        newSpell.GetComponent<Rigidbody>().velocity = newSpell.transform.forward * spellShotSpeed;
        RpcAddtolist2(newSpell);

    }
    //____________________________________
    [Command]
    void CmdSpellOne()
    {

        var newSpell = Instantiate(spellOne, spellSpawn.transform.position, spellSpawn.transform.rotation);
        NetworkServer.SpawnWithClientAuthority(newSpell, transform.root.gameObject);

        ShootList.Add(newSpell);
        newSpell.transform.SetParent(spellSpawn.transform);
        RpcAddtolist(newSpell);
    }

    //____________________________________
    [Command]
    void CmdSpellTwo()
    {

        var newSpell = Instantiate(spellTwo, spellSpawn.transform.position, spellSpawn.transform.rotation);
        NetworkServer.SpawnWithClientAuthority(newSpell, transform.root.gameObject);
        ShootList.Add(newSpell);
        newSpell.transform.SetParent(spellSpawn.transform);
        RpcAddtolist(newSpell);
    }
    //____________________________________
    [Command]
    void CmdSpellThree()
    {

        var newSpell = Instantiate(spellThree, spellSpawn.transform.position, spellSpawn.transform.rotation);
        NetworkServer.SpawnWithClientAuthority(newSpell, transform.root.gameObject);
        ShootList.Add(newSpell);
        newSpell.transform.SetParent(spellSpawn.transform);
        RpcAddtolist(newSpell);

    }
    //_____________________________________________________________________________________________________________________
    // synv up spells on cliants

    [ClientRpc]
    public void RpcAddtolist(GameObject newSpell)
    {
        ShootList.Add(newSpell);
        newSpell.transform.SetParent(spellSpawn.transform);
        newSpell.transform.position = spellSpawn.transform.position;
    }
    [ClientRpc]
    public void RpcAddtolist2(GameObject newSpell)
    {
        ShootList.Add(newSpell);
        newSpell.transform.position = spellSpawn.transform.position;

    }
}


