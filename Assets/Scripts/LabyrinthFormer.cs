using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthFormer : MonoBehaviour
{
    public struct Coordinate {
        public int xCoord;
        public int yCoord;

        public Coordinate(int x, int y) {
            xCoord = x;
            yCoord = y;
        }
    }

    public GameObject uBase;
    public GameObject hallBase;
    public GameObject cornerBase;
    public GameObject finishLine;
    public CameraControl camera;
    public PlayerControl player;

    public LabyrinthGenerator labyrinth;

    // Start is called before the first frame update
    void Start()
    {
        List<Coordinates> t = new List<Coordinates>();
        Coordinates coord = new Coordinates(1, 1);
        t.Add(coord);
        coord = new Coordinates(1, 2);
        t.Add(coord);
        coord = new Coordinates(0, 2);
        t.Add(coord);
        LabyrinthMap(labyrinth.GetLabyrinthCoordinates());
        //LabyrinthMap(t);
    }

    public void LabyrinthMap(List<Coordinates> path) {
        int previousRow = 0; int previousCol = 0;
        int nextRow = 0; int nextCol = 0;
        GameObject labyrinthBase = null ;
        int tracker = 0;
        for (int i = 0; i < path.Count; i++) {
            Debug.Log("Row: " + path[i].zCoord + " Col: " + path[i].xCoord);

            if (i == 0) { //First Piece
                labyrinthBase = GameObject.Instantiate(uBase, transform);
                //Rotate correct order
                if (path[i + 1].zCoord > path[i].zCoord) { //Up
                    labyrinthBase.transform.RotateAround(transform.position, transform.up, 180);
                    camera.yaw = 0f; player.yaw = 0f;

                }
                else if (path[i + 1].zCoord < path[i].zCoord) { //Down
                    labyrinthBase.transform.RotateAround(transform.position, transform.up, 0);
                }
                else if (path[i + 1].xCoord > path[i].xCoord) { //Right
                    labyrinthBase.transform.RotateAround(transform.position, transform.up, 270);
                    camera.yaw = 90f; player.yaw = 90f;
                }
                else if (path[i + 1].xCoord < path[i].xCoord) { //Left
                    labyrinthBase.transform.RotateAround(transform.position, transform.up, 90);
                }

            }
            else if (i == path.Count - 1) { //Last Piece
                labyrinthBase = GameObject.Instantiate(uBase, transform);
                GameObject end = GameObject.Instantiate(finishLine, transform);
                end.transform.position = new Vector3((path[i].xCoord) * 8f, 0f + end.transform.position.y, (path[i].zCoord) * 8f);
                if (path[i - 1].zCoord > path[i].zCoord) { //Up
                    labyrinthBase.transform.RotateAround(transform.position, transform.up, 180);
                }
                else if (path[i - 1].zCoord < path[i].zCoord) { //Down
                    labyrinthBase.transform.RotateAround(transform.position, transform.up, 0);
                }
                else if (path[i - 1].xCoord > path[i].xCoord) { //Right
                    labyrinthBase.transform.RotateAround(transform.position, transform.up, 270);
                }
                else if (path[i - 1].xCoord < path[i].xCoord) { //Left
                    labyrinthBase.transform.RotateAround(transform.position, transform.up, 90);
                }
            }
            else { //Other Pieces
                if (i + 1 < path.Count) {
                    nextRow = path[i + 1].zCoord;
                    nextCol = path[i + 1].xCoord;

                    if (previousRow == nextRow || previousCol == nextCol) { //Hall
                        labyrinthBase = GameObject.Instantiate(hallBase, transform);
                        if(previousRow == nextRow) {
                            labyrinthBase.transform.RotateAround(transform.position, transform.up, 90);
                        }
                    }
                    else { //Corner
                        labyrinthBase = GameObject.Instantiate(cornerBase, transform);
                        if (previousRow < path[i].zCoord) {
                            if (path[i].xCoord < nextCol) { //Up then Right
                                labyrinthBase.transform.RotateAround(transform.position, transform.up, 0);
                            }
                            else if (path[i].xCoord > nextCol) {//Up then Left
                                labyrinthBase.transform.RotateAround(transform.position, transform.up, 90);
                            }
                        }
                        else if (previousRow > path[i].zCoord) {
                            if (path[i].xCoord < nextCol) { //Down then Right
                                labyrinthBase.transform.RotateAround(transform.position, transform.up, 270);
                            }
                            else { //Down then Left
                                labyrinthBase.transform.RotateAround(transform.position, transform.up, 180);
                            }

                        }
                        else if (previousCol < path[i].xCoord) {
                            if (path[i].zCoord < nextRow) { //Right and Up
                                labyrinthBase.transform.RotateAround(transform.position, transform.up, 180);
                            }
                            else { //Right and Down                
                                labyrinthBase.transform.RotateAround(transform.position, transform.up, 90);
                            }
                        }
                        else if (previousCol > path[i].xCoord) {
                            if(path[i].zCoord < nextRow) { //Left and Up
                                labyrinthBase.transform.RotateAround(transform.position, transform.up, 270);
                            }
                            else { //Left and Down                      
                                labyrinthBase.transform.RotateAround(transform.position, transform.up, 0);
                            }
                        }
                    }
                }
            }
            labyrinthBase.transform.position = new Vector3((path[i].xCoord) * 8f, 0f, (path[i].zCoord) * 8f);
            previousRow = path[i].zCoord;
            previousCol = path[i].xCoord;
        }
    }
}
