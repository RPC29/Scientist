using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public BoxCollider2D col;
    bool dead;
    public bool deflected;
    float speed = 0.1f;
    public AudioSource deflectsfx;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.checkpoint < 4) if (!col.IsTouchingLayers(LayerMask.GetMask("camera"))) Destroy(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dead) return;

        if (transform.rotation.eulerAngles.y >= 0.001)
            transform.position += speed * Vector3.right;
        else
            transform.position -= speed * Vector3.right;
    }

    private void Update()
    {
        if (Time.timeScale == 0) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("shield")) deflect();
        if (collision.gameObject.layer == 6) Destroy(this.gameObject);


        if (!collision.isTrigger && !collision.gameObject.name.Equals("Player"))
        {
            Destroy(this.gameObject.GetComponent<Rigidbody2D>());
            Destroy(this.gameObject.GetComponent<BoxCollider2D>());
            dead = true;
        }

        if (collision.gameObject.name.Equals("Player")) Destroy(this.gameObject);

        if (deflected)
        {
            deflectsfx.Play();
            if (collision.gameObject.layer == 8)
            {
                collision.gameObject.GetComponent<Angel>().despawned = true;
                Destroy(this.gameObject);
            }
            if (collision.gameObject.layer == 7)
            {
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
        }
    }

    void deflect()
    {
        deflected = true;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0,180,0));
        speed = 0.5f;
    }
}
