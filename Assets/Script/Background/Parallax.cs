using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject cam;

    public float xEffect;
    public float YEffect;
    float startPosX;
    float startPosY;
    // Start is called before the first frame update
    void Start()
    {
        startPosX = cam.transform.position.x;
        startPosY = cam.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float distX = cam.transform.position.x * xEffect;
        float distY = cam.transform.position.y *YEffect;
        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);
    }
}
