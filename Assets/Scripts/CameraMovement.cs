using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject cubeCamera1;
    public GameObject cubeCamera2;
    public GameObject meio;

    GameObject nextPosition;

    float lastStep;
    float timeBetweenSteps;

    // Start is called before the first frame update
    void Start()
    {
        timeBetweenSteps = 0.1f;
        nextPosition = cubeCamera1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastStep > timeBetweenSteps) {
            transform.position = Vector3.Lerp(transform.position, nextPosition.transform.position, 0.01f);
            transform.LookAt(meio.transform);
            lastStep = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other) {
        print("trigger camera");
        if (other.gameObject.name.Contains("1")) {
            nextPosition = cubeCamera2;
        } else {
            nextPosition = cubeCamera1;
        }
    }

}
