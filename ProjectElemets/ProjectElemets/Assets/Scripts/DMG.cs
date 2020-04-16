using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DMG : MonoBehaviour
{
    public int dMG;
    void OnTriggerEnter(Collider other)
    {
        if (this.enabled)
        {
            this.enabled = false;
        }
        
        GameObject hit = other.gameObject;
		Health health = hit.GetComponent<Health> ();
		if (health != null) {
			health.TakeDamage (dMG);
            //Debug.Log(other.gameObject.layer);

        }
		
	}

}
