using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioClip landingSound;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();   
    }

    void PlayFootStep()
    {
        AudioClip clip = footstepSounds[(int)Random.Range(0, footstepSounds.Length)];
        source.clip = clip;
        source.Play();
    }

    void PlayLandingSound()
    {
        source.clip = landingSound;
        source.Play();
    }
}
