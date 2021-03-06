using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// T26 Used so I can use Floor and round values
using System;

public class Gameboard : MonoBehaviour
{
    // T26 Holds all game objects in an array,
    // so they can move and interact with other objects and know where Link is, always

    // T26 TODO Remember to change space under rock to stairs
    // after rock is moved
    // TODO Change to Gem and then Stump after the bush is moved
    // Do the same for Boomarang, Bomb, Bow, Vace

    // TODO THIS ARRAY NEED FIXING, SHOULD BE (28,24), Some if the HILL vectors are wrong!
    // T26 Types of objects for our array Hill, Tree, Bush, House, Boomarang,
    // Bomb, Stone, Bow, Vace, Gem, Stairs, Door, Chest
    // array is the dimmensions of your game x and y.
    public string[,] gameObjects = new string[29, 26];

    // Start is called before the first frame update
    void Start()
    {
        // T26 load all the following objects, when game starts, into the array
        // we will load our objects using the AddYRowXRange(), and AddXColYRange() functions, below
        // AddYRowXRange() first value is y = 0, second value start x value, third is x end value, the object type
        // we will add the objects by there type, and by there groups of types, starting from the 0,0 position

        AddYRowXRange(0, 0, 28, "Hill");
        AddYRowXRange(24, 0, 28, "Hill");
        AddYRowXRange(3, 0, 7, "Hill");
        AddYRowXRange(4, 0, 7, "Hill");
        AddYRowXRange(5, 0, 7, "Hill");
        AddYRowXRange(6, 0, 6, "Hill");
        AddYRowXRange(9, 10, 12, "Hill");
        AddYRowXRange(10, 12, 13, "Hill");
        AddYRowXRange(13, 0, 10, "Hill");
        AddYRowXRange(14, 3, 4, "Hill");
        AddYRowXRange(14, 8, 9, "Hill");
        AddYRowXRange(20, 0, 6, "Hill");
        AddYRowXRange(21, 1, 2, "Hill");
        AddYRowXRange(21, 3, 4, "Hill");
        AddYRowXRange(21, 5, 6, "Hill");
        AddYRowXRange(21, 11, 12, "Hill");
        AddYRowXRange(21, 13, 14, "Hill");
        AddYRowXRange(20, 10, 12, "Hill");
        AddYRowXRange(17, 17, 26, "Hill");
        AddYRowXRange(16, 17, 26, "Hill");
        AddYRowXRange(10, 18, 24, "Hill");
        AddYRowXRange(9, 17, 25, "Hill");
        AddYRowXRange(8, 17, 25, "Hill");

        AddXColYRange(0, 0, 24, "Hill");
        AddXColYRange(13, 0, 19, "Hill");
        AddXColYRange(14, 0, 19, "Hill");
        AddXColYRange(15, 0, 19, "Hill");
        AddXColYRange(28, 0, 24, "Hill");

        AddYRowXRange(22, 16, 17, "Tree");
        AddYRowXRange(23, 16, 17, "Tree");
        AddYRowXRange(22, 19, 20, "Tree");
        AddYRowXRange(23, 19, 20, "Tree");
        AddYRowXRange(20, 24, 25, "Tree");
        AddYRowXRange(19, 24, 25, "Tree");

        AddYRowXRange(2, 3, 5, "Bush");
        AddYRowXRange(23, 24, 25, "Bush");
        AddXColYRange(1, 8, 10, "Bush");
        AddXColYRange(19, 1, 1, "Bush");
        AddXColYRange(21, 3, 3, "Bush");
        AddXColYRange(23, 5, 5, "Bush");

        AddXColYRange(20, 12, 14, "House");
        AddXColYRange(21, 13, 14, "House");
        AddXColYRange(22, 12, 14, "House");

        // Place single game objects
        gameObjects[8, 11] = "Boomarang";
        gameObjects[1, 17] = "Bomb";
        gameObjects[26, 11] = "Stone";
        gameObjects[23, 4] = "Bow";
        gameObjects[21, 12] = "Door";
    }



    // T26 get  object names, by the row they are in,
    // so we can load them into the array, with its Game Object name, same y value changing x value
    void AddYRowXRange(int yRow, int xStart, int xEnd, string gOName)
    {
        // T26 cycle through all the assets
        for(int i = xStart; i <= xEnd; i++)
        {
            // get the array and go up by i
            gameObjects[i, yRow] = gOName;
        }
    }

    // T26 get  object names, by the column they are in,
    // so we can load them into the array, with its Game Object name, same x value changing y value
    void AddXColYRange(int xColumn, int yStart, int yEnd, string gOName)
    {
        // T26 cycle through all the assets
        for (int i = yStart; i <= yEnd; i++)
        {
            // get the array and go up by i
            gameObjects[xColumn, i] = gOName;
        }
    }

    // T26 verify if there is a valid space for Link to move into
    // horzMove and vertMove will be created in the Link.cs, later on
    // this function may or may not be used, through out the game, but for now,
    // will not allow Link to move into a 0,0 position, if equal to 0 then add one
    public bool IsValidSpace(float x, float y, float horzMove, float vertMove)
    {
        x = horzMove < 0 ? x + 1 : x;
        y = vertMove < 0 ? y + 1 : y;

        // we have to use floats because that is what vectors work with, 
        // keep link from falling off the screen
        x = (float)Math.Floor(Convert.ToDouble(x));
        y = (float)Math.Floor(Convert.ToDouble(y));

        // if there is no object (null) there, yes we can move there
        // convert x, y to int see if it is empty
        if(gameObjects [(int)x, (int)y] == null)
        {
            return true;
        }
        else
        {
            // something is in that vector
            return false;
        }
    }
}
