using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthGenerator : MonoBehaviour
{
    //2D Array which will holds the [3][3]. Make it public so it can be flexible later on, 0 for blank, 1 for currenth path, 2 for start, 3 for end

    // Start is called before the first frame update
    void Start()
    {
        //1. Initialize 2D array to hold zeroes
        //2. Call PathGenerator
    }

    //Function to generate the path
    void PathGenerator() {
        //Variables
        //[][] value that keeps track of the current path

        //1. Set starting point from the array. [0,0] //Set starting point as 1.
        //2. Set end point from the array. [2.2]
        //3. While the current path is not the endpath.
            //3.1. From this position call Pathsearch on each possible movements available [up, right, down , left]
            //3.2. Keep track of which path have a possible solution randomly select one of the path.
            //3.3. Update Array to make that one the currentpath.
    }

    //Recursive function to checks paths
    void PathSearch() {
        //1. Check position
        //2. If 1, return false
        //   If 3, return true
        //   If 0, Call Pathsearch on each possible available [up,right,down,left]
    }


    void UpdateLabyrinthPath() {
        //1. Use function variable to update the array
    }

    void LabyrinthPathToString() {
        //Variable
        //string to print the path
        //For loop row
            //For loop column
        //Return string of paths
    }
}
