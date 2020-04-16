using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PotionSpawn : NetworkBehaviour
{
    public GameObject[] potions;
    public GameObject potion;
    public List<GameObject> potionCount;
    public int potionListCount;
    public Transform[] potionSpawnLocations;
    public Transform potionSpawnLocation;

    private void Start()
    {
        spawnPotions();
        InvokeRepeating("spawnPotions", 0, 0.10f);
    }
    private void Update()
    {
        potionListCount = potionCount.Count;
        for (var i = potionCount.Count - 1; i > -1; i--)
        {
            if (potionCount[i] == null)
                potionCount.RemoveAt(i);
        }
    }
    void spawnPotions()
    {
        CmdSpawnPotions();
    }
    [Command]
    void CmdSpawnPotions() { 
        int amount = Random.Range(1, 10);
        //spawn 1 - 10 potions
        for (int i = 0; i < amount; i++)
        {
            if (potionListCount < 10)
            {
                // select a random potion from thew aray
                potion = potions[Random.Range(0, potions.Length)];
                potionSpawnLocation = potionSpawnLocations[Random.Range(0, potionSpawnLocations.Length)];
                // stet spawn position and rotoation random
                Vector3 origin = potionSpawnLocation.position;
                Vector3 range = potionSpawnLocation.transform.localScale * 2.0f;
                Vector3 randomRange = new Vector3(Random.Range(-range.x, range.x),
                                                  Random.Range(1, 1),
                                                  Random.Range(-range.z, range.z));

                Vector3 randomPosition = origin + randomRange;

                float randomRotation = Random.Range(-180, 180);

                var newPotion = Instantiate(potion, randomPosition, Quaternion.Euler(0, randomRotation, 0));
                NetworkServer.Spawn(newPotion);
                
                potionCount.Add(newPotion);
            }
        }
    }
}