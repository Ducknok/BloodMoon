using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    private float length, startpox;
    public GameObject cam;
    public float parallaxEffect;
    void Start()
    {
        startpox = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startpox + dist, transform.position.y, transform.position.z);

        if (temp > startpox + length) startpox += length;
        else if(temp < startpox - length) startpox -= length;
    }
}
