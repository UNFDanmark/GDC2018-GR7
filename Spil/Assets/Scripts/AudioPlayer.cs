using UnityEngine;

/// <summary>
/// Creates a GameObject with a audiosource, which will be played.
///
/// CREDIT:
/// Julius 'Bukz' Bendt
/// Juto Studio
/// https://wwww.juto.dk
/// Free license
///
/// </summary>



public class AudioPlayer : MonoBehaviour
{


    public static AudioSource playSound(AudioClip clip)
    {
        return CreateAudioSource(clip, 1, 1, true, false);
    }

    public static AudioSource playSound(AudioClip clip, bool twoD)
    {
        return CreateAudioSource(clip, 1, 1, twoD, false);
    }

    public static AudioSource playSound(AudioClip clip, bool twoD, bool loop)
    {
        return CreateAudioSource(clip, 1, 1, twoD, loop);
    }

    public static AudioSource playSound(AudioClip clip, float volume, float pitch, bool twoD, bool loop)
    {
        return CreateAudioSource(clip, volume, pitch, twoD, loop);
    }

    private static AudioSource CreateAudioSource(AudioClip clip, float volume, float pitch, bool twoD, bool loop)
    {
        GameObject g = new GameObject("AudioPlayer " + clip.name);
        AudioSource source = g.AddComponent<AudioSource>();

        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
        source.spatialBlend = (twoD) ? 0 : 1;

        if (!loop)
            Destroy(g, clip.length);


        source.Play();

        return source;

    }

}