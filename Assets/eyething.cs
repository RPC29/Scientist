using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyething : MonoBehaviour
{
    public SpriteRenderer sp;
    public Sprite[] sps;
    public bool angry;
    public bool superangry;
    float spinspeed = 1;
    float speed = 1;
    float spritecounter = 0;
    public GameObject redpepis;
    Vector3 position;
    GameObject Player;
    public int disabledtimer;

    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
        spinspeed = 1;
        sp.sprite = sps[0];
        angry = false;
        superangry = false;
        spinspeed = 0;
        position = transform.position;
        Player = GameObject.Find("Player");
        disabledtimer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (disabledtimer-- > 0)
        {
            sp.color = new Color(0.3f, 0.3f, 0.3f, 1);
            if (disabledtimer > 120) sp.sprite = sps[5];
            if (disabledtimer < 120) sp.sprite = sps[4];
            if (disabledtimer < 60) sp.sprite = sps[3];
            Target = Player;
            return;
        }
        sp.color = new Color(1f, 1f, 1f, 1);

        spritecounter += spinspeed;
        if (superangry)
        {
            spinspeed = 11;
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, 0), Target.transform.position, speed-1);

            if ((int)spritecounter % 33 < 11)
                if (sp.sprite == sps[5]);
            else if (sp.sprite == sps[4])
                sp.sprite = sps[5];
            else if (sp.sprite == sps[3])
                sp.sprite = sps[4];
            else
                sp.sprite = sps[3];

            speed *= 1.05f;
            angry = false;
        }
        else if (angry)
        {
            sp.sprite = sps[((int)spritecounter % 99) / 33];
            spinspeed += 0.1f;
            if (spinspeed > 7)
            {
                Instantiate(redpepis, transform.position, transform.rotation, null);
                Target = Player;
                angry = false;
            }

            if (transform.position != position)
            { 
                speed = 0.2f;
                transform.position = Vector3.MoveTowards(transform.position, position, speed);
               
            }
        }
        else
        {
            sp.sprite = sps[((int)spritecounter % 99) / 33];
            spinspeed -= 0.1f;

            if (transform.position != position)
            {
                speed = 0.2f;
                transform.position = Vector3.MoveTowards(transform.position, position, speed);
            }
        }
        spinspeed = Mathf.Clamp(spinspeed, 1, 11);
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            transform.position = position;
            angry = false;
            superangry = false;
            disabledtimer = 0;
        }

        if (superangry)
        {
            if (Input.GetMouseButtonUp(0) && PlayerMovement.canteleport)
            {
                Target = Player.GetComponent<PlayerMovement>().toteleport;
            }
        }

    }
}
