using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class loclController : MonoBehaviour
{
    FreePlay freePlay;

    // Start is called before the first frame update
    private void Awake()
    {
        freePlay = GetComponent<FreePlay>();
        for(int i = 0; i < 10; i++)
        {
            string savedLevelString = "Level" + (i + 1) + "Played";
            if (PlayerPrefs.GetString(savedLevelString, "null") == "true")
            {
                GameObject.Find("Lock" + (i + 1)).SetActive(false) ;
                GameObject.Find("Level" + (i + 1)).GetComponent<Button>().GetComponent<LevelButton>().isLocked = false;
            }
            else
            {
                GameObject.Find("Level" + (i + 1)).GetComponent<Button>().GetComponent<LevelButton>().isLocked = true;
            } 
        }
    }
}
