using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingSpells : MonoBehaviour
{
    public GameObject SpellThrowable;
    public GameObject spell;
    
    // Use this for initialization
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject)
        //{
            if ((other.gameObject.layer > 9)|| (other.gameObject.layer < 9))
            {
                var newSpell = Instantiate(spell, SpellThrowable.transform.position, Quaternion.identity);
                gameObject.GetComponent<SphereCollider>().enabled = false;
                Destroy(SpellThrowable);
            }
        //}
    }
}
