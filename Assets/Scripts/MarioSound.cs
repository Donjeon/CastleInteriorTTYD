using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioSound : MonoBehaviour
{

    AudioSource marioAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        marioAudioSource = GetComponent <AudioSource>();
    }

    private void MarioFootstepSound()
    {
        marioAudioSource.Play();
    }
}
