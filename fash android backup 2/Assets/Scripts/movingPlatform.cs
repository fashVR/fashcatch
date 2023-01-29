using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    ColorChange colorChange;
    Animator anim;
    bool isOpen = false;
    private void Awake()
    {
        colorChange = GameObject.Find("Fash").GetComponent<ColorChange>();
        anim = GameObject.Find("movingPlatform").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isOpen && colorChange.isCamouflaged)
        {
                Debug.Log("Reached!");
                anim.Play("PlatformMoveAnim Tutorial");
                isOpen = true;            
        }
        
    }
}
