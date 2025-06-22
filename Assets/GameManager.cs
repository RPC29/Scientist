using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int checkpoint;
    public AudioClip[] song;
    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        checkpoint = 0;
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(3);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
