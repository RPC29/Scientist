using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introcutscene : MonoBehaviour
{
    public GameObject building;
    public GameObject newyork;
    public SpriteRenderer lab;
    public GameObject guidance;
    public GameObject guidance2;
    public GameObject explosion;
    int step;
    int labstep;
    int explostep;

    // Start is called before the first frame update
    void Start()
    {
        step = 0;
        explostep = 0;
        labstep = 0;
        guidance.SetActive(false);
        guidance2.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        step++;
        building.transform.localScale = new Vector3(1 + 0.002f * step, 1 + 0.002f * step, 1);
        newyork.transform.localScale = new Vector3(1 + 0.001f * step, 1 + 0.001f * step, 1);
        if ((step > 120 && step < 180) || (step > 180* 3 && step < 180 * 4))
        {
            labstep++;
            lab.color += new Color(0,0,0,0.02f);
            lab.transform.localScale = new Vector3(1 + 0.0005f * labstep, 1 + 0.0005f * labstep, 1);
        }
        if (step > 180 && step < 180*3)
        {
            guidance.SetActive(true);
        }
        else
        {
            guidance.SetActive(false);
        }
        if (step > 180* 4 && step < 180*5)
        {
            guidance2.SetActive(true);
        }
        else
        {
            guidance2.SetActive(false);
        }
        if ((step > 180 * 3.5 && step < 180 * 4 )|| step > 180 * 5)
        {
            explosion.transform.localScale = new Vector3(++explostep * 0.1f, explostep * 0.1f, 1);
        }
        if (step > 180 * 6)
        {
            SceneManager.LoadScene(4);
        }
    }
}
