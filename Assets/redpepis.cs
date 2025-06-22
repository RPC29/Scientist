using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redpepis : MonoBehaviour
{
    public bool deflected;
    float starty;
    float speed;

    public SpriteRenderer sp;
    public Sprite[] sps;
    int spritenumber;

    public GameObject Target;
    bool swapped;

    GameObject Player;
    Vector3 Direction;

    // Start is called before the first frame update
    void Start()
    {
        deflected = false;
        starty = transform.position.y;
        speed = 0.1f;
        spritenumber = 0;
        Player = GameObject.Find("Player");
        Target = Player;
        swapped = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!deflected)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, starty, 0), Player.transform.position, speed);
            starty = transform.position.y;
            transform.position = new Vector3(transform.position.x, starty + 0.2f * Mathf.Sin(Time.time * 15), 0) ;
            spritenumber++;
            sp.sprite = sps[(spritenumber%20)/5];
        }

        if (deflected)
        {
            transform.position += Direction * speed * 4;
            spritenumber+= 2;
            sp.sprite = sps[(spritenumber % 20) / 5];
        }
    }

    void Update()
    {
        if (Time.timeScale == 0) Destroy(this.gameObject);

        if (!swapped && deflected)
        {
            print("step 1");
            if (Input.GetMouseButtonUp(0) && PlayerMovement.canteleport)
            {
                print("step 2");
                Target = Player.GetComponent<PlayerMovement>().toteleport;
                swapped = true;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name.Equals("shield"))
        {
            deflected = true;
            Direction = this.transform.position - Player.transform.position;
            Direction = new Vector3(Direction.x, 0, 0);
            Direction = Direction.normalized;
        }

        if (collision.gameObject.name.Equals("Player")) Destroy(this.gameObject);

        if (deflected)
        {
            if (collision.gameObject.layer == 10)
            {
                collision.gameObject.transform.parent.gameObject.GetComponent<eyething>().superangry = true;
                collision.gameObject.transform.parent.gameObject.GetComponent<eyething>().Target = Target;
                collision.gameObject.transform.parent.gameObject.GetComponent<AudioSource>().Play();
                Destroy(this.gameObject);
            }
            if (collision.gameObject.layer == 7)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
