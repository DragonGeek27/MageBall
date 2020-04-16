using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PotionPickup : MonoBehaviour
{
    public AudioMixer mixerWithChuck;
    private string myChuck1;
    // Start is called before the first frame update
    void Start()
    {
        myChuck1 = "my_chuck";
        Chuck.Manager.Initialize(mixerWithChuck, myChuck1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Chuck.Manager.RunCode(myChuck1,
            @"
ModalBar m => dac;
800 => m.freq;
    // define function 'addOne'
1.0 => m.noteOn;
.20::second => now;
    fun float addOne(float x)
    {
       ( m.freq() + 100) => x;
        x => m.freq;
	return x;
    }
addOne (m.freq()) => m.freq;  
1.0 => m.noteOn;
.20::second => now;
addOne (m.freq()) => m.freq;   
1.0 => m.noteOn;
2.0::second => now;
");
            Destroy(gameObject);
        }
    }
}
