using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class storystuff : MonoBehaviour
{

    public Image img;
    public Sprite[] imgs;
    public Text txt;
    public Image angel;
    float angelcol;
    string[] txts1 = {"...", "Where am I?", "...I died!?", "Are those the gates of judgement?", "Oh no, they must be sending me to hell.", "\"Next.\"", "Oh no that's me.", "\"Dr. Evil McEvilness\"", "\"Heaven\"", "Wait what?", "But I'm an Evil Scientist, I've done Evil deeds", "\"Name one evil deed you've done\"", "Well I uhhh... robbed a store once!", "Using my revolutionary Lock Breaker 3000", "\"First of all\"", "\"Your Lock Breaker was just a hairpin.\"", "\"And the store you robbed was secretly a front for selling drugs.\"", "\"The news of the robbery helped the police find out\"", "WHAT", "I must have done something more evil right?", "OH YEAH", "I KILLED A DOG ONCE, NOBODY WHO KILLS A DOG CAN GET TO HEAVEN RIGHT?", "\"The 'dog' you killed was actually the Robo-Hound 1500.\"", "\"A Robot made by an ACTUAL evil scientist who was terrorizing the city.\"", "No way... I dedicated my life to evil.", "I can't accept this!", "I'll escape and find my own way to hell!","But I CANNOT GO UP to heaven", "     "};
    string[] txts2 = {"I recognize this!", "The coin deflector!", "I created this as a way to escape the cops.", "I'd toss it up and it would create a shield around me", "Imagine the amount of aura I'd gain!", "Unfortunately, I never got caught by cops.", "So I never had an opportunity to use it", "     " };
    string[] txts3 = {"What is this place?", "Wait... is this purgatory?", "A place between heaven and hell, where the souls of the Morally Gray rest.", "That explains all of those humanoid creatures", "     " };
    string[] txts4 = {"Another one of my gadgets!?", "It's The Swap Gun!", "It allows the user the swap places with another person.", "I remember when I was trying it out for the first time.", "A person running away from a dog ran right in front of me when I shot the trigger.", "We swapped places and the dog started chasing me intsead.", "So that dog was a robot this entire time, huh?", "Now that I think about it, I could have used it the other way around.","If a dog was chasing me, I could swap places with someone else, making the dog chase them instead.", "I wonder if I can use it anywhere here...", "     " };
    string[] txts5 = {"The platform creator!", "This lets the user create a platform above them.", "The idea was to break into people's houses through window.", "But turns out you can only open them from the inside.", "So I modified it to automatically create a platform whenever it sensed a weakness- like an open window with nobody in the house.", "Sadly, it never did.", "I wonder if this could even be useful here", "     " };
    int i = 0;
    int j = 0;
    string onsc, left;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        j = 0;
        onsc = "";
        if (GameManager.checkpoint == 0)
        {
            left = txts1[i];
            GameObject.Find("GameManager").GetComponent<GameManager>().music.clip = GameObject.Find("GameManager").GetComponent<GameManager>().song[0];
            GameObject.Find("GameManager").GetComponent<GameManager>().music.Play();
        }
        if (GameManager.checkpoint == 2) left = txts2[i];
        if (GameManager.checkpoint == 3)
        {
            left = txts3[i];
            GameObject.Find("GameManager").GetComponent<GameManager>().music.clip = GameObject.Find("GameManager").GetComponent<GameManager>().song[1];
            GameObject.Find("GameManager").GetComponent<GameManager>().music.Play();
        }
        if (GameManager.checkpoint == 4) left = txts4[i];
    }

    // Update is called once per frame
    void Update()
    {

        if (!onsc.Equals(left))
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                onsc = left;
                j = left.Length + 1;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                i++;
                j = 0;
                onsc = "";
                if (GameManager.checkpoint == 0) left = txts1[i];
                if (GameManager.checkpoint == 2) left = txts2[i];
                if (GameManager.checkpoint == 3) left = txts3[i];
                if (GameManager.checkpoint == 4) left = txts4[i];
                if (GameManager.checkpoint == 6) left = txts5[i];
            }
        }
        if (GameManager.checkpoint == 0)
        {
            img.sprite = imgs[0];
            if (i > 6 && i < 25) angelcol +=0.05f;
            if (i > 24) angelcol -= 0.05f;
            if (i > 27) SceneManager.LoadScene(1);
        }
        if (GameManager.checkpoint == 2)
        {
            img.sprite = imgs[1];
            if (i > 6) SceneManager.LoadScene(1);
        }
        if (GameManager.checkpoint == 3)
        {
            img.sprite = imgs[2];
            if (i > 3) SceneManager.LoadScene(2);
        }
        if (GameManager.checkpoint == 4)
        {
            img.sprite = imgs[3];
            if (i > 9) SceneManager.LoadScene(2);
        }
        if (GameManager.checkpoint == 4)
        {
            img.sprite = imgs[3];
            if (i > 6) SceneManager.LoadScene(2);
        }
        angelcol = Mathf.Clamp01(angelcol);
        angel.color = new Color(1, 1, 1, angelcol);
        print(i);
        txt.text = onsc;
    }

    void FixedUpdate()
    {
        if (j < left.Length)
        {
            if (GameManager.checkpoint == 0) onsc = onsc + txts1[i][j++];
            if (GameManager.checkpoint == 2) onsc = onsc + txts2[i][j++];
            if (GameManager.checkpoint == 3) onsc = onsc + txts3[i][j++];
            if (GameManager.checkpoint == 4) onsc = onsc + txts4[i][j++];
            if (GameManager.checkpoint == 6) onsc = onsc + txts5[i][j++];
        }
    }
}
