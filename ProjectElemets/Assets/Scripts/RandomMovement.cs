using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public Rigidbody fireTornado; // Needs rigidbody attached with a collider
    public Vector3 vel; // Holds the random velocity
    public float switchDirection = 3;
    public float curTime = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        SetVel();
        fireTornado = GetComponent<Rigidbody>();
    }

    void SetVel()
    {
        if (Random.value > .5)
        {
            vel.x = 5 * Random.value;
        }
        else
        {
            vel.x = -5 * Random.value;
        }
        if (Random.value > .5)
        {
            vel.z = 5  * Random.value;
        }
        else
        {
            vel.z = -5 * Random.value;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (curTime < switchDirection)
        {
            curTime += 1 * Time.deltaTime;
        }
        else
        {
            SetVel();
            if (Random.value > .5)
            {
                switchDirection += Random.value;
            }
            else
            {
                switchDirection -= Random.value;
            }
                if (switchDirection < 1)
                {
                    switchDirection = 1 + Random.value;
                }
                curTime = 0;
            }
        
    }
    void FixedUpdate()
    {
        vel.y = fireTornado.velocity.y-5;

        fireTornado.velocity = vel;
        
    }
}
