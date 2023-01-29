using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCol : MonoBehaviour
{
    ColorChange colorChange;
    Color color;

    private void Awake()
    {
        colorChange = GameObject.Find("Fash").GetComponent<ColorChange>();
    } 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Fash")
        {
            colorChange.canColorSwitch = true;
            colorChange.colorToSwitch = gameObject.GetComponent<SpriteRenderer>().color;
            colorChange.wallColor = gameObject.GetComponent<SpriteRenderer>().color;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Fash")
        {
            colorChange.canColorSwitch = false;
            colorChange.wallColor.r = 1;
            colorChange.wallColor.g = 1;
            colorChange.wallColor.b = 1;
            colorChange.colorToSwitch.r = 1;
            colorChange.colorToSwitch.b = 1;
            colorChange.colorToSwitch.g = 1;
        }
    }
}
