using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{

    public Material day;
    public Material sunset;
    public Material night;

    // Start is called before the first frame update
    void Start()
    {
        int sky = PlayerPrefs.GetInt("sky");
        if(sky==0)  RenderSettings.skybox = day;
        if(sky==1)  RenderSettings.skybox = sunset;
        if(sky==2)  RenderSettings.skybox = night;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
