using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Gamestate
{
    //Roads? Where we're going. We don't need roads. 
    public static bool initialized = false;

    //Inventory
    public static int[] inventory = new int[15];

    //Score
    public static int score;

    public static int accomplishment; // what was just crossed of the to do list

    public static int day;

    public static int lastDayHunted; //are roaches gone today?

    public static int disaster;

    // some booleans to keep track of the summer plan achivements
    public static bool beach = false;
    public static bool iceCream = false;
    public static bool movie = false;
    public static bool baseball = false;
    public static bool videoGames = false;
    public static bool bicycle = false;

    // some booleans to keep track of school shutdowns
    public static bool closedRoaches = false;
    public static bool closedElectrical = false;
    public static bool closedPlumbing = false;
    public static bool closedKeys = false;
    public static bool closedSmoke = false;

    // some booleans to keep track of the experiment
    public static bool scienceFan = false;
    public static bool scienceFanCase = false;
    public static bool scienceRefrigerant = false;
    public static bool scienceBakingSoda = false;
    public static bool scienceChemical = false;
    public static bool scienceWater = false;
}
