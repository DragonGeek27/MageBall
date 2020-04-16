using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class runButton : MonoBehaviour
{
    public GameObject button;

    private void Start()
    {
        button = gameObject.transform.parent.gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hand")
        {
            button.GetComponent<Button>().onClick.Invoke();
        }
    }
}
