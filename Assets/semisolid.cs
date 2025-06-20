using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class semisolid : MonoBehaviour
{
    Transform player;
    public BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Feet").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < player.transform.position.y) col.isTrigger = false;
        else col.isTrigger = true;
    }
}
