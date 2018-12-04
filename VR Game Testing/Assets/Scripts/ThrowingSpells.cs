using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingSpells : MonoBehaviour
{
    public GameObject SpellThrowable;
    public GameObject smallFlames;
    public GameObject spell;
    public int destroyTimer;
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
        if (other.gameObject)
        {
                var newSpell = Instantiate(spell, SpellThrowable.transform.position, Quaternion.identity);
                gameObject.GetComponent<Renderer>().enabled = false;
                Destroy(SpellThrowable);
            
        }
    }
}
