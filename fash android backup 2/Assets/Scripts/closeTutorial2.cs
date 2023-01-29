using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeTutorial2 : MonoBehaviour
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
