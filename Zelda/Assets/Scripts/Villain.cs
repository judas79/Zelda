using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villain : MonoBehaviour
{
    // T29 define the villains speed, public so we can easily tweak it
    public float speed = 2f;

    // T29The villains will all start going right when loaded, and appear on screen
    Vector2 direction = Vector2.right;

    // T29 Keeps track of, if Villain is chasing Link, or not
    // the villian will bounce back and forth between the VillainStart and VillainEnd Empty, points
    // until a chase is initiated.
    bool chasing = true;

    // T29 Get reference to the Animator, so we can change the animations
    private Animator animator;

    // T29 we will create a Link GameObject later, but here is its reference.
    // public so we can drag and drop the Link, into it later on in the scripts fields.
    GameObject linkGO;

    // T29 define Rigidbody 2d, to move Villain
    Rigidbody2D rb;

    // T30 array to hold all our travelNodes, by there position
    int[,] villainNodes = new int[,]{
        {2, -4}, { 11, -4 }, { 9,  0 }, { 2, 6 }, { 2,  16 }, { 8, 15 }, { 4, 11 }, { 5,  8 }, { 16, 9 }, { 20, 16 }, { 27, 10 }, { 25,  2 }, { 21, 5 }
    };

    // T30 access to our current Villain node, in our index
    public int currentNode = 1;

    // track where are target node is, where Villain is going to go to
    public int targetNode = 2;

    // T30 array to keep track of the Node nearest to Link, by the Nodes index value
    GameObject[] villainNodeGOs;

    // T30 store the Nearest Nodes location that is nearest to Link
    GameObject linksClosestNodeGO;

    // T29 initialize things, so they are setup and ready when called upon to perform
    void Awake()
    {
        // get our reference to the Link Component, for our linkGO,
        // use find to get the only Link character in the game
        linkGO = GameObject.Find("Link");

        // setup the rigidbody 2d
        rb = GetComponent<Rigidbody2D>();

        // setup the animator
        animator = GetComponent<Animator>();

        // T30 initialize villainNodeGOs array, to get all the Nodes
        villainNodeGOs = GameObject.FindGameObjectsWithTag("Villain");

    }

    // T29 fixed update will call our rigidbody and change its velocity, on a regular basis
    void FixedUpdate()
    {
        // change rigidbody2d velocity
        //rb.velocity = direction * speed; //T30 commented out and moved to the Update()
        //GetComponent<Rigidbody2D>().velocity = direction * speed;

        // T30 make the nodes work for now, using a key, and automate them later
        // watch how Villain moves between the nodes, by hitting the f key, and going forward,
        // to debug, if it gets caught on objects
        if (Input.GetKeyUp("f"))
        {
            // go to a node that is less than 12 index spaces away out of 13
            if (targetNode < 12)
            {
                // as long as we have 1 node left before the array runs out, 
                // we will will increment our targetNode, to keep count
                targetNode = targetNode + 1;

                // use our function to move forward to that next node
                GoForwardToNode();

                // T30 output Links position, to our array, using our function LinksClosestNodeGO() to get the closest GameObject Node
                linksClosestNodeGO = LinksClosestNode();

                // T30 we also will want the GameObjects node index,
                Debug.Log("Links closest Nodes index # " + GetIndexForLinksClosestNode(linksClosestNodeGO));
            }

        }
        // T30 go backwards through the nodes, to debug
        if (Input.GetKeyUp("b"))
        {
            // go to a node that is less than 12 index spaces away out of 13
            if (targetNode > 0)
            {
                // as long as we have 1 node left before the array gets to index[0], 
                // we will will decrement our targetNode, to keep count
                targetNode = targetNode - 1;

                // use our function to move forward to that next node
                GoForwardToNode();
            }
        }
    }
    // T30 setup the way to move the Villain to the next node
    void GoForwardToNode()
    {
        // if we are Not already at the Target Node, we want to go there
        if (!AtTargetNode())
        {
            // get the x and y positions, that we want to move to, for our TargetNode, from our villainNodes array
            // by subtracting Villains current position from the targetNodes position
            Vector2 targetPosition = new Vector2(villainNodes[targetNode, 0] - transform.position.x, villainNodes[targetNode, 1] - transform.position.y);

            Debug.Log("Target X : " + villainNodes[targetNode, 0] +
            "Y : " + villainNodes[targetNode, 1]);

            Debug.Log("Target Node : " + targetNode);

            // set the direction to our target
            direction = targetPosition;
        }
    }

    // T30 how to check if Villain is at the target node
    bool AtTargetNode()
    {
        // if Villains x posiion is equal to the x position of VillainNodes targetNode 0 (0 means the x position) 
        // and Villains y position equals the y position of the VillainNodes targetNode 1 (1 means the x position)
        // its at the targetNode
        if (transform.position.x == villainNodes[targetNode, 0] && transform.position.y == villainNodes[targetNode, 1])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // T29 handle when our Villain hits one of those triggers
    void OnTriggerEnter2D(Collider2D col)
    {
        // we hit a trigger now what
        // find out if we where chasing  or not,
        // if we where not chasing, and collided with the VillainStart or VillainEnd
        // we will change the x direction our Villain is facing, by flipping it, which reacts super quick
        if (!chasing)
        {
            transform.localScale = new Vector2(-1 * transform.localScale.x,
                transform.localScale.y);

            // also change the direction that we are moving in
            direction = new Vector2(-1 * direction.x, direction.y);
        }
        // TODO else is for when our villain is chasing Link
        else
        {

        }

        // T30 check to see if our Villain collided with a VillainNode
        if (col.tag == "VillainNode")
        {
            // and if it is the TargetNode, then stop moving
            if (col.transform.position.x == villainNodes[targetNode, 0] && col.transform.position.y == villainNodes[targetNode, 1])
            {
                direction = Vector3.zero;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // T30 change rigidbody2d velocity, moved here from FixedUpdate()
        rb.velocity = direction;
    }

    // T30 get the node that is nearest to Link, by returning that Node
    GameObject LinksClosestNode()
    {
        // The closest node will start off with a value of null
        GameObject closestNodeGo = null;

        // cycle through all of our villain Node game objects
        foreach (GameObject nodeGO in villainNodeGOs)
        {
            // if the node has no value
            if (closestNodeGo == null)
            {
                // then we will make the first one equal to null, 
                // then continue cycling through the list, from there, till we find one that isn't null
                closestNodeGo = nodeGO;
            }
            // check the distance between Links and the Nodes, the current node we are cycling through
            // or the last one that wasn't null, to see which node, of the two is closest to link
            if (Vector3.Distance(linkGO.transform.position, nodeGO.transform.position) <= Vector3.Distance(linkGO.transform.position, closestNodeGo.transform.position))
            {
                // Assign the closest gameobject for comparison
                closestNodeGo = nodeGO;
            }
        }
        // return our closest node
        return closestNodeGo;
    }

    // T30 get the index for the closest node that we found, above in LinksClosestNode()
    // this will return an int
    int GetIndexForLinksClosestNode(GameObject linksNodeGO)
    {
        int nodeIndex = 0;
        for (int i = 0; i <= villainNodes.GetUpperBound(0); i++)
        {
            Debug.Log("X : " + villainNodes[i, 0]);
            Debug.Log("Y : " + villainNodes[i, 1]);

            // check inside the index for value of x at the index of i, 0 is the x position
            // check inside the index for value of y at the index of i, 1 is the y position
            if (linksNodeGO.transform.position.x == villainNodes[i, 0] &&
            linksNodeGO.transform.position.y == villainNodes[i, 1])
            {
                // if both x and y are true assign the index number to nodeIndex
                nodeIndex = i;
            }
        }
        return nodeIndex;
    }
}
