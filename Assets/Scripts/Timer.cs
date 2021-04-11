using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public GameObject mostradorTempo;

    public float contador;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;

        string minutes = Mathf.Floor(contador / 60).ToString("00");
        string seconds = (contador % 60).ToString("00");

        string texto = string.Format("{0}:{1}", minutes, seconds);

        mostradorTempo.GetComponent<Text>().text = texto;
    }
}
