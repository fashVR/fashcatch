using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public AudioClip metalSound;
    public AudioClip clickSound;
    public GameObject warning;
    AudioSource audioSource;
    FreePlay freePlay;
    public bool isLocked;

    private void Awake()
    {
        freePlay = GetComponentInParent<FreePlay>();
        audioSource = GetComponent<AudioSource>();
    }


    public void OnClick()
    {
        if (isLocked)
        {
            audioSource.clip = metalSound;
            audioSource.Play();
            warning.SetActive(false);
            warning.SetActive(true);
        }
        else
        {
            audioSource.clip = clickSound;
            audioSource.Play();
            StartCoroutine(freePlay.LoadNextLevel(gameObject.name));
        }
    }
}
