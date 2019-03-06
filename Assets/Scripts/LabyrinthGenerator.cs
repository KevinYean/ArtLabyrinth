using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthGenerator : MonoBehaviour
{

    //2D Array which will holds the [3][3]. Make it public so it can be flexible later on, 0 for blank, 1 for currenth path, 1 for start, 2 for end
    int[,] labyrinthBoard;
    int rowSize = 4;
    int colSize = 4;
    int effiency = 0;
    List<string> labyrinthPath = new List<string>();
    public List<Coordinates> labyrinthCoordinates = new List<Coordinates>();

    // Start is called before the first frame update
    void Awake()
    {
        //1. Initialize 2D array to hold zeroes
        labyrinthBoard = new int[rowSize, colSize];
        //2. Call PathGenerator
        PathGenerator();
    }

    //Function to generate the path
    void PathGenerator() {
        //[][] value that keeps track of the current path
        int currentRow, currentCol;
        int endRow, endCol;

        //1. Set starting point from the array. [0,0] //Set starting point as 1.
        currentRow = 0; currentCol = 0;
        Coordinates coord = new Coordinates(currentRow, currentCol);
        labyrinthCoordinates.Add(coord);
        //2. Set end point from the array. [2,2]
        endRow = 3; endCol = 3;
        SetLabyrinthPath(endRow, endCol, -1,labyrinthBoard);
        //3. While the current path is not the endpath.
        int pathNumber = 0;
        while ( currentRow!=endRow || currentCol != endCol) {
            pathNumber++;
            //3.1. From this position call Pathsearch on each possible movements available [up, right, down , left]
            //3.2. Keep track of which path have a possible solution randomly select one of the path.
            SetLabyrinthPath(currentRow, currentCol, pathNumber, labyrinthBoard);
            var listMoves = new List<string>();
            bool left = false;bool right = false;bool up = false;bool down = false;

            if ((currentRow + 1) < rowSize) { //Up
                if (PathSearch(currentRow + 1, currentCol, labyrinthBoard.Clone() as int[,], ref up)) {
                    listMoves.Add("up");
                }
            }
            if ( (currentCol+1) < colSize) { //Right
                if(PathSearch(currentRow, currentCol+1, labyrinthBoard.Clone() as int[,], ref right)) {
                    listMoves.Add("right");
                }
            }
            if ( (currentRow-1) >= 0) { //Down
                if (PathSearch(currentRow-1, currentCol, labyrinthBoard.Clone() as int[,], ref down)) {
                    listMoves.Add("down");
                }
            }
            if ((currentCol - 1) >= 0) { //Left
                if (PathSearch(currentRow, currentCol - 1, labyrinthBoard.Clone() as int[,], ref left)) {
                    listMoves.Add("left");
                }
            }
            //3.3. Update Array to make that one the currentpath.
            string path = listMoves[Random.Range(0, listMoves.Count)];
            if (path == "up") {
                currentRow = currentRow + 1;
            }
            else if (path == "left") {
                currentCol = currentCol - 1;
            }
            else if (path == "down") {
                currentRow = currentRow - 1;
            }
            else { //Right
                currentCol = currentCol + 1;
            }
            coord = new Coordinates(currentRow,currentCol);
            labyrinthCoordinates.Add(coord);
            labyrinthPath.Add(path);
        }
        SetLabyrinthPath(endRow, endCol, pathNumber+1, labyrinthBoard);
        Debug.Log(LabyrinthPathToString(labyrinthBoard));
        //Debug.Log("Effiency: " + effiency);
    }

    //Recursive function to checks paths
    bool PathSearch(int row,int col,int[,] board, ref bool move) {
        effiency++;
        //1. Check position
        int value = labyrinthBoard[row, col];
        //2. If 1, return false
        if(move == true) {
            return true;
        }
        if (value >= 1) {
            return false;
        }
        //   If 2, return true
        if (value == -1) {
            move = true;
            //Debug.Log("Hit");
            return true;
        }
        //   If 0, Call Pathsearch on each possible available [up,right,down,left]
        else {
            SetLabyrinthPath(row, col, 1, board);
            int[,] testBoard = labyrinthBoard.Clone() as int[,];

            if ((row + 1) < rowSize) { //Up
                return PathSearch(row + 1, col, testBoard, ref move);
            }
           testBoard = labyrinthBoard.Clone() as int[,];

            if ((col + 1) < colSize) { //Right
                return PathSearch(row, col + 1, testBoard, ref move);
            }
          testBoard = labyrinthBoard.Clone() as int[,];

            if ((col- 1) > 0) { //Down
                return PathSearch(row, col - 1, testBoard, ref move);
            }
          testBoard = labyrinthBoard.Clone() as int[,];
            if ((row - 1) > 0) { //Left
                return PathSearch(row - 1, col, testBoard, ref move);
            }
        }
        return move;
    }

    void SetLabyrinthPath(int row, int col,int value,int[,] board) {
        //1. Use function variable to update the array
        board[row, col] = value;
    }

    string LabyrinthPathToString(int[,] board) {
        //Variable
        string labyrinthString ="";
        for (int i = rowSize-1; i >= 0; i--){
            for (int x = 0; x < colSize; x++) {
                labyrinthString += board[i, x] + " ";
            }
            labyrinthString += "\n";
        }
        return labyrinthString;
    }

    /// <summary>
    /// Function that returns map of the labyrinth
    /// </summary>
    /// <returns></returns>
    public int[,] GetLabyrinthMap() {
        return labyrinthBoard;
    }

    public List<string> GetLabyrinthPath() {
        return labyrinthPath;
    }

    public List<Coordinates> GetLabyrinthCoordinates() {
        return labyrinthCoordinates;
    }
}
