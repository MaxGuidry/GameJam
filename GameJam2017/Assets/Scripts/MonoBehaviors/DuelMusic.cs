using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelMusic : MonoBehaviour
{
    public AudioClip MusicClip;

    private AudioSource _backSound;
    // Use this for initialization
    void Start()
    {
        _backSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_backSound.isPlaying)
        {
            Debug.LogWarning("already playing sound");
        }
        else
        {
            _backSound.clip = MusicClip;
            _backSound.Play();
        }
    }
}
