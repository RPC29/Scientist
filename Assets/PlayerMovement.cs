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
    public GameObject particles;
    public GameObject sky;
    public GameObject Mouse;
    public Transform point;
    public LineRenderer line;
    public GameObject toteleport;
    public GameObject instructions;
    public GameObject boss;

    int coyotetimer;
    int stuntimer;
    int caughttimer;
    public static bool coining;
    bool hascoin;
    bool hasline;
    bool hasplat;
    bool laststunwasleft;
    float health = 100;
    float redflashstrength = 0;
    public static bool canteleport;
    int walkcounter;

    [Header("Spritres")]
    public Sprite idle;
    public Sprite[] walk;
    public Sprite flip;

    [Header("Sound Effects")]
    public AudioSource hurt;
    public AudioSource swapsfx;

    // Start is called before the first frame update
    void Start()
    {
        hascoin = true;
        hasline = true;
        hasplat = true;
        if (GameManager.checkpoint == 1) 
        {
            transform.position = new Vector3(-10, 4.3f, 0.2f);
            cam.transform.position = new Vector3(-10, 5, -10);
        }
        if (GameManager.checkpoint == 2) 
        {
            transform.position = new Vector3(-10, 22, 0.2f);
            cam.transform.position = new Vector3(-10, 25, -10);
            hascoin = true;
            Destroy(GameObject.Find("coin giver"));
        }
        if (GameManager.checkpoint == 3) 
        { 
            hascoin = true;
        }

        if (GameManager.checkpoint == 4)
        {
            transform.position = new Vector3(-50, 12, 0.2f);
            cam.transform.position = new Vector3(-50, 15, -10);
            hascoin = true;
            hasline = true;
            Destroy(GameObject.Find("line giver"));
            instructions.SetActive(true);
        }
        if (GameManager.checkpoint == 5) 
        { 
            hascoin = true;
            hasline = true;
        }

        if (GameManager.checkpoint == 6)
        {
            transform.position = new Vector3(25, 22, 0.2f);
            cam.transform.position = new Vector3(30, 25, -10);
            hascoin = true;
            hasline = true;
            hasplat = true;
            Destroy(GameObject.Find("plat giver"));
        }

        coyotetimer = 10;
        coining = false;
        stuntimer = 0;
        redflash.color = new Color(0.4339623f,0,0, 0f);
        redflashstrength = 0;
        walkcounter = 0;
        health = 100;
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
        if (caughttimer-- > 0)
        {
            if (caughttimer == 60 || caughttimer == 120)
            {
                health -= (hasplat) ? 2.5f : 20;
                hurt.Play();
                if (health > 0)
                    redflashstrength = 1 - (health / 100f);
                else
                {
                    redflashstrength = 1;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            playerb.velocity = Vector2.zero;
            redflash.color = new Color(0.4339623f, 0, 0, Mathf.Clamp01(redflashstrength));
            redflashstrength *= 0.95f;
            if (health <= 40) redoverlay.color = new Color(0.4339623f, 0, 0, Mathf.Clamp01((40 - health) / 40f));
            health += 0.2f;
            health = Mathf.Clamp(health, 0,100);
            return;
        }
        if (caughttimer < 0) playersp.color = new Color(1, 1, 1, 1);

        if (hasline && Input.GetMouseButton(0))
        {
            Vector2 direction = Mouse.transform.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, LayerMask.GetMask("Default", "Shadow"));

            if (hit.collider != null)
            {
                point.position = hit.point;
                if (hit.collider.gameObject.layer == 11)
                {
                    toteleport = hit.collider.gameObject;
                    canteleport = true;
                }
                else canteleport = false;
            }
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

        walkcounter++;
    }
    private void Update()
    {

        if (Time.timeScale != 0 && caughttimer <=0 && stuntimer <= 0 && hasline && Input.GetMouseButton(0))
        {
            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, point.position);
        }
        else
        {
            line.enabled = false;
        }

        if (coining) return;
        else
        if (playercol.IsTouchingLayers(LayerMask.GetMask("Ground")) && Input.GetAxisRaw("Horizontal") != 0)
            playersp.sprite = walk[(walkcounter % 40) / 10];
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
        particles.transform.position = cam.transform.position / 2f;
        particles.transform.position = new Vector3(particles.transform.position.x, particles.transform.position.y, 10);
        sky.transform.position = new Vector3(cam.transform.position.x / 1.1f, cam.transform.position.y / 1.1f, 10);

    }

    private void LateUpdate()
    {

        if (Time.timeScale != 0 && stuntimer <= 0 && hasline && Input.GetMouseButtonUp(0) && canteleport)
        {
            Vector3 swap = transform.position;
            transform.position = toteleport.transform.position;
            toteleport.transform.position = swap;
            canteleport = false;
            swapsfx.Play();
            
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 && !collision.gameObject.GetComponent<arrow>().deflected)
        {
            stuntimer = (hasplat) ? 10 : 20;
            health -= (hasplat) ? 10 : 20;
            if (health > 0)
            redflashstrength = 1 - (health / 100f);
            else
            {
                redflashstrength = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            playerb.velocity = (new Vector2((collision.gameObject.transform.rotation.y != 0)?20:-20, -5));
            if ((collision.gameObject.transform.rotation.y != 0)) laststunwasleft = false;
            else laststunwasleft = true;
            hurt.Play();
        }
        if (collision.gameObject.layer == 9 && !collision.gameObject.GetComponent<redpepis>().deflected)
        {
            stuntimer = (hasplat) ? 10 : 50;
            health -= (hasplat) ? 10 : 30;
            if (health > 0)
            redflashstrength = 1 - (health / 100f);
            else
            {
                redflashstrength = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            playerb.velocity = (new Vector2((collision.gameObject.transform.position - transform.position).x <= 0?50:-50, -5));
            if (((collision.gameObject.transform.position - transform.position).x <= 0)) laststunwasleft = false;
            else laststunwasleft = true;
            hurt.Play();
        }
        if (collision.gameObject.layer == 10 && collision.gameObject.transform.parent.gameObject.GetComponent<eyething>().superangry && collision.gameObject.transform.parent.gameObject.GetComponent<eyething>().Target == this.gameObject)
        {
            collision.gameObject.transform.parent.gameObject.GetComponent<eyething>().superangry = false;
            collision.gameObject.transform.parent.gameObject.GetComponent<eyething>().disabledtimer = 180;
            stuntimer = (hasplat) ? 10 : 50;
            health -= (hasplat) ? 10 : 30;
            if (health > 0)
                redflashstrength = 1 - (health / 100f);
            else
            {
                redflashstrength = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            playerb.velocity = (new Vector2((collision.gameObject.transform.rotation.y != 0) ? 50 : -50, -5));
            if ((collision.gameObject.transform.rotation.y != 0)) laststunwasleft = false;
            else laststunwasleft = true;
            hurt.Play();
        }
        if (collision.gameObject.layer == 18)
        {
            stuntimer = (hasplat) ? 10 : 50;
            health -= (hasplat) ? 10 : 20;
            collision.enabled = false;
            if (health > 0)
                redflashstrength = 1 - (health / 100f);
            else
            {
                redflashstrength = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            playerb.velocity = (new Vector2((collision.gameObject.transform.rotation.y != 0) ? 50 : -50, -5));
            if ((collision.gameObject.transform.rotation.y != 0)) laststunwasleft = false;
            else laststunwasleft = true;
            hurt.Play();
        }
        if (collision.gameObject.layer == 16 && collision.gameObject.transform.parent.gameObject.GetComponent<handofheaven>().caught)
        {
            caughttimer = 180;
            health -= (hasplat)?5:20;
            if (health > 0)
                redflashstrength = 1 - (health / 100f);
            else
            {
                redflashstrength = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            player.transform.position = collision.gameObject.transform.parent.GetChild(5).position;
            hurt.Play();
            playersp.color = new Color(0, 0, 0, 0);
        }

        if (collision.gameObject.name.Equals("coin giver"))
        {
            GameManager.checkpoint = 2;
            SceneManager.LoadScene(3);
        }
        if (collision.gameObject.name.Equals("PurgEntry"))
        {
            GameManager.checkpoint = 3;
            SceneManager.LoadScene(3);
        }
        if (collision.gameObject.name.Equals("line giver"))
        {
            GameManager.checkpoint = 4;
            SceneManager.LoadScene(3);
        }
        if (collision.gameObject.name.Equals("HellEntry"))
        {
            GameManager.checkpoint = 5;
            SceneManager.LoadScene(5);
        }
        if (collision.gameObject.name.Equals("plat giver"))
        {
            GameManager.checkpoint = 6;
            SceneManager.LoadScene(5);
        }
        if (collision.gameObject.name.Equals("Start BossFight"))
        {
            boss.SetActive(true);
            collision.gameObject.SetActive(false);
        }
    }
}
