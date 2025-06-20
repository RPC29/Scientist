using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{
    public GameObject angel;
    float setpos;
    int arrowtimer;
    public GameObject arrow;
    public SpriteRenderer angelsp;
    public Sprite[] angelsprites;

    public int desync;
    public bool despawned;

    // Start is called before the first frame update
    void Start()
    {
        setpos = angel.transform.position.y;
        arrowtimer = desync;
        despawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        angel.transform.position = new Vector2(angel.transform.position.x, setpos + 0.1f * Mathf.Sin(Time.time * 2));
        if (Time.timeScale == 0) despawned = false;
    }

    private void FixedUpdate()
    {
        if (despawned)
        {
            angelsp.color = new Color(0.3f, 0.3f, 0.3f, 1);
            return;
        }
        else
        {
            angelsp.color = new Color(1f, 1f, 1f, 1);
        }
        if (arrowtimer++ % 60 == 0) shoot();
    }

    void shoot()
    {
        Instantiate(arrow, transform.position + new Vector3(0,0.3f,0), transform.rotation, null);
    }
}
