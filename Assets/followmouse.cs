using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followmouse : MonoBehaviour
{
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position += 10 * Vector3.forward;
    }
}
