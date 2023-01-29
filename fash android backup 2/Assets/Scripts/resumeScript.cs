using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resumeScript : MonoBehaviour
{
    Animator resumeAnim;
    public GameObject GameUI;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Awake()
    {
        resumeAnim = GameObject.Find("PauseMenu").GetComponent<Animator>();
    }
    
    public void ResumeStart()
    {
        resumeAnim.SetBool("DoFadeOut", true);
    }

    public void ResumeEnd()
    {
        pauseMenu.SetActive(false);        
    }
}
