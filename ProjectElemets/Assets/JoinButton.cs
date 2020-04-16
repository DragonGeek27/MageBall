using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class JoinButton : MonoBehaviour
{

    private Text buttonText;
    private MatchInfoSnapshot match;
    public JoinButton joinOBJ;
    public GameObject host;
    public GameObject join;

    private void Awake()
    {
        buttonText = GetComponentInChildren<Text>();
        join = GameObject.Find("JoinPor");
        host = GameObject.Find("host");
        //join.SetActive(false);
        joinOBJ = GameObject.Find("JoinPor").GetComponent<JoinButton>();
    }

    // Update is called once per frame
    public void Initialize(MatchInfoSnapshot match, Transform panelTransform)
    {
        this.match = match;
        buttonText.text = match.name;
        if (gameObject.layer == 5)
        {
            transform.SetParent(panelTransform);
            transform.localScale = Vector3.one;
        }
        //transform.localRotation = Quaternion.identity;
        //transform.localPosition = Vector3.zero;
    }

    public void JoinMatch()
    {
        FindObjectOfType<CustomNetworkManager>().JoinMatch(match);
    }
    public void SetMatch()
    {
        if (host != null)
        {
            join.transform.position = host.transform.position;
            host.SetActive(false);
        }
        //join.SetActive(true);
        // set protal To join match
        joinOBJ.match = match;

        //random protal color
        
        Color newColor = new Color(Random.value, Random.value, Random.value, .3f);
        Transform shader1 = joinOBJ.transform.Find("Shader");
        Transform shader2 = shader1.transform.Find("Shader2");
        Renderer rend1 = shader1.GetComponent<Renderer>();
        Renderer rend2 = shader2.GetComponent<Renderer>();
        rend1.material.color = newColor;
        rend2.material.color = newColor;
        if (host == null)
        {
            return;
        }
    }
}
