using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDatabase : MonoBehaviour {

    public AudioClip washerSound;
    public AudioClip stoveSound;

    public void WasherSound()
    {
        AudioPlayer.playSound(washerSound, true, true);
    }

    public void StoveSound()
    {
        AudioPlayer.playSound(stoveSound, true, true);
    }
}
