using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AngelBossSpawn : MonoBehaviour
{
    public Transform shootpoint;
    public Transform leavepoint;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.DOMove(shootpoint.position, 1f).SetEase(Ease.OutSine);
        StartCoroutine("dothing");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator dothing() 
    {
        yield return new WaitForSeconds(1.3f);
        this.transform.DOMove(leavepoint.position, 0.5f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
