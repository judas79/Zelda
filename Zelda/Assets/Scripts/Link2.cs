using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// T26 Used so I can use Floor and round values
using System;

// T27 or T28 copied from Link.cs paste and altered here.
public class Link2 : MonoBehaviour
{
    // T26 think of evertything you want to tracked and/ or worked with, realated to Link
    // that needs to be defined

    // T26 speed multiplier
    public float speed;

    // T26 rigidBody to move link
    private Rigidbody2D rb;

    // T26 access to IsValidSpace() in Gameboard.cs
    //Gameboard gameboard;

    // T26 access to soundManager
    SoundManager soundManager;

    // T26 access to change Links' animation
    Animator animator;

    // T29 make references for the Sword and Weapon objects, so the sword will show up when in a chase
    GameObject weaponGO;
    GameObject swordGO;

    // T29 track the direction we want to attack with enum
    public enum AttkDir
    {
        LEFT, RIGHT, UP, DOWN
    }

    // T29 specifically have attack direction
    AttkDir attkDir;


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
        //gameboard = FindObjectOfType(typeof(Gameboard)) as Gameboard;

        // T26 Find GameObject, if we do get the component get access and load our sound manager
        //soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        // T26 get component animator for access and to load it, which is already attached to Link
        animator = GetComponent<Animator>();

        // T29 make references to load our weapon and sword, and find them by name, so we can work with them
        weaponGO = GameObject.Find("Weapon");
        swordGO = GameObject.Find("Sword");

        // T29 get our sword component and render and disable it
        //swordGO.GetComponent<Renderer>().enabled = false; // got rid of the error of Sword no being found by the script
    }

    // T26 fixedupdate to do all of our animations
    private void FixedUpdate()
    {
        // T26 get the key pressed input, either of a or d, left arrow, or right arrow
        // and vertMove the key pressed, for Vertical, s or w, or alts, up arrow down arrow 
        // for the way we should move
        float horzMove = Input.GetAxisRaw("Horizontal");
        float vertMove = Input.GetAxisRaw("Vertical");

        // T26 changed for Link2 in T28 because it the position wasalready checked in the Link script
        // this is a valid place to move, move there
        rb.velocity = new Vector2(horzMove * speed, vertMove * speed);

        // T26 change animation, base on what key has been depressed
        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            // T26 play walkDown animation
            animator.Play("WalkDown");

            // T29 when link walks down, we will set our attack direction, and that will track him
            attkDir = AttkDir.DOWN;
        }
        if (Input.GetKey("w") || Input.GetKey("up"))
        {
            // play walkDown animation
            animator.Play("WalkUp");

            // T29 when link walks up, we will set our attack direction, and that will track him
            attkDir = AttkDir.UP;
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            // T26 play walkRight animation to move right
            animator.Play("WalkRight");

            /*
             * commented out in T29
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
            else if (!facingLeft)
            {
                facingRight = true;
            }
            */

            // T29 when link walks down, we will set our attack direction, and that will track him
            attkDir = AttkDir.RIGHT;
        }
        if (Input.GetKey("a") || Input.GetKey("left"))
        {

            // T26 play walkRight animation to move left, by fliping it, below
            animator.Play("WalkLeft");

            // T29 when link walks left, we will set our attack direction, and that will track him
            attkDir = AttkDir.LEFT;

            /*
             * T29 commented out
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
            */

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

            // T29 get our sword component and render and disable it
            swordGO.GetComponent<Renderer>().enabled = false;
        }
        // T29 when the user clicks the return key this will trigger the attack
        if(Input.GetKey(KeyCode.Return))
        {
            // T29 call this function to attack
            LinkAttack();
        }
    }

    // T29 Link attack what is in front of you, based on the information inside the attkDir enum
    // LEFT, RIGHT, UP, DOWN
    void LinkAttack()
    {
        // dependant upon Links attack direction LEFT, RIGHT, UP, DOWN
        switch (attkDir)
        {
            case AttkDir.DOWN:
                {
                    // don't do anything, its our default action, pass 0,0 to coroutine
                    StartCoroutine(AnimateSword(0f, 0f));
                    break;
                }
            case AttkDir.LEFT:
                {
                    // move the sword to the left; -90 degrees, and down a bit
                    StartCoroutine(AnimateSword(270f, -.5f));
                    break;
                }
            case AttkDir.RIGHT:
                {
                    // move the sword to the right; 90 degrees
                    StartCoroutine(AnimateSword(90f, 0f));
                    break;
                }
            case AttkDir.UP:
                {
                    // move the sword so it faces up
                    StartCoroutine(AnimateSword(180f, 0f));
                    break;
                }
        }
    }

    // T29 coroutine to run with LinkAttack, to rotate the sword, 
    // and if we want it to do a vertical movement
    IEnumerator AnimateSword(float rotation, float vertMove)
    {
        // make the sword appear by refrencing it and enabling it
        swordGO.GetComponent<Renderer>().enabled = true;

        // we will rotate the sword by getting passed in the rotation value for z
        // the Sword is inside the Weapon empty, which is inside the Link empty.
        weaponGO.transform.rotation = Quaternion.Euler(0, 0, rotation);

        // potntially move it vertically, when the sword goes from pointing downwards to upwards
        weaponGO.transform.position = new Vector3(transform.position.x, transform.position.y + vertMove, transform.position.z);

        // wait a second before hiding the sword again
        yield return new WaitForSeconds(.2f);

        // disable the sword so it dissapears
        swordGO.GetComponent<Renderer>().enabled = false;
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
