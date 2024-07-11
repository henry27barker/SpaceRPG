using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    //public GameObject wall;

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
        if(columnPos == columnHeight / 2 && rowPos == rowHeight / 2){
            rand = Random.Range(0, 4);
            GameObject[] temp;
            if(rand == 0){
                temp = bottomRooms;
            }
            else if(rand == 1){
                temp = topRooms;
            }
            else if(rand == 2){
                temp = leftRooms;
            }
            else{
                temp = rightRooms;
            }
            rand = Random.Range(0, temp.Length);
            rooms[columnPos, rowPos] = Instantiate(temp[rand], transform.position, Quaternion.identity);
            for(int i = 0; i < temp[rand].GetComponent<RoomType>().openingDirections.Count; i++){
                if(temp[rand].GetComponent<RoomType>().openingDirections[i] == 1){
                    MakeLevel(columnPos, rowPos + 1);
                }
                if(temp[rand].GetComponent<RoomType>().openingDirections[i] == 2){
                    MakeLevel(columnPos + 1, rowPos);
                }
                if(temp[rand].GetComponent<RoomType>().openingDirections[i] == 3){
                    MakeLevel(columnPos, rowPos - 1);
                }
                if(temp[rand].GetComponent<RoomType>().openingDirections[i] == 4){
                    MakeLevel(columnPos - 1, rowPos);
                }
            }
        }
        else if(columnPos < columnHeight && columnPos > 0 && rowPos < rowHeight && rowPos > 0){
            if(rooms[columnPos, rowPos + 1] != null && rooms[columnPos, rowPos + 1].GetComponent<RoomType>().openingDirections.Contains(3)){
                //Instantiate()
            }
        }
    }
}
