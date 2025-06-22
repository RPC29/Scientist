using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handrange : MonoBehaviour
{

    public bool punch;
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
            if (!punch) this.gameObject.transform.parent.gameObject.GetComponent<handofheaven>().angry = true;
            else this.gameObject.transform.parent.gameObject.GetComponent<Punchofheaven>().angry = true;
    }
}
