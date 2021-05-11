using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // T28 Singleton
    public static SoundManager Instance = null;

    // T28 Sound clips for link
    //  public AudioClip 'sound name';


    // T28 reference to the AudioSource we added earlier
    private AudioSource SoundEffectsAudio;

    // T28 use for initialization
    // make sure there is only one SoundManager
    // Start is called before the first frame update
    void Start()
    {
        // if there is no Instance of the SoundManager, created the Instance
        if (Instance == null)
        {
            Instance = this;
        }
        // otherwise destroy one if there was another one created somehow
        else if (Instance != null)
        {
            Destroy(gameObject);
        }

        // T28 get a hold of our audio source and assign it to soundEffectAudio instance
        AudioSource theSource = GetComponent<AudioSource>();
        SoundEffectsAudio = theSource;
    }

    // T28 the function any script can call, to play an audio clip
    public void PlayOneShot(AudioClip clip)
    {
        SoundEffectsAudio.PlayOneShot(clip);
    }
}




/*
* TEMPLATES TO PLAY SOUNDS IN SCRIPTS

*  play a sound
*  SoundManager.Instance.PlayOneShot(SoundManager.Instance.'sound name');

*/
