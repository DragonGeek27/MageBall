using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour {
    public GameObject Spawn;
    public GameObject Target;


    // Use this for initialization
    void Start () {
        Target.SetActive(true);
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Spell")
        {
            Invoke("Spawn2", 2);
            //Instantiate(Spawn, transform.position, transform.rotation);
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            Target.SetActive(false);
            Destroy(gameObject, 2);
            
        }
    }
    void Spawn2()
    {
        Instantiate(Spawn, transform.position, transform.rotation);
    }
}
