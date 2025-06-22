using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoosFight : MonoBehaviour
{
    public GameObject LHand;
    public GameObject RHand;
    public GameObject Plat1;
    public GameObject Plat2;
    public GameObject eye;
    public GameObject RightAngels;
    public GameObject LeftAngels;
    public GameObject Hands;
    public GameObject Punch1;
    public GameObject Punch2;
    public GameObject Pepis;
    public Transform[] Spawners;
    int lasthand;
    int step;
    int phase;
    public static bool pause;

    // Start is called before the first frame update
    void Start()
    {
        step = 0;
        phase = 1;
        Plat1.SetActive(false);
        Plat2.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!pause) step++;
        if (step == 5)
        {
            LHand.transform.DOMove(new Vector3(21.26854f, 25.3f, 0), 1f).SetEase(Ease.OutBounce);
            RHand.transform.DOMove(new Vector3(39, 25.3f, 0), 1f).SetEase(Ease.OutBounce);
        }
        if (step == 65)
        {
            eye.transform.DOMove(new Vector3(29.91f, 28.91f, 0), 1f);
        }
        if (step == 30 * 60)
        {
            if (phase == 1) phase = 2; 
            if (phase == 3) phase = 4; 
            if (phase == 5) phase = 6; 
            Plat1.SetActive(true);
        }
        if (step == 60 * 60)
        {
            Plat2.SetActive(true);
        }
        if (godseye.hit)
        {
            StartCoroutine("pauseabit");
        }
        if (step%120 == 0 && phase == 1)
        {
            int c = Random.Range(0,4);
            switch (c)
            {
                case 0: 
                    Instantiate(RightAngels);
                    Instantiate(LeftAngels);
                    break;
                case 1:
                    Instantiate(Pepis, Spawners[Random.Range(0, 6)]);
                    break;
                case 2:
                    Hands.SetActive(true);
                    break;
                case 3:
                    Instantiate(Punch1);
                    break;
            }
        }
        if (step%120 == 0 && phase == 2)
        {
            int c = Random.Range(0,4);
            switch (c)
            {
                case 0: 
                    Instantiate(RightAngels);
                    Instantiate(LeftAngels);
                    break;
                case 1:
                    Instantiate(Pepis, Spawners[Random.Range(0, 6)]);
                    break;
                case 2:
                    Hands.SetActive(true);
                    break;
                case 3:
                    Instantiate(Punch2);
                    break;
            }
        }
        if (step%60 == 0 && phase == 3)
        {
            int c = Random.Range(0,4);
            switch (c)
            {
                case 0: 
                    Instantiate(RightAngels);
                    Instantiate(LeftAngels);
                    break;
                case 1:
                    Instantiate(Pepis, Spawners[Random.Range(0, 6)]);
                    break;
                case 2:
                    Hands.SetActive(true);
                    break;
                case 3:
                    Instantiate(Punch1);
                    break;
            }
        }
        if (step%60 == 0 && phase == 4)
        {
            int c = Random.Range(0,4);
            switch (c)
            {
                case 0: 
                    Instantiate(RightAngels);
                    Instantiate(LeftAngels);
                    break;
                case 1:
                    Instantiate(Pepis, Spawners[Random.Range(0, 6)]);
                    break;
                case 2:
                    Hands.SetActive(true);
                    break;
                case 3:
                    Instantiate(Punch2);
                    break;
            }
        }
        if (step%60 == 0 && phase == 5)
        {
            int c = Random.Range(0,4);
            switch (c)
            {
                case 0: 
                    Instantiate(RightAngels);
                    Instantiate(LeftAngels);
                    break;
                case 1:
                    Instantiate(Pepis, Spawners[Random.Range(0, 6)]);
                    break;
                case 2:
                    Hands.SetActive(true);
                    break;
                case 3:
                    Instantiate(Punch1);
                    break;
            }
        }
        if (step%60 == 0 && phase == 6)
        {
            int c = Random.Range(0,4);
            switch (c)
            {
                case 0: 
                    Instantiate(RightAngels);
                    Instantiate(LeftAngels);
                    break;
                case 1:
                    Instantiate(Pepis, Spawners[Random.Range(0, 6)]);
                    break;
                case 2:
                    Hands.SetActive(true);
                    break;
                case 3:
                    Instantiate(Punch2);
                    break;
            }
        }
    }

    IEnumerator pauseabit()
    {
        pause = true;
        if (phase == 2) phase = 3;
        if (phase == 4) phase = 5;
        if (phase == 6) ;
        Plat1.SetActive(false);
        Plat2.SetActive(false);
        step = 1;
        yield return new WaitForSeconds(2f);
        godseye.hit = false;
        pause = false;
    }
}
