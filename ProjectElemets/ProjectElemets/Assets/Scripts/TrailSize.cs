using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSize : MonoBehaviour
{
    public GameObject FireBall;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Transform>().localScale = FireBall.GetComponent<Transform>().localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
