using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuCircleWipe : MonoBehaviour
{
    public Camera cameraMain;
    public Shader shader;
    private int firstTime;
    public Material material;
    [Range(0, 1.5f)]
    public float _radius = 1;
    private float _ratio = (float)Screen.width / (float)Screen.height;
    public float _duration = 1f;
    public string savedLastLevel;
    public MenuCircleWipe wipeController;

    //public GameObject blackScreen;

    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log(SceneManager.GetActiveScene().name);
        //Debug.Log(PlayerPrefs.GetString("Mode"));
        wipeController = Camera.main.GetComponent<MenuCircleWipe>();
        //FadeIn();
        material = new Material(shader);
        material.SetFloat("_Ratio", _ratio);
        _radius = 1.5f;
        
        if(PlayerPrefs.GetString("Circle") == "Yello")
        {
            PlayerPrefs.SetString("LastLevel", "MainMenu");
            material.SetFloat("_ColorR", 0.4980392f);
            material.SetFloat("_ColorG", 0.4f);
            material.SetFloat("_ColorB", 0);
            material.SetFloat("_ColorA", 0);
            UpdateShader();
            FadeIn();
        }
        else if (PlayerPrefs.GetString("LastLevel", "null") == "null")
        {
            PlayerPrefs.SetString("LastLevel", "MainMenu");
            material.SetFloat("_ColorR", 0.4980392f);
            material.SetFloat("_ColorG", 0.4f);
            material.SetFloat("_ColorB", 0);
            material.SetFloat("_ColorA", 0);
            UpdateShader();
            FadeIn();
        }
        else if(PlayerPrefs.GetString("LastLevel", "null") != "null")
        {
            if (PlayerPrefs.GetString("LastLevel") == "Options" || PlayerPrefs.GetString("LastLevel") == "LevelSelect" || PlayerPrefs.GetString("LastLevel") == "SpeedrunEnding" || PlayerPrefs.GetString("LastLevel") == "MainMenu")
            {
                wipeController.enabled = false;
            }
            else if((PlayerPrefs.GetString("Mode") == "FreePlay") && (SceneManager.GetActiveScene().name == "LevelSelect"))
            {
                Debug.Log("Oof");
                wipeController.enabled = true;
                UpdateShader();
                FadeIn();
            }
            else
            {
                Debug.Log("How");
                UpdateShader();
                FadeIn();
            }
        }
        
    }

    private void Start()
    {
         
    }
    IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(1);
        UpdateShader();
        FadeIn();
    }

    // Update is called once per frame
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {        
            Graphics.Blit(source, destination, material);
    }

    public void FadeOut()
    {
        StartCoroutine(DoFade(1.5f, 0f));
    }

    public void FadeIn()
    {
        StartCoroutine(DoFade(0f, 1.5f));
    }

    IEnumerator DoFade(float start, float end)
    {
        _radius = start;
        UpdateShader();
        var time = 0f;
        while (time < 1f)
        {
            _radius = Mathf.Lerp(start, end, time);
            time += Time.deltaTime / _duration;
            UpdateShader();
            yield return null;
        }
        _radius = end;
        UpdateShader();
    }
    private void UpdateShader()
    {
        float _xAxisPercentage = 0.5f;
        float _yAxisPercentage = 0.5f;
        material.SetFloat("_Radius", _radius);
        material.SetFloat("_XAxisPercentage", _xAxisPercentage);
        material.SetFloat("_YAxisPercentage", _yAxisPercentage);
    }
}
