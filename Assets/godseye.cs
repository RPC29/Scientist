using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class godseye : MonoBehaviour
{

    public static bool hit;

    // Start is called before the first frame update
    void Start()
    {
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player")) hit = true;
    }
}
