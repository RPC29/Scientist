using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillsItselfAfterThreeSeconds : MonoBehaviour
{
    int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    private void OnEnable()
    {
        count = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        count++;

        if (count > 180)
        {
            if (!gameObject.transform.GetChild(0).gameObject.GetComponent<handofheaven>().angry && !gameObject.transform.GetChild(1).gameObject.GetComponent<handofheaven>().angry)
            this.gameObject.SetActive(false);
        }
    }
}
