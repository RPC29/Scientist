using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Punchofheaven : MonoBehaviour
{
    public SpriteRenderer sp;
    public Sprite[] hands;
    public bool angry;
    public bool animated;
    public GameObject hand;
    public Transform point;
    public Transform origin;
    public Transform catchpoint;
    public BoxCollider2D col;
    public BoxCollider2D handcol;
    public bool notboss;

    // Start is called before the first frame update
    void Start()
    {
        angry = false;
        animated = false;
        sp.sprite = hands[1];
        hand.transform.position = origin.transform.position;
        hand.SetActive(false);
        if (col.IsTouching(GameObject.Find("Player").GetComponent<BoxCollider2D>())) angry = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!animated && angry)
        {
            hand.SetActive(true);
            StartCoroutine("Fall");
            animated = true;
        }
        //sp.sprite = hands[1];
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            angry = false;
            animated = false;
            hand.transform.position = origin.transform.position;
            hand.SetActive(false);
        }
    }

    IEnumerator Fall()
    {
        hand.transform.DOMove(catchpoint.position, 1f, false).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(1);
        hand.transform.DOMove(point.position, 0.25f, false).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(0.25f);
        hand.transform.DOMove(origin.position, 0.25f, false).SetEase(Ease.InExpo);
        yield return new WaitForSeconds(1.5f);
        angry = false;
        animated = false;
        hand.SetActive(false);
        if (!notboss) Destroy(this.gameObject);
    }

}
