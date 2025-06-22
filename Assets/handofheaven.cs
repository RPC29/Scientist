using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class handofheaven : MonoBehaviour
{
    public SpriteRenderer sp;
    public Sprite[] hands;
    public bool angry;
    public bool animated;
    public bool caught;
    public bool canimated;
    float speed;
    public GameObject hand;
    public Transform point;
    public Transform origin;
    public Transform catchpoint;
    public BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
        angry = false;
        animated = false;
        canimated = false;
        if (col.IsTouching(GameObject.Find("Player").GetComponent<BoxCollider2D>())) angry = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!animated && angry)
        {
            StartCoroutine("Fall");
            animated = true;
        }
        if (!canimated && caught)
        {
            StartCoroutine("caughtem");
            canimated = true;
        }


        if (caught) sp.sprite = hands[1];
        else sp.sprite = hands[0];
    }

    IEnumerator Fall()
    {
        hand.transform.DOMove(point.position, 1f, false).SetEase(Ease.OutFlash);
        yield return new WaitForSeconds(1);
        if (!caught)
        {
            hand.transform.DOMove(origin.position, 1f, false).SetEase(Ease.InExpo);
            yield return new WaitForSeconds(2);
            angry = false;
            animated = false;
            if (col.IsTouching(GameObject.Find("Player").GetComponent<BoxCollider2D>())) angry = true;
        }
    }

    IEnumerator caughtem()
    {
        hand.transform.DOMove(catchpoint.position, 1f, false).SetEase(Ease.InOutFlash);
        yield return new WaitForSeconds(4);
        caught = false;
        sp.sprite = hands[0];
        yield return new WaitForSeconds(1);
        angry = false;
        animated = false;
        canimated = false;
        hand.transform.DOMove(origin.position, 1f, false).SetEase(Ease.InExpo);
        yield return new WaitForSeconds(1);
        if (col.IsTouching(GameObject.Find("Player").GetComponent<BoxCollider2D>())) angry = true;
    }
}
