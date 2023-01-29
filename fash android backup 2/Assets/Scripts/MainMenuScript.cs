using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    Animator mainMenuAnim;
    public GameObject mainMenu;
    // Start is called before the first frame update
    void Awake()
    {
        mainMenuAnim = GameObject.Find("Main Menu").GetComponent<Animator>();
    }

    // Update is called once per frame
    public void playEnd()
    {
        mainMenu.SetActive(false);
    }
}
