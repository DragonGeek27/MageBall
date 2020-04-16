using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public const int maxHealth = 100;

    public Text maxHealthTxt;
    
    public Text currentHealthTXT;
    [SyncVar(hook="OnChangeHealth")]   
    public int currentHealth;
	public RectTransform healthBar; 
	public bool destroyOnDeath;
	private NetworkStartPosition[] spawnPoints;

	void Start(){

        maxHealthTxt.text = "/" + maxHealth.ToString();
        currentHealthTXT.text = currentHealth.ToString();

        if (isLocalPlayer) {
			spawnPoints = FindObjectsOfType <NetworkStartPosition>();
		}
	}
    //respawn player if health = 0

    public void TakeDamage(int amount){
        
        if (!isServer) {
			return;
		}


		currentHealth -= amount;

		if (currentHealth <= 0) {

			if (destroyOnDeath) {
				Destroy (gameObject);
			} 
			else {
				currentHealth = maxHealth;
				RpcRespawn ();
			}

		}

	}

    public void Update()
    {
        currentHealthTXT.text = currentHealth.ToString();
        
    }
    // have healthbarsize = health
    void OnChangeHealth(int health){
        currentHealth = health;
		healthBar.sizeDelta = new Vector2 (currentHealth * .02f, healthBar.sizeDelta.y);
        //healthBar.position = new Vector2(health * 2, healthBar.position.y);

    }

	[ClientRpc]
    //respawn player if health = 0
	void RpcRespawn(){
		if (isLocalPlayer) {

			Vector3 spawnPoint = Vector3.zero;
			if (spawnPoints != null && spawnPoints.Length > 0)
            {
				spawnPoint = spawnPoints [ Random.Range (0, spawnPoints.Length) ].transform.position;
			}

			transform.position = spawnPoint;
 		}
	}

}
