using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    public Rigidbody2D playerb;
    public BoxCollider2D playercol;
    public SpriteRenderer playersp;
    public GameObject coin;
    public GameObject coinshield;
    public SpriteRenderer redflash;
    public SpriteRenderer redoverlay;


    int coyotetimer;
    int stuntimer;
    public static bool coining;
    bool hascoin;
    bool laststunwasleft;
    float health = 100;
    float redflashstrength = 0;

    [Header("Spritres")]
    public Sprite idle;
    public Sprite flip;

    // Start is called before the first frame update
    void Start()
    {
        hascoin = false;
        if (GameManager.checkpoint == 1)
        {
            transform.position = new Vector3(-10, 4.3f, 0.2f);
            cam.transform.position = new Vector3(-10, 5, -10);
        }

        coyotetimer = 10;
        coining = false;
        stuntimer = 0;
        redflash.color = new Color(0.4339623f,0,0, 0f);
        redflashstrength = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stuntimer-- > 0)
        {
            playerb.velocity = new Vector2(((!laststunwasleft)? stuntimer : -stuntimer), playerb.velocity.y);
            redflash.color = new Color(0.4339623f, 0, 0, Mathf.Clamp01(redflashstrength));
            redflashstrength *= 0.95f;
            if (health <= 40) redoverlay.color = new Color(0.4339623f, 0, 0, Mathf.Clamp01((40 - health) / 40f));
            health += 0.2f;
            health = Mathf.Clamp(health, 0,100);
            return;
        }

        if (coining) return;
        redflash.color = new Color(0.4339623f, 0, 0, Mathf.Clamp01(redflashstrength));
        redflashstrength *= 0.95f;
        if (health <= 40) redoverlay.color = new Color(0.4339623f, 0, 0, Mathf.Clamp01((40 - health)/40f));
        health += 0.2f;
        playerb.velocity = new Vector2(5 * Input.GetAxisRaw("Horizontal"), playerb.velocity.y);
        if (Input.GetAxisRaw("Horizontal") < -0.01f)
        {
            player.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
        }
        if (Input.GetAxisRaw("Horizontal") > 0.01f)
        {
            player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        if (!Input.GetKey("w") && playercol.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            coyotetimer = 10;
        }
        else
        {
            coyotetimer--;
        }

        if (cam.transform.position == new Vector3(-10, 5, -10) && GameManager.checkpoint < 1) GameManager.checkpoint = 1;
    }
    private void Update()
    {
        if (coining) return;
        else playersp.sprite = idle;

        if (Input.GetKeyDown("w") && coyotetimer > 0)
        {
            playerb.velocity += new Vector2(0,7);
            coyotetimer = -1;
        }
        if (Input.GetKeyDown(KeyCode.Space) && hascoin && playercol.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            coining = true;
            playersp.sprite = flip;
            playerb.velocity = Vector2.zero;
            coinshield.SetActive(true);
            coin.SetActive(true);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 && !collision.gameObject.GetComponent<arrow>().deflected)
        {
            stuntimer = 20;
            health -= 20;
            if (health > 0)
            redflashstrength = 1 - (health / 100f);
            else
            {
                redflashstrength = 1;
                SceneManager.LoadScene(1);
            }
            playerb.velocity = (new Vector2((collision.gameObject.transform.rotation.y != 0)?20:-20, -5));
            if ((collision.gameObject.transform.rotation.y != 0)) laststunwasleft = false;
            else laststunwasleft = true;
        }

        if (collision.gameObject.name.Equals("coin giver")) hascoin = true;
    }
}
