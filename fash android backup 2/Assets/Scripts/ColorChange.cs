using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColorChange : MonoBehaviour
{
    [Range(0, 10)] public float lerpTime;
    public List<GameObject> blueFash = new List<GameObject>();
    public GameObject hat, glasses, fins, spray, colorButtons;
    public int seconds;
    Color hc, gc, fc;
    bool executeColorLerp = false;
    bool executeColorReturn = false;
    public Color colorToSwitch;
    Controller controller;
    Animator anim;
    Color originalBodyColor, originalHatColor, originalGlassesColor, originalFinsColor;
    public List<GameObject> slots = new List<GameObject>();
    private Color saveColor;
    public Color wallColor;
    Color defaultColor;
    private bool performDestroy = false;
    public bool isCamouflaged = false;
    public Color fashColor;
    public Camera cameraMain;
    GameObject savedG;
    public AudioSource audioSource;
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioClip steps;
    public AudioClip steps2;
    public AudioClip jump;
    public AudioClip dive;
    public AudioClip afterJump;
    public AudioClip spraySound;
    public AudioClip fallingSound;
    CircleWipeController circleWipe;
    GameManager gameManager;
    public bool isDaying;
    public bool canColorSwitch;

    private void Awake()
    {
        canColorSwitch = false;
        gameManager = FindObjectOfType<GameManager>();
        originalBodyColor = blueFash[0].GetComponent<SpriteRenderer>().color;
        originalHatColor = hat.GetComponent<SpriteRenderer>().color;
        originalGlassesColor = glasses.GetComponent<SpriteRenderer>().color;
        originalFinsColor = fins.GetComponent<SpriteRenderer>().color;

        defaultColor = colorToSwitch;

        wallColor.r = 1;
        wallColor.g = 1;
        wallColor.b = 1;

        anim = GetComponent<Animator>();
        controller = GameObject.Find("FashParent").GetComponent<Controller>();

        circleWipe = cameraMain.GetComponent<CircleWipeController>();
    }

    
    private void Update()
    {
        if (Mathf.Round(wallColor.r * 100.0f) * 0.1f == Mathf.Round(blueFash[0].GetComponent<SpriteRenderer>().color.r * 100.0f) * 0.1f)
        {
            if (Mathf.Round(wallColor.b * 100.0f) * 0.1f == Mathf.Round(blueFash[0].GetComponent<SpriteRenderer>().color.b * 100.0f) * 0.1f)
            {
                if (Mathf.Round(wallColor.g * 100.0f) * 0.1f == Mathf.Round(blueFash[0].GetComponent<SpriteRenderer>().color.g * 100.0f) * 0.1f)
                {
                    isCamouflaged = true;
                }
                else
                {
                    isCamouflaged = false;
                }
            }
            else
            {
                isCamouflaged = false;
            }
        }
        else
        {
            isCamouflaged = false;
        }

        if (executeColorLerp)
        {
            LerpObject(fins.GetComponent<SpriteRenderer>(), fc, lerpTime * Time.deltaTime);
            LerpObject(glasses.GetComponent<SpriteRenderer>(), gc, lerpTime * Time.deltaTime);
            LerpObject(hat.GetComponent<SpriteRenderer>(), hc, lerpTime * Time.deltaTime);
            foreach (GameObject g in blueFash)
            {
                LerpObject(g.GetComponent<SpriteRenderer>(), saveColor, lerpTime * Time.deltaTime);
            }
        }
        
        if (executeColorReturn)
        {
            LerpObject(fins.GetComponent<SpriteRenderer>(), originalFinsColor, 7 * Time.deltaTime);
            LerpObject(glasses.GetComponent<SpriteRenderer>(), originalGlassesColor, 7 * Time.deltaTime);
            LerpObject(hat.GetComponent<SpriteRenderer>(), originalHatColor, 7 * Time.deltaTime);
            foreach (GameObject g in blueFash)
            {
                LerpObject(g.GetComponent<SpriteRenderer>(), originalBodyColor, 7 * Time.deltaTime);
            }
        }
    }

    IEnumerator WaitBeforeReturn()
    {
        
        yield return new WaitForSeconds(8);
        executeColorReturn = true;
    }

    public void Sound(string sound)
    {

        if(sound == "Spray")
        {
            audioSource.clip = spraySound;
        }
        else if(sound == "Falling")
        {
            audioSource.clip = fallingSound;
        }
        audioSource.Play();
    }
    public void freezeAll()
    {
        if (controller.isGrounded())
            controller.freezeAll = true;

    }
    
        public void BlackFadeOut()
    {
        cameraMain.GetComponent<CircleWipeController>().FadeOut();
    }

    void AllowColor()
    {
        canColorSwitch = true;
    }

    void DisableColor()
    {
        canColorSwitch = false;
    }

    public void colorSwitch()
    {
        executeColorReturn = false;
        
        hc = colorToSwitch;
        hc.r -= .29f;
        hc.g -= .32f;
        hc.b -= .35f;

        gc = colorToSwitch;
        gc.r -= .09f;
        gc.g -= .094f;
        gc.b -= .094f;

        fc = colorToSwitch;
        fc.r += .25f;
        fc.g += .13f;
        fc.b += .074f;
        if (!isCamouflaged && controller.isGrounded() && canColorSwitch)
        {
                foreach (GameObject g in slots)
                {
                    if (g.transform.childCount == 5)
                    {
                        foreach (Transform child in g.transform)
                        {
                            if (child.gameObject.name[0] == 'c' || child.gameObject.name[0] == 'C')
                            {

                                if (child.gameObject.GetComponent<SpriteRenderer>().color.r != 1 || child.gameObject.GetComponent<SpriteRenderer>().color.b != 1 || child.gameObject.GetComponent<SpriteRenderer>().color.g != 1)
                                {
                                    if (Mathf.Round(child.gameObject.GetComponent<SpriteRenderer>().color.r * 100.0f) * 0.1f == Mathf.Round(colorToSwitch.r * 100.0f) * 0.1f)
                                    {
                                        if (Mathf.Round(child.gameObject.GetComponent<SpriteRenderer>().color.b * 100.0f) * 0.1f == Mathf.Round(colorToSwitch.b * 100.0f) * 0.1f)
                                        {
                                            if (Mathf.Round(child.gameObject.GetComponent<SpriteRenderer>().color.g * 100.0f) * 0.1f == Mathf.Round(colorToSwitch.g * 100.0f) * 0.1f)
                                            {
                                                foreach (Transform i in g.transform)
                                                {
                                                    if (i.gameObject.name == "countDownBlack(Clone)")
                                                    {
                                                        Animator blackShadeAnim = i.gameObject.GetComponent<Animator>();
                                                        blackShadeAnim.SetBool("ShouldFillUp", true);
                                                    }
                                                }
                                                controller.freezeAll = true;
                                                anim.SetBool("Spray", true);
                                                saveColor = colorToSwitch;
                                                savedG = g;
                                                StopCoroutine("WaitBeforeReturn");
                                                StartCoroutine("destroySlot");
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
        }
        
    }

    public void StartLerp()
    {
        executeColorLerp = true;
    }
    public void SparyStop()
    {
        anim.SetBool("Spray", false);
        controller.freezeAll = false;
        executeColorLerp = false;
        StartCoroutine("WaitBeforeReturn");

    }

    public void PlayFootStep()
    {
        audioSource1.clip = afterJump;
        audioSource1.Play();


    }

    public void PlayJump()
    {
        audioSource2.clip = jump;
        audioSource2.Play();
        audioSource1.clip = afterJump;
        audioSource1.Play();
    }

    public void PlayDive()
    {
        audioSource2.clip = jump;
        audioSource2.Play();
        audioSource1.clip = afterJump;
        audioSource1.Play();
        audioSource3.clip = dive;
        audioSource3.Play();
       
    }

    public void sprayColorSwitch()
    {
        spray.GetComponent<SpriteRenderer>().color = colorToSwitch;
    }

    public void LerpObject(SpriteRenderer renderer, Color color, float time)
    {
        renderer.color = Color.Lerp(renderer.color, color, time);
    }

    public void ResetScene()
    {
        if (!isDaying)
        {
            StartCoroutine(RestartScene());
        }
    }

    public IEnumerator RestartScene()
    {
        circleWipe.enabled = true;
        circleWipe.FadeOut();
        PlayerPrefs.SetFloat("Timer", gameManager.timer);
        PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
        yield return new WaitForSeconds(1);
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        
    }

    public void WaitForGrounded()
    {
        StartCoroutine(GroundWaitCommand());
    }

    IEnumerator destroySlot()
    {
        GameObject savedsavedG = savedG;
        yield return new WaitForSeconds(11);        
        foreach (Transform child1 in savedsavedG.transform)
        {
            if(child1.gameObject.name[0] != 'S')
            {
                GameObject.Destroy(child1.gameObject);
            }
        }        
    }

    IEnumerator GroundWaitCommand()
    {
        yield return new WaitUntil(controller.isGrounded);
        anim.SetBool("Detected", false);
        anim.SetBool("Detected2", true);       
    }

        public void DisableLockOnX()
    {
        controller.shouldMove = true;
    }
}
