using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JeWoPlayerController : MonoBehaviour
{
    private const int ELF = 1;
    private const int SNOWMAN = 2;
    private const int REINDEER = 3;

    public float speed;

    private Rigidbody2D rb2d;
    private int count;

    //internal timer stuff
    private float timer;
    private int wholetime;

    public Text CollectText;
    public Text startText;
    public Text endText;
    public Text countText;

    public Text myStartText;

    private AudioSource audiosource;
    public AudioClip GetHit;

    public AudioClip SayElf;
    public AudioClip SaySnowman;
    public AudioClip SayReindeer;

    private bool gotDesiredObj;
    private bool gotWrongObj;
    public GameObject myElf;
    public GameObject mySnowman;
    public GameObject myReindeer;
    private System.Random rnd;
    public int myRndNum;   // 1=elf, 2=snowman, 3=reindeer

    // Use this for initialization
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();

        audiosource = GetComponent<AudioSource>();

        count = 0;
        endText.text = "";
        gotDesiredObj = false;
        gotWrongObj = false;
        rnd = new System.Random();
        myRndNum = rnd.Next(1, 4);

        startText.text = "Get that ";
        if (myRndNum == ELF)
        {
            startText.text += "elf!";
            audiosource.PlayOneShot(SayElf);
        }
        else if (myRndNum == SNOWMAN)
        {
            startText.text += "snowman!";
            audiosource.PlayOneShot(SaySnowman);
        }
        else if (myRndNum == REINDEER)
        {
            startText.text += "reindeer!";
            audiosource.PlayOneShot(SayReindeer);
        }

        Debug.Log("my rnd num: " + myRndNum);
        SetCountText();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);

        timer = timer + Time.deltaTime;
        if (!gotDesiredObj && timer >= 10)
        {
            endText.text = "Too Slow! Better Luck Next Time!";
            StartCoroutine(ByeAfterDelay(2));
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (myRndNum == 1 && other.gameObject.CompareTag("Elf"))
        {
            gotDesiredObj = true;
        }
        else if (myRndNum == 2 && other.gameObject.CompareTag("Snowman"))
        {
            gotDesiredObj = true;
        }
        else if (myRndNum == 3 && other.gameObject.CompareTag("Reindeer"))
        {
            gotDesiredObj = true;
        }
        else if (myRndNum == 1 && !other.gameObject.CompareTag("Elf"))
        {
            gotWrongObj = true;
        }
        else if (myRndNum == 2 && !other.gameObject.CompareTag("Snowman"))
        {
            gotWrongObj = true;
        }
        else if (myRndNum == 3 && !other.gameObject.CompareTag("Reindeer"))
        {
            gotWrongObj = true;
        }

        if (other.gameObject.CompareTag("Elf") || other.gameObject.CompareTag("Snowman") || other.gameObject.CompareTag("Reindeer"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            //GameLoader.AddScore(1);
            SetCountText();
            audiosource.PlayOneShot(GetHit);
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 1 && gotDesiredObj)
        {
            endText.text = "Happy Holidays!";
            StartCoroutine(ByeAfterDelay(2));
        }
        else if (count >= 1 && gotWrongObj)
        {
            endText.text = "Not that one!";
            StartCoroutine(ByeAfterDelay(2));
        }
    }

    IEnumerator ByeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);

        //GameLoader.gameOn = false;
    }

}
    
