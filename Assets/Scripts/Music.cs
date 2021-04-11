using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audios = gameObject.GetComponents<AudioSource>();

        int musicNumber = PlayerPrefs.GetInt("music");

        audios[musicNumber].Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
