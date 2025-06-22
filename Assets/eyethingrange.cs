using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyethingrange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            this.gameObject.transform.parent.gameObject.GetComponent<eyething>().angry = true;
        }
    }
}
