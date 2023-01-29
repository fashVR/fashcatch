using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LayoutOptions : MonoBehaviour
{
    public GameObject lJump;
    public GameObject rJump;
    public GameObject lColor;
    public GameObject rColor;

    public TextMeshProUGUI dropLabel;
    public GameObject optPanel;

    public Slider jumpSlider;
    public Slider colorSlider;

    string colorCurrentSide;
    float colorCurrentSize;
    float jumpCurrentSize;

    Animator animator;

    private void Awake()
    {
        colorCurrentSide = PlayerPrefs.GetString("ColorSide", "BOTH SIDES");
        dropLabel.text = colorCurrentSide;
        if (colorCurrentSide == "BOTH SIDES")
        {
            lColor.SetActive(true);
            rColor.SetActive(true);
        }
        else if (colorCurrentSide == "RIGHT")
        {
            lColor.SetActive(false);
            rColor.SetActive(true);
        }
        else if (colorCurrentSide == "LEFT")
        {
            lColor.SetActive(true);
            rColor.SetActive(false);
        }

        colorCurrentSize = PlayerPrefs.GetFloat("ColorSize", 300);
        jumpCurrentSize = PlayerPrefs.GetFloat("JumpSize", 300);
        PlayerPrefs.SetString("LastLevel", "LayoutOptions");

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        colorSlider.value = colorCurrentSize;
        jumpSlider.value = jumpCurrentSize;
    }

    public void SidesChange()
    {
        if(!optPanel.active)
        {
            optPanel.SetActive(true);
        }
        else
        {
            optPanel.SetActive(false);
        }
    }

    public void Side(string side)
    {
        if (side == "b")
        {
            lColor.SetActive(true);
            rColor.SetActive(true);
            PlayerPrefs.SetString("ColorSide", "BOTH SIDES");
        }
        else if (side == "r")
        {
            lColor.SetActive(false);
            rColor.SetActive(true);
            PlayerPrefs.SetString("ColorSide", "RIGHT");
        }
        else if (side == "l")
        {
            lColor.SetActive(true);
            rColor.SetActive(false);
            PlayerPrefs.SetString("ColorSide", "LEFT");
        }
        colorCurrentSide = PlayerPrefs.GetString("ColorSide");

        dropLabel.text = colorCurrentSide;
        optPanel.SetActive(false);
    }

    public void ColorSlider(float value)
    {
        lColor.GetComponent<RectTransform>().sizeDelta = new Vector2(value, value * 1.8f);
        rColor.GetComponent<RectTransform>().sizeDelta = new Vector2(value, value * 1.8f);
        PlayerPrefs.SetFloat("ColorSize", value);
        colorCurrentSize = value;
        colorSlider.value = value;
    }

    public void JumpSlider(float value)
    {
        lJump.GetComponent<RectTransform>().sizeDelta = new Vector2(value, value);
        rJump.GetComponent<RectTransform>().sizeDelta = new Vector2(value, value);
        PlayerPrefs.SetFloat("JumpSize", value);
        jumpCurrentSize = value;
        jumpSlider.value = value;
    }

    public void Back()
    {
        animator.SetBool("DoFadeOut", true);
    }    

    public void BackToOptions()
    {
        PlayerPrefs.SetString("LastLevel", "LayoutOptions");
        SceneManager.LoadScene("Options");
    }
}
