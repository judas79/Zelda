using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// T26 Used so I can use Floor and round values
using System;

public class Link : MonoBehaviour
{
    // T26 think of evertything you want to tracked and/ or worked with, realated to Link
    // that needs to be defined

    // T26 speed multiplier
    public float speed;

    // T26 rigidBody to move link
    private Rigidbody2D rb;

    // T26 access to IsValidSpace() in Gameboard.cs
    Gameboard gameboard;

    // T26 access to soundManager
    SoundManager soundManager;

    // T26 access to change Links' animation
    Animator animator;

    // T26 change animations when Link is facing right, and we want to face left
    // using a bool, by default is false
    bool facingRight = false;
    bool facingLeft = false;

    // T26 Load the above defined things as well as, maybe, some others, at startup, 
    // By getting them, to avoid errors
    private void Awake()
    {
        // T26 get access to Links rigidBody 2d, which is already attached to Link
        rb = GetComponent<Rigidbody2D>();

        // T26 get access to gameboard, by finding object type
        // so we can get objects from inside our hierarchy
        gameboard = FindObjectOfType(typeof(Gameboard)) as Gameboard;

        // T26 Find GameObject, if we do get the component get access and load our sound manager
        //soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        // T26 get component animator for access and to load it, which is already attached to Link
        animator = GetComponent<Animator>();
    }

    // T26 fixedupdate to do all of our animations
    private void FixedUpdate()
    {
        // T26 get the key pressed input, either of a or d, left arrow, or right arrow
        // and vertMove the key pressed, for Vertical, s or w, or alts, up arrow down arrow 
        // for the way we should move
        float horzMove = Input.GetAxisRaw("Horizontal");
        float vertMove = Input.GetAxisRaw("Vertical");

        // T26 if we have a valid space to move to, according to our IsValudSpace function
        // convert its value to a float, then get Links x position and add horxMove, which is where Links'
        // position he wants to move to, is at
        // do the same thing for Link wanting to vertMove, then pass in both; horzMove, vertMove, to IsValudSpace()
        if (gameboard.IsValidSpace((float)transform.position.x + horzMove, (float)transform.position.y + vertMove, horzMove, vertMove))
        {
            // if it is a valid place to move, move there
            rb.velocity = new Vector2(horzMove * speed, vertMove * speed);
        }
        // T26 if its not a valid vector to move into, stop (0,0)
        else
        {
            rb.velocity = new Vector2(0, 0);
        }

        // T26 change animation, base on what key has been depressed
        if(Input.GetKey("s") || Input.GetKey("down"))
        {
            // play walkDown animation
            animator.Play("WalkDown");
        }
        if (Input.GetKey("w") || Input.GetKey("up"))
        {
            // play walkDown animation
            animator.Play("WalkUp");
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            // play walkRight animation to move right
            animator.Play("WalkRight");

            // T26 find out weather the animation is facing left',
            // since the facingRight bool is set to False, set it to true since facing left
            // set the bool facinLeft to false, which is also its default setting
            // then rotate the animation the opposite way, horrizontaly
            if (facingLeft)
            {
                facingRight = true;
                facingLeft = false;
                animator.transform.Rotate(0, 180, 0);
            }
            // T26 if we are not facing left
            else if(!facingLeft)
            {
                facingRight = true;
            }
        }
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            // play walkRight animation to move left, by fliping it, below
            animator.Play("WalkRight");

            // T26 find out weather the animation is facing right',
            // if it is facing right, we will set it to true, since the default is false
            // so that it will now face left
            // the animator will do the rotation to the opposite of right, which is 180 degrees
            if (facingRight)
            {
                facingRight = true;
                facingLeft = true;
                animator.transform.Rotate(0, 180, 0);
            }
            // T26 if we are not facing right
            else if (!facingRight)
            {
                facingLeft = true;
            }
        }
        // T26 change animation, base on what key has been released, for Idle
        if (Input.GetKeyUp("s"))
        {
            // play walkDown animation
            animator.Play("Idle");


            // T26 anytime Links x and or y position is in a decimal position, round it up to an int
            // this is so Link doesn't catch on the edge of any other game objects, and gets stuck
            // decimaled float values, will get stuck in the array, but we have to use float values to move
            float centerX = (float)Math.Round(Convert.ToDouble(transform.position.x));
            float centerY = (float)Math.Round(Convert.ToDouble(transform.position.y));

            // T26 anytime we set a position on the gameboard, or any game object, 
            // like Links position, we have to use a new vector
            transform.position = new Vector2(centerX, centerY);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
