using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{
    public GameObject angel;
    float setpos;
    int arrowtimer;
    public GameObject arrow;
    public Sprite angelsp;
    public Sprite[] angelsprites;

    // Start is called before the first frame update
    void Start()
    {
        setpos = angel.transform.position.y;
        arrowtimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        angel.transform.position = new Vector2(angel.transform.position.x, setpos + 0.1f * Mathf.Sin(Time.time * 2));
    }

    private void FixedUpdate()
    {
        if (arrowtimer++ % 60 == 0) shoot();
    }

    void shoot()
    {

    }
}
