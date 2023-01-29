using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectDestroy : MonoBehaviour
{
    public AudioClip shake;
    public AudioSource audioSource;
    public void destroyEffect()
    {
        Destroy(gameObject);
    }

    public void playSound()
    {
        audioSource.clip = shake;
        audioSource.Play();
    }
}
