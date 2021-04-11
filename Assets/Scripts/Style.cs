using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Style : MonoBehaviour
{

    GameObject cube;
    GameObject sphere;
    GameObject diamond;

    // Start is called before the first frame update
    void Start()
    {

        cube = GameObject.Find("cubeStyle");
        sphere = GameObject.Find("sphereStyle");
        diamond = GameObject.Find("diamondStyle");
        
        GameObject current = new GameObject();
        int styleNumber = PlayerPrefs.GetInt("brick");
        //if (styleNumber == 0) current = cube;
        if (styleNumber == 1) {
            current = sphere;
            Instantiate(current, transform.position, current.transform.rotation);
        }
        if (styleNumber == 2) {
            current = diamond;
            Instantiate(current, transform.position, current.transform.rotation);
        }

        
        //Destroy(gameObject);

        //gameObject.GetComponent<MeshFilter>().mesh = currentMesh;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
