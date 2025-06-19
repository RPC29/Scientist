using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D playerb;
    public BoxCollider2D playercol;
    public SpriteRenderer playersp;
    public GameObject coin;
    


    int coyotetimer;
    public static bool coining;

    [Header("Spritres")]
    public Sprite idle;
    public Sprite flip;

    // Start is called before the first frame update
    void Start()
    {
        coyotetimer = 10;
        coining = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (coining) return;

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
    }
    private void Update()
    {
        if (coining) return;
        else playersp.sprite = idle;

        if (Input.GetKeyDown("w") && coyotetimer > 0)
        {
            playerb.velocity += new Vector2(0, 5);
            coyotetimer = -1;
        }
        if (Input.GetKeyDown(KeyCode.Space) && playercol.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            coining = true;
            playersp.sprite = flip;
            playerb.velocity = Vector2.zero;
            coin.SetActive(true);
        }
    }
}
