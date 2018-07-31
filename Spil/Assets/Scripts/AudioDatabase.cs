using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDatabase : MonoBehaviour {

    public AudioClip washerSound;

    public void WasherSound()
    {
        AudioPlayer.playSound(washerSound, true, true);
    }
}
