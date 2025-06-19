using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinflip : MonoBehaviour
{
    public SpriteRenderer coin;
    public Sprite[] coinsprite;
    int coincounter = 0;

    private void OnEnable()
    {
        coin.sprite = coinsprite[0];
        coincounter = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        coincounter++;
        coin.sprite = coinsprite[coincounter / 3];
        if (coincounter > 38)
        {
            PlayerMovement.coining = false;
            coin.gameObject.SetActive(false);
        }
    }
}
