using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager globalManager;

    public bool firstTime;
    // Start is called before the first frame update
    private void Awake()
    {
        
        if (globalManager == null)
            globalManager = this;
        else
            Destroy(this);
        DontDestroyOnLoad(this.gameObject);
        if (PlayerPrefs.GetInt("FirstTime", 1) == 1) 
        {
            firstTime = true;
            //PlayerPrefs.SetInt("FirstTime", 0);
        }
        else
        {
            firstTime = false;
        }
        


    }
    
}
