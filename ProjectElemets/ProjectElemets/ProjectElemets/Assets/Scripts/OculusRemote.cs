using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class OculusRemote : MonoBehaviour
{
    public GameObject spellOne;
    public GameObject spellTwo;
    public GameObject spellThree;
    public GameObject spellFore;
    public GameObject spellSpawn;

    public float coolDownTime = 7; 
    private float nextFireTime = 0;

    public float coolDownTime2 = 7;
    private float nextFireTime2 = 0;

    public float coolDownTime3 = 7;
    private float nextFireTime3 = 0;

    public float spellSpeed = 7;

    
    private void Awake()
    {

    }
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update()
    {
        //oculus controler controls 
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            SpellShootOne();

        }
        if (Time.time > nextFireTime)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
            {
                SpellThrowOne();

            }
        }

        if (Time.time > nextFireTime2)
        {
            if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                SpellDefOne();

            }
        }
        if (Time.time > nextFireTime3)
        {
            if (OVRInput.GetDown(OVRInput.Button.Four))
            {
                SpellThrowTwo();

            }
        }

    }
    //spawn in or shoot spells
    void SpellShootOne()
    {
        var newSpell =  Instantiate(spellOne, spellSpawn.transform.position, spellSpawn.transform.rotation);
        newSpell.GetComponent<Rigidbody>().velocity = newSpell.transform.forward * spellSpeed;


        //newSpell.transform.SetParent (spellShoot.transform);

        Debug.Log("Hello");

    }

    void SpellThrowOne()
    {

            var newSpell = Instantiate(spellTwo, spellSpawn.transform.position, spellSpawn.transform.rotation);
            nextFireTime = Time.time + coolDownTime;
        //newSpell.transform.SetParent(spellSpawn.transform);


    }

    void SpellDefOne()
    {
        var newSpell = Instantiate(spellThree, spellSpawn.transform.position, spellSpawn.transform.rotation);
        nextFireTime2 = Time.time + coolDownTime2;
    }

    void SpellThrowTwo()
    {

        var newSpell = Instantiate(spellFore, spellSpawn.transform.position, spellSpawn.transform.rotation);
        nextFireTime3 = Time.time + coolDownTime3;
        //newSpell.transform.SetParent(spellSpawn.transform);


    }
}

