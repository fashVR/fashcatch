using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    Animator batman;
    public Animator resetAnim;

    public AudioMixer sfxMixer;
    public Slider sfxSlider;

    public AudioMixer musicMixer;
    public Slider musicSlider;

    public GameObject resetDialog;

    public string discordInvite = "https://discord.gg/y2mPfYT";
    public string instagremLink = "https://www.instagram.com/the_banana_project_official/";
    public string twitterLink = "https://twitter.com/ProjectBanana22";
    public string youtubeLink = "https://www.youtube.com/channel/UCyLvgdHRYTtb_3pCwZNq5ww";

    private void Awake()
    {
        batman = GetComponent<Animator>();

        batman.SetBool("FadeFromLayout", false);

        sfxSlider.value = PlayerPrefs.GetFloat("SfxValue");
        musicSlider.value = PlayerPrefs.GetFloat("MusicValue");
    }

    public void ResetShow()
    {
        resetDialog.SetActive(true);
    }


    public void ResetHide()
    {
        resetAnim.SetBool("CloseReset", true);
    }

    public void ResetAll()
    {

        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("LastLevel", "Options");
        SceneManager.LoadScene("MainMenu");
    }

    public void Layout()
    {
        batman.SetBool("FadeToLayout", true);
    }

    public void Menu()
    {
        batman.SetBool("FadeToMenu", true);
    }

    public void BackToMenu()
    {
        PlayerPrefs.SetString("LastLevel", "Options");
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToLayout()
    {
        
        SceneManager.LoadScene("ButtonsLayout");
    }

    public void Discord()
    {
        Application.OpenURL(discordInvite);
    }

    public void Instagrem()
    {
        Application.OpenURL(instagremLink);
    }

    public void Twitter()
    {
        Application.OpenURL(twitterLink);
    }

    public void Youtube()
    {
        Application.OpenURL(youtubeLink);
    }

    public void SfxSlider(float value)
    {
        sfxMixer.SetFloat("Volume", value);
        PlayerPrefs.SetFloat("SfxValue", value);
    }

    public void MusicSlider(float value)
    {
        musicMixer.SetFloat("Volume", value);
        PlayerPrefs.SetFloat("MusicValue", value);
    }
}
