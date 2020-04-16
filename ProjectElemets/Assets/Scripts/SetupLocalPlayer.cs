using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;
    public GameObject ui;
    public GameObject body;
    // Use this for initialization
    void Start()
    {

        if (!isLocalPlayer)
        {
            //things you want disabled so you dont contoll both player
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
                CharacterController cc = GetComponent(typeof(CharacterController)) as CharacterController;
                cc.enabled = false; // Turn off the component
                ui.SetActive(false);

            }

        }

        if (isLocalPlayer)
        {

            body.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update () {
		if (!isLocalPlayer)
        {
            return;
        } 
    }
}
