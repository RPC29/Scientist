using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transition : MonoBehaviour
{

    public float camx;
    public float camy;
    public static bool transitioning;

    public GameObject semisolid;
    public bool hassemi;

    // Start is called before the first frame update
    void Start()
    {
        transitioning = false;
        if (hassemi)
        {
            semisolid.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            if (Camera.main.transform.position != new Vector3(camx, camy, -10))
            {
                StartCoroutine("MoveCam");
            }
        }
    }


    IEnumerator MoveCam()
    {
        Time.timeScale = 0;
        transitioning = true;
        while (Camera.main.transform.position != new Vector3(camx, camy, -10))
        {
            yield return new WaitForSecondsRealtime(1f/60f);
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(camx, camy, -10), 10/60f);
        }
        Debug.Log("Movement done");
        if (hassemi)
        {
            semisolid.SetActive(true);
            GameObject.Find("Player").transform.position = this.transform.position;
            GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.1f);
        }
        transitioning = false;
        Time.timeScale = 1;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player") && hassemi)
            semisolid.SetActive(false);

    }
}
