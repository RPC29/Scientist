using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinshield : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localPosition = new Vector2(0.4f, -0.2f + 0.7f * Mathf.Sin((Mathf.PI/2f) * coinflip.coincounter/21f));
        if (coinflip.coincounter > 37)
            this.gameObject.SetActive(false);
    }
}
