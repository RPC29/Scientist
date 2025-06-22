using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowguy : MonoBehaviour
{

    public Rigidbody2D rb;
    Vector3 position;
    int stuntimer;
    bool laststunwasleft;

    public bool selected;
    public Sprite normal;
    public Sprite select;
    public SpriteRenderer sp;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) transform.position = position;
    }

    private void FixedUpdate()
    {
        if(player.GetComponent<PlayerMovement>().toteleport == this.gameObject && PlayerMovement.canteleport)
        {
            selected = true;
        }
        else
        {
            selected = false;
        }
        if (stuntimer-- > 0)
        {
            rb.velocity = new Vector2(((!laststunwasleft) ? stuntimer : -stuntimer), rb.velocity.y);
            return;
        }
        if (selected)
        {
            sp.sprite = select;
        }
        else
        {
            sp.sprite = normal;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && collision.gameObject.transform.parent.gameObject.GetComponent<eyething>().superangry && collision.gameObject.transform.parent.gameObject.GetComponent<eyething>().Target == this.gameObject)
        {
            collision.gameObject.transform.parent.gameObject.GetComponent<eyething>().superangry = false;
            collision.gameObject.transform.parent.gameObject.GetComponent<eyething>().disabledtimer = 180;
            stuntimer = 50;
            rb.velocity = (new Vector2((collision.gameObject.transform.rotation.y != 0) ? 50 : -50, -5));
            if ((collision.gameObject.transform.rotation.y != 0)) laststunwasleft = false;
            else laststunwasleft = true;
        }
    }
}
