using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleWipeController : MonoBehaviour
{
    public GameObject fashBody;
    public Camera cameraMain;
    public Shader shader;
    private int firstTime;
    public Material material;
    [Range(0, 1.5f)]
    public float _radius = 1;
    private float _ratio = (float)Screen.width / (float)Screen.height;
    public float _duration = 1f;

    //public GameObject blackScreen;

    // Start is called before the first frame update
    void Awake()
    {
        firstTime = PlayerPrefs.GetInt("FirstTime", 1);
        //FadeIn();
        material = new Material(shader);
        material.SetFloat("_Ratio", _ratio);
        _radius = 1.5f;
        if (PlayerPrefs.GetString("LastLevel") == "MainMenu")
            UpdateShader();            
        else
            StartCoroutine(WaitForLoad());
        
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
        Debug.Log("klfdjhfkjd");
        StartCoroutine(DoFade(0f, 1.5f));
    }

    IEnumerator DoFade(float start, float end)
    {
        _radius = start;
        UpdateShader();
        var time = 0f;
        while (time< 1f)
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
        Vector3 ScreenPos = cameraMain.WorldToScreenPoint(fashBody.transform.position);
        float _xAxisPercentage = ((ScreenPos.x / cameraMain.pixelWidth) * 100) / 100;
        float _yAxisPercentage = ((ScreenPos.y / cameraMain.pixelHeight) * 100) / 100;
        material.SetFloat("_Radius", _radius);
        material.SetFloat("_XAxisPercentage", _xAxisPercentage);
        material.SetFloat("_YAxisPercentage", _yAxisPercentage);
    }
}
