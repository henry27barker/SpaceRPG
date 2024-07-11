using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    /*
    public int openingDirection;
    // 1->need bottom opening
    // 2->need top opening
    // 3->need left opening
    // 4->need right opening

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;
    public bool isWall;

    // Start is called before the first frame update
    void Start()
    {
        Random.seed = System.DateTime.Now.Millisecond;
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    // Update is called once per frame
    void Spawn()
    {
        if(spawned == false && isWall == false){
            if(openingDirection == 1){
                //need to spawn with bottom opening
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if(openingDirection == 2){
                //need to spawn with top opening
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if(openingDirection == 3){
                //need to spawn with left opening
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if(openingDirection == 4){
                //need to spawn with right opening
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            spawned = true;
        }
        // else if(spawned == false && isWall == true){
        //     Instantiate(templates.wall, transform.position, Quaternion.identity);
        //     spawned = true;
        // }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("SpawnPoint")){
            // if(other.GetComponent<RoomSpawner>().isWall == true && isWall == true){
            //     //spawn walls to block off opening
            //     //Instantiate(templates.wall, transform.position, Quaternion.identity);
            //     if(openingDirection < other.GetComponent<RoomSpawner>().openingDirection)
            //         Destroy(gameObject);
            // }
            // else if(other.GetComponent<RoomSpawner>().isWall == true){
            //     Destroy(gameObject);
            // }
            // else if(isWall == true){
            //     // Instantiate(templates.wall, transform.position, Quaternion.identity);
            //     // spawned = true;
            //     Destroy(other.gameObject);
            // }
            // if(other.GetComponent<RoomSpawner>().spawned == false && spawned == false){
            //     //Instantiate(templates.wall, transform.position, Quaternion.identity);
            //     // if(openingDirection < other.GetComponent<RoomSpawner>().openingDirection)
            //         Destroy(gameObject);
            // }
            spawned = true;
        }
    }
    */
}
