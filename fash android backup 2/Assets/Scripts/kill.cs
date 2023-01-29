using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class kill : MonoBehaviour
{
    Camera cameraMain;
    CircleWipeController circleWipe;
    CameraFollow cameraFollow;
    bool isDaying;

    ColorChange colorChange;

    private void Awake()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        cameraMain = Camera.main;
        circleWipe = cameraMain.GetComponent<CircleWipeController>();
        colorChange = GameObject.Find("Fash").GetComponent<ColorChange>();
    }

    void waitToReset()
    {
        colorChange.ResetScene();
    }
    

    public void BlackFadeOut()
    {
        cameraMain.GetComponent<CircleWipeController>().FadeOut();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Fash" && !colorChange.isDaying)
        {
            isDaying = true;
            Debug.Log("Hey");
            cameraFollow.shouldLockOnY = true;
            BlackFadeOut();
            waitToReset();
            PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
        }
    }
}