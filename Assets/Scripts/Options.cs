using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{

    public Dropdown brickDD;
    public Dropdown skyDD;
    public Dropdown musicDD;

    // Start is called before the first frame update
    void Start()
    {
        brickDD.value = PlayerPrefs.GetInt("brick");
        skyDD.value = PlayerPrefs.GetInt("sky");
        musicDD.value = PlayerPrefs.GetInt("music");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
