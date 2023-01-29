using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseTutorial : MonoBehaviour
{
    public GameObject tutorialText;

    private void Awake()
    {
        
    }

    public void closeText()
    {
        tutorialText.SetActive(false);
        gameObject.SetActive(false);
    }
}
