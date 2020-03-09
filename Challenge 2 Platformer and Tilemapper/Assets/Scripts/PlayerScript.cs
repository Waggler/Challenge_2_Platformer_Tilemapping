using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    private bool facingRight = true;

    public float speed;

    public Text score; //create score text
    public Text winText; //create win text
    public Text livesText; //create text for life count

    private int scoreValue = 0;
    private int lives; //set lives to use integers

    /*private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;*/ //Ground check code, never ended up working


    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource; //set audio

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        score.text = "Score: " + scoreValue.ToString();
        lives = 3; //set lives to 3
        winText.text = "";
        musicSource.clip = musicClipOne; //Set and Play Background Music
        musicSource.Play();
        SetLivesText();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));


        if (facingRight == false && hozMovement > 0) //Flip Player when they changer directions
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if (scoreValue == 4) 
            {
                lives = 3;
                SetLivesText(); //Reset Lives to 3
                transform.position = new Vector2(45.0f, 0.0f); //when 4 pickups are collected, teleport player to second map
            }
            if (scoreValue >= 8) //if count is 8 or higher display win text
            {
                winText.text = "You Win! Game created by Kevin H. Davis!";
                musicSource.loop = false; //stop music from looping
                musicSource.volume = 1f;
                musicSource.clip = musicClipTwo; // Play Win muisc
                musicSource.Play();
            }
        }

        if (collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            SetLivesText();
            Destroy(collision.collider.gameObject); //Destroy enemy and remove a life if the player walks into them

            if (lives <= 0) //if lives are 0 or less, destroy player and display text
            {
                winText.text = "You Lose! Please Try Again!";
                Destroy(gameObject); //Player is destroyed
            }
        }

    }
    //private bool isAirborne = false;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {

            anim.SetInteger("State", 0); //Set animation to idle if on ground and no keys are pressed
            anim.SetBool("Ground", true);

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) //Set animation to run if movement keys are pressed
            {
                anim.SetInteger("State", 1);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                anim.SetInteger("State", 1);
            }
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) //Set animation to jump if jump keys are pressed
            {
                anim.SetBool("Ground", false);
                anim.SetInteger("State", 2);
                rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse); //Set Jump hight
            }



        }
    }
        void SetLivesText() //function that sets player's lives
        {
            livesText.text = "Lives: " + lives.ToString(); //create text output for lives

            if (lives <= 0) //if lives are 0 or less, destroy player and display text
            {
                winText.text = "You Lose! Please Try Again!";
                Destroy(gameObject);
            }
        }

        void Flip() //Function that flips player
        {
            facingRight = !facingRight;
            Vector2 Scaler = transform.localScale;
            Scaler.x = Scaler.x * -1;
            transform.localScale = Scaler;
        }
}
