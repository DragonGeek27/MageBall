using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OculusRemote : MonoBehaviour {
    public GameObject spellOne;
    public GameObject spellTwo;
    public Transform spellSpawn;
    public GameObject spellSpawnGO;
    public float spellRate;




    private AudioSource audioSource;
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
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            SpellThrowOne();

        }

    }
    //spawn in or shoot spells
    void SpellShootOne()
    {
        var newSpell =  Instantiate(spellOne, spellSpawn.position, spellSpawn.rotation);
            //newSpell.transform.SetParent (spellShoot.transform);

            //Debug.Log("Hello");
        
    }
    void SpellThrowOne()
    {

            var newSpell = Instantiate(spellTwo, spellSpawn.position, spellSpawn.rotation);
            //newSpell.transform.SetParent(spellSpawn.transform);

        
    }
}

