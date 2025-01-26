using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Xylophone : MonoBehaviour
{
    public AudioSource mySound;

    private void Start()
    {
        mySound = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        mySound.Play();
    }
}