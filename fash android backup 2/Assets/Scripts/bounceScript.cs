using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounceScript : MonoBehaviour
{
    Animator anim;
    public AudioSource audioSource;
    public AudioClip bounce;
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Fash")
        {


            anim.Play("flowerBounce");
            
        }
    }
}



