using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // T28 Put Link here to track his movement, this is where he is stored
    public Transform cameraTarget;

    // T28 define how qucikly the camera will move
    public float cameraSpeed;

    // define x and y movements Min and max X and Y movements
    // this will help keep Link centered in the screen
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // T28 fixed update always when dealing with rigidBody 2d.
    private void FixedUpdate()
    {
        // T28 assign the target, Link, a target Camera, if not already assigned to one
        if (cameraTarget != null)
        {
            // we will use lerp to smooth the movement from the starting position 
            // to the target position.  newPosition, for camera, is its targets position,
            // then references where the camera is at, now, Time.deltaTime * cameraSpeed how fast we are going to move there
            var newPos = Vector2.Lerp(transform.position, cameraTarget.position, Time.deltaTime * cameraSpeed);

            // define the cameras new position, 
            var vect3 = new Vector3(newPos.x, newPos.y, -10f);

            // use clamp to get x position, then hold it between the minx and maxX values
            var clampX = Mathf.Clamp(vect3.x, minX, maxX);
            var clampY = Mathf.Clamp(vect3.y, minY, maxY);

            // move our camera by passing in clampx and clampy then goint to those positions
            transform.position = new Vector3(clampX, clampY, -10f);
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
