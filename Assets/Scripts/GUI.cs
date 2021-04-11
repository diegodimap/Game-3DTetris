using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{

    public GameObject tetris;
    public Text scoreText;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        score = tetris.GetComponent<Tetris>().score;
        
        scoreText.text = score + "";
    }

    // Update is called once per frame
    void Update()
    {
        score = tetris.GetComponent<Tetris>().score;

        scoreText.text = score + "";
    }
}
