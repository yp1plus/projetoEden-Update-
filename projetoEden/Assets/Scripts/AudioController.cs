using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip currentClip;
    public bool audioIsPlaying {get {return audioSource.isPlaying;}} 
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (audioSource != null)
            audioSource.Play();
    }

    /// <summary>
    /// Plays a specific sound, normally when occurs collision.
    /// </summary>
    /// <param name = "clip"> The AudioClip to be executed. </param>
    public void PlaySound(AudioClip clip)
    {
        if (clip != currentClip)
        {
            currentClip = clip;
            if (audioSource.clip == null)
                audioSource.clip = clip;
            audioSource.PlayOneShot(currentClip);
        }
        else
        {
            if(!audioSource.isPlaying || audioSource.clip != clip)
            {
                if (audioSource.clip == null)
                    audioSource.clip = clip;
                audioSource.PlayOneShot(currentClip);
            }
                
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
