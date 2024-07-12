using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject[] allRoomTypes;
    //public GameObject wall;

    //1->top
    //2->right
    //3->bottom
    //4->left

    public GameObject[,] rooms;

    public int columnHeight = 20;
    public int rowHeight = 20;

    private int rand;


    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        rooms = new GameObject[columnHeight,rowHeight];
        MakeLevel(columnHeight / 2, rowHeight / 2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakeLevel(int columnPos, int rowPos){
        Debug.Log(columnPos + " " + rowPos);
        if(columnPos == columnHeight / 2 && rowPos == rowHeight / 2){
            rand = Random.Range(0, allRoomTypes.Length);
            rooms[columnPos, rowPos] = Instantiate(allRoomTypes[rand], transform.position, Quaternion.identity);
            if(rooms[columnPos, rowPos].GetComponent<RoomType>().openingDirections.Contains(1) && rooms[columnPos, rowPos + 1] == null){
                MakeLevel(columnPos, rowPos + 1);
            }
            if(rooms[columnPos, rowPos].GetComponent<RoomType>().openingDirections.Contains(2) && rooms[columnPos + 1, rowPos] == null){
                MakeLevel(columnPos + 1, rowPos);
            }
            if(rooms[columnPos, rowPos].GetComponent<RoomType>().openingDirections.Contains(3) && rooms[columnPos, rowPos - 1] == null){
                MakeLevel(columnPos, rowPos - 1);
            }
            if(rooms[columnPos, rowPos].GetComponent<RoomType>().openingDirections.Contains(4) && rooms[columnPos - 1, rowPos] == null){
                MakeLevel(columnPos - 1, rowPos);
            }
        }
        else if(columnPos < columnHeight - 1 && columnPos > 0 && rowPos < rowHeight - 1 && rowPos > 0){
            List<int> needOpening = new List<int>();
            List<int> needClosed = new List<int>();
            if(rooms[columnPos, rowPos + 1] != null && rooms[columnPos, rowPos + 1].GetComponent<RoomType>().openingDirections.Contains(3)){
                needOpening.Add(1);
            }
            if(rooms[columnPos, rowPos + 1] != null && !rooms[columnPos, rowPos + 1].GetComponent<RoomType>().openingDirections.Contains(3)){
                needClosed.Add(1);
            }
            if(rooms[columnPos + 1, rowPos] != null && rooms[columnPos + 1, rowPos].GetComponent<RoomType>().openingDirections.Contains(4)){
                needOpening.Add(2);
            }
            if(rooms[columnPos + 1, rowPos] != null && !rooms[columnPos + 1, rowPos].GetComponent<RoomType>().openingDirections.Contains(4)){
                needClosed.Add(2);
            }
            if(rooms[columnPos, rowPos - 1] != null && rooms[columnPos, rowPos - 1].GetComponent<RoomType>().openingDirections.Contains(1)){
                needOpening.Add(3);
            }
            if(rooms[columnPos, rowPos - 1] != null && !rooms[columnPos, rowPos - 1].GetComponent<RoomType>().openingDirections.Contains(1)){
                needClosed.Add(3);
            }
            if(rooms[columnPos - 1, rowPos] != null && rooms[columnPos - 1, rowPos].GetComponent<RoomType>().openingDirections.Contains(2)){
                needOpening.Add(4);
            }
            if(rooms[columnPos - 1, rowPos] != null && !rooms[columnPos - 1, rowPos].GetComponent<RoomType>().openingDirections.Contains(2)){
                needClosed.Add(4);
            }
            List<GameObject> possibleRooms = new List<GameObject>();
            foreach(GameObject temp in allRoomTypes){
                foreach(int i in needOpening){
                    if(!temp.GetComponent<RoomType>().openingDirections.Contains(i)){
                        goto EndLoop;
                    }
                }
                foreach(int i in needClosed){
                    if(temp.GetComponent<RoomType>().openingDirections.Contains(i)){
                        goto EndLoop;
                    }
                }
                possibleRooms.Add(temp);
                EndLoop:
                    ;
            }
            if(possibleRooms.Count != 0){
                rand = Random.Range(0, possibleRooms.Count);
                int columnAdjustment = (columnPos - columnHeight / 2) * 4;
                int rowAdjustment = (rowPos - rowHeight / 2) * 4;
                Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                rooms[columnPos, rowPos] = Instantiate(possibleRooms[rand], newPosition, Quaternion.identity);
                if(rooms[columnPos, rowPos].GetComponent<RoomType>().openingDirections.Contains(1) && rooms[columnPos, rowPos + 1] == null){
                    MakeLevel(columnPos, rowPos + 1);
                }
                if(rooms[columnPos, rowPos].GetComponent<RoomType>().openingDirections.Contains(2) && rooms[columnPos + 1, rowPos] == null){
                    MakeLevel(columnPos + 1, rowPos);
                }
                if(rooms[columnPos, rowPos].GetComponent<RoomType>().openingDirections.Contains(3) && rooms[columnPos, rowPos - 1] == null){
                    MakeLevel(columnPos, rowPos - 1);
                }
                if(rooms[columnPos, rowPos].GetComponent<RoomType>().openingDirections.Contains(4) && rooms[columnPos - 1, rowPos] == null){
                    MakeLevel(columnPos - 1, rowPos);
                }
            }
        }
    }
}
