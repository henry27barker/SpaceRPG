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

    public GameObject[] allWallTypes;
    public GameObject[] enemyTypes;
    public int[] enemyTypeChances;

    public GameObject lamp;
    public GameObject crate;
    public GameObject enemy;
    public GameObject mine;

    public GameObject door;

    public Sprite window;

    public int flickerChance = 10;

    public int windowChance = 20;

    public int lampChance = 8;
    public int crateChance = 8;
    public int enemyChance = 8;
    public int mineChance = 5;

    private bool enemySpawned = false;
    private bool crateSpawned = false;
    private bool mineSpawned = false;

    private GameObject player;

    private Inventory incrementers;
    //public GameObject wall;

    //1->top
    //2->right
    //3->bottom
    //4->left

    public GameObject[,] rooms;

    public int columnHeight = 0;
    public int rowHeight = 0;

    private int lowestIndex;
    private GameObject lowestFloor;
    private GameObject destroyPossibleSpawn;

    private Vector3 highestY = new Vector3(0, -99999, 0);
    private GameObject highestWall;

    private int rand;
    public int spdrBotStartingLevel = 5;
    public float healthIncrementAmount = 10;

    void Awake(){
        incrementers = GameObject.FindWithTag("GameManager").GetComponent<Inventory>();
        columnHeight += incrementers.startingColumnIncrementer;
        rowHeight += incrementers.startingRowIncrementer;
        lowestIndex = rowHeight / 2;
        player = GameObject.FindWithTag("Player");
        Random.InitState(System.DateTime.Now.Millisecond);
        rooms = new GameObject[columnHeight,rowHeight];
        MakeLevel(columnHeight / 2, rowHeight / 2);
        if(destroyPossibleSpawn != null){
            Destroy(destroyPossibleSpawn);
        }
        player.transform.position = lowestFloor.transform.position;
        MakeWalls();
        Instantiate(door, highestY, Quaternion.identity);
        Destroy(highestWall);
        AstarPath.active.Scan();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    GameObject RandomlyGenerateLamp(int chance, Vector3 position){
        if(!enemySpawned){
            if(!crateSpawned){
                if(!mineSpawned){
                    int rand = Random.Range(1, 101);
                    if(rand <= chance){
                        rand = Random.Range(-1, 2);
                        int temp = Random.Range(-1, 2);
                        int flickerRand = Random.Range(1,101);
                        if(flickerRand <= flickerChance){
                            lamp.GetComponent<Flicker>().flickerOn = true;
                        }
                        else{
                            lamp.GetComponent<Flicker>().flickerOn = false;
                        }
                        Vector3 newPosition = new Vector3(position.x + rand, position.y + temp, 0);
                        return Instantiate(lamp, newPosition, Quaternion.identity);
                    }
                    else{
                        return null;
                    }
                }
                return null;
            }
            return null;
        }
        return null;
    }

    GameObject RandomlyGenerateCrate(int chance, Vector3 position){
        if(!enemySpawned){
            int crateRand = Random.Range(1, 101);
            if(crateRand <= chance){
                crateRand = Random.Range(-1, 2);
                int temp = Random.Range(-1, 2);
                Vector3 newPosition = new Vector3(position.x + crateRand, position.y + temp, 0);
                crateSpawned = true;
                return Instantiate(crate, newPosition, Quaternion.identity);
            }
            else{
                crateSpawned = false;
                return null;
            }
        }
        return null;
    }

    GameObject RandomlyGenerateEnemy(int chance, Vector3 position){
        int newRand = Random.Range(1, 101);
        if(newRand <= chance){
            newRand = Random.Range(-1, 2);
            int temp = Random.Range(-1, 2);
            Vector3 newPosition = new Vector3(position.x + newRand, position.y + temp, 0);
            enemySpawned = true;
            int index = 0;
            int rand = Random.Range(1, 101);
            if(rand <= enemyTypeChances[0]){
                if(incrementers.level < spdrBotStartingLevel){
                    rand = Random.Range(enemyTypeChances[0] + 1, 101);
                }
                else{
                    index = 0;
                }
            }
            for(int j = 1; j < enemyTypeChances.Length; j++){
                if(rand > enemyTypeChances[j - 1] && rand <= enemyTypeChances[j])
                    index = j;
            }
            return Instantiate(enemyTypes[index], newPosition, Quaternion.identity);
        }
        else{
            enemySpawned = false;
            return null;
        }
    }

    GameObject RandomlyGenerateMine(int chance, Vector3 position){
        if(!enemySpawned){
            if(!crateSpawned){
                int rand = Random.Range(1, 101);
                if(rand <= chance){
                    rand = Random.Range(-1, 2);
                    int temp = Random.Range(-1, 2);
                    mineSpawned = true;
                    Vector3 newPosition = new Vector3(position.x + rand, position.y + temp, 0);
                    return Instantiate(mine, newPosition, Quaternion.identity);
                }
                else{
                    mineSpawned = false;
                    return null;
                }
            }
            return null;
        }
        return null;
    }

    void MakeLevel(int columnPos, int rowPos){
        Debug.Log(columnPos + " " + rowPos);
        if(columnPos == columnHeight / 2 && rowPos == rowHeight / 2){
            //rand = Random.Range(0, allRoomTypes.Length);
            //rooms[columnPos, rowPos] = Instantiate(allRoomTypes[rand], transform.position, Quaternion.identity);
            rooms[columnPos, rowPos] = Instantiate(allRoomTypes[0], transform.position, Quaternion.identity);
            GameObject tempEnemy = RandomlyGenerateEnemy(enemyChance, transform.position);
            if(tempEnemy != null)
                tempEnemy.GetComponentInChildren<EnemyMovement>().health += (int)((incrementers.level / 5) * healthIncrementAmount);
            RandomlyGenerateCrate(crateChance, transform.position);
            RandomlyGenerateMine(mineChance, transform.position);
            RandomlyGenerateLamp(lampChance, transform.position);
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
                if(rowPos < lowestIndex){
                    lowestIndex = rowPos;
                    lowestFloor = rooms[columnPos, rowPos];
                    GameObject tempGameObject = RandomlyGenerateEnemy(enemyChance, newPosition);
                    if(tempGameObject != null)
                        tempGameObject.GetComponentInChildren<EnemyMovement>().health += (int)((incrementers.level / 5) * healthIncrementAmount);
                    if(tempGameObject != null){
                        destroyPossibleSpawn = tempGameObject;
                    }
                    tempGameObject = RandomlyGenerateCrate(crateChance, newPosition);
                    if(tempGameObject != null){
                        destroyPossibleSpawn = tempGameObject;
                    }
                    tempGameObject = RandomlyGenerateMine(mineChance, newPosition);
                    if(tempGameObject != null){
                        destroyPossibleSpawn = tempGameObject;
                    }
                    tempGameObject = RandomlyGenerateLamp(lampChance, newPosition);
                    if(tempGameObject != null){
                        destroyPossibleSpawn = tempGameObject;
                    }
                }
                else{
                    GameObject tempEnemy = RandomlyGenerateEnemy(enemyChance, newPosition);
                    if(tempEnemy != null)
                        tempEnemy.GetComponentInChildren<EnemyMovement>().health += (int)((incrementers.level / 5) * healthIncrementAmount);
                    RandomlyGenerateCrate(crateChance, newPosition);
                    RandomlyGenerateMine(mineChance, newPosition);
                    RandomlyGenerateLamp(lampChance, newPosition);
                }
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

    void MakeWalls(){
        for(int i = 1; i < columnHeight - 1; i++){
            for(int j = 1; j < rowHeight - 1; j++){
                List<int> roomDirections = new List<int>();
                if(rooms[i,j] == null){
                    if(rooms[i,j+1] != null){
                        roomDirections.Add(1);
                    }
                    if(rooms[i+1,j+1] != null){
                        roomDirections.Add(2);
                    }
                    if(rooms[i+1,j] != null){
                        roomDirections.Add(3);
                    }
                    if(rooms[i+1,j-1] != null){
                        roomDirections.Add(4);
                    }
                    if(rooms[i,j-1] != null){
                        roomDirections.Add(5);
                    }
                    if(rooms[i-1,j-1] != null){
                        roomDirections.Add(6);
                    }
                    if(rooms[i-1,j] != null){
                        roomDirections.Add(7);
                    }
                    if(rooms[i-1,j+1] != null){
                        roomDirections.Add(8);
                    }
                    if(roomDirections.Count == 0){
                        continue;
                    }
                    if(roomDirections.Contains(1) && roomDirections.Contains(3) && roomDirections.Contains(5) && roomDirections.Contains(7)){
                        List<int> temp = new List<int>();
                        temp.Add(1);
                        temp.Add(3);
                        temp.Add(5);
                        temp.Add(7);
                        foreach(GameObject tempGameObject in allWallTypes){
                            if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                int columnAdjustment = (i - columnHeight / 2) * 4;
                                int rowAdjustment = (j - rowHeight / 2) * 4;
                                Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                break;
                            }
                        }
                    }
                    else if(roomDirections.Contains(1) && roomDirections.Contains(3) && roomDirections.Contains(5)){
                        List<int> temp = new List<int>();
                        temp.Add(1);
                        temp.Add(3);
                        temp.Add(5);
                        foreach(GameObject tempGameObject in allWallTypes){
                            if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                int columnAdjustment = (i - columnHeight / 2) * 4;
                                int rowAdjustment = (j - rowHeight / 2) * 4;
                                Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                break;
                            }
                        }
                    }
                    else if(roomDirections.Contains(1) && roomDirections.Contains(3) && roomDirections.Contains(7)){
                        List<int> temp = new List<int>();
                        temp.Add(1);
                        temp.Add(3);
                        temp.Add(7);
                        foreach(GameObject tempGameObject in allWallTypes){
                            if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                int columnAdjustment = (i - columnHeight / 2) * 4;
                                int rowAdjustment = (j - rowHeight / 2) * 4;
                                Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                break;
                            }
                        }
                    }
                    else if(roomDirections.Contains(1) && roomDirections.Contains(5) && roomDirections.Contains(7)){
                        List<int> temp = new List<int>();
                        temp.Add(1);
                        temp.Add(5);
                        temp.Add(7);
                        foreach(GameObject tempGameObject in allWallTypes){
                            if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                int columnAdjustment = (i - columnHeight / 2) * 4;
                                int rowAdjustment = (j - rowHeight / 2) * 4;
                                Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                break;
                            }
                        }
                    }
                    else if(roomDirections.Contains(3) && roomDirections.Contains(5) && roomDirections.Contains(7)){
                        List<int> temp = new List<int>();
                        temp.Add(3);
                        temp.Add(5);
                        temp.Add(7);
                        foreach(GameObject tempGameObject in allWallTypes){
                            if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                int columnAdjustment = (i - columnHeight / 2) * 4;
                                int rowAdjustment = (j - rowHeight / 2) * 4;
                                Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                break;
                            }
                        }
                    }
                    else if(roomDirections.Contains(1) && roomDirections.Contains(3)){
                        if(roomDirections.Contains(6)){
                            List<int> temp = new List<int>();
                            temp.Add(1);
                            temp.Add(3);
                            temp.Add(6);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else{
                            List<int> temp = new List<int>();
                            temp.Add(1);
                            temp.Add(3);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                    }
                    else if(roomDirections.Contains(1) && roomDirections.Contains(5)){
                        List<int> temp = new List<int>();
                        temp.Add(1);
                        temp.Add(5);
                        foreach(GameObject tempGameObject in allWallTypes){
                            if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                int columnAdjustment = (i - columnHeight / 2) * 4;
                                int rowAdjustment = (j - rowHeight / 2) * 4;
                                Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                break;
                            }
                        }
                    }
                    else if(roomDirections.Contains(1) && roomDirections.Contains(7)){
                        if(roomDirections.Contains(4)){
                            List<int> temp = new List<int>();
                            temp.Add(1);
                            temp.Add(4);
                            temp.Add(7);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else{
                            List<int> temp = new List<int>();
                            temp.Add(1);
                            temp.Add(7);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                    }
                    else if(roomDirections.Contains(3) && roomDirections.Contains(5)){
                        if(roomDirections.Contains(8)){
                            List<int> temp = new List<int>();
                            temp.Add(3);
                            temp.Add(5);
                            temp.Add(8);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else{
                            List<int> temp = new List<int>();
                            temp.Add(3);
                            temp.Add(5);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                    }
                    else if(roomDirections.Contains(3) && roomDirections.Contains(7)){
                        List<int> temp = new List<int>();
                        temp.Add(3);
                        temp.Add(7);
                        foreach(GameObject tempGameObject in allWallTypes){
                            if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                int columnAdjustment = (i - columnHeight / 2) * 4;
                                int rowAdjustment = (j - rowHeight / 2) * 4;
                                Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                break;
                            }
                        }
                    }
                    else if(roomDirections.Contains(5) && roomDirections.Contains(7)){
                        if(roomDirections.Contains(2)){
                            List<int> temp = new List<int>();
                            temp.Add(2);
                            temp.Add(5);
                            temp.Add(7);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else{
                            List<int> temp = new List<int>();
                            temp.Add(5);
                            temp.Add(7);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                    }
                    else if(roomDirections.Contains(1)){
                        if(roomDirections.Contains(4) && roomDirections.Contains(6)){
                            List<int> temp = new List<int>();
                            temp.Add(1);
                            temp.Add(4);
                            temp.Add(6);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else if(roomDirections.Contains(4)){
                            List<int> temp = new List<int>();
                            temp.Add(1);
                            temp.Add(4);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else if(roomDirections.Contains(6)){
                            List<int> temp = new List<int>();
                            temp.Add(1);
                            temp.Add(6);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else{
                            List<int> temp = new List<int>();
                            temp.Add(1);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                    }
                    else if(roomDirections.Contains(3)){
                        if(roomDirections.Contains(6) && roomDirections.Contains(8)){
                            List<int> temp = new List<int>();
                            temp.Add(3);
                            temp.Add(6);
                            temp.Add(8);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else if(roomDirections.Contains(6)){
                            List<int> temp = new List<int>();
                            temp.Add(3);
                            temp.Add(6);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else if(roomDirections.Contains(8)){
                            List<int> temp = new List<int>();
                            temp.Add(3);
                            temp.Add(8);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else{
                            List<int> temp = new List<int>();
                            temp.Add(3);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                    }
                    else if(roomDirections.Contains(5)){
                        if(roomDirections.Contains(2) && roomDirections.Contains(8)){
                            List<int> temp = new List<int>();
                            temp.Add(2);
                            temp.Add(5);
                            temp.Add(8);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else if(roomDirections.Contains(2)){
                            List<int> temp = new List<int>();
                            temp.Add(2);
                            temp.Add(5);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else if(roomDirections.Contains(8)){
                            List<int> temp = new List<int>();
                            temp.Add(5);
                            temp.Add(8);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else{
                            List<int> temp = new List<int>();
                            temp.Add(5);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    GameObject tempWallPiece = Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    if(newPosition.y > highestY.y){
                                        highestY = newPosition;
                                        highestWall = tempWallPiece;
                                    }
                                    int windowRand = Random.Range(1, 101);
                                    if(windowRand <= windowChance){
                                        tempWallPiece.transform.Find("BlueWalls2_16").gameObject.GetComponent<SpriteRenderer>().sprite = window;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else if(roomDirections.Contains(7)){
                        if(roomDirections.Contains(2) && roomDirections.Contains(4)){
                            List<int> temp = new List<int>();
                            temp.Add(2);
                            temp.Add(4);
                            temp.Add(7);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else if(roomDirections.Contains(2)){
                            List<int> temp = new List<int>();
                            temp.Add(2);
                            temp.Add(7);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else if(roomDirections.Contains(4)){
                            List<int> temp = new List<int>();
                            temp.Add(4);
                            temp.Add(7);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else{
                            List<int> temp = new List<int>();
                            temp.Add(7);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                    }
                    else if(roomDirections.Contains(2)){
                        if(roomDirections.Contains(4) && roomDirections.Contains(6) && roomDirections.Contains(8)){
                            List<int> temp = new List<int>();
                            temp.Add(2);
                            temp.Add(4);
                            temp.Add(6);
                            temp.Add(8);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else if(roomDirections.Contains(4) && roomDirections.Contains(6)){
                            List<int> temp = new List<int>();
                            temp.Add(2);
                            temp.Add(4);
                            temp.Add(6);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else if(roomDirections.Contains(4) && roomDirections.Contains(8)){
                            List<int> temp = new List<int>();
                            temp.Add(2);
                            temp.Add(4);
                            temp.Add(8);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else if(roomDirections.Contains(6) && roomDirections.Contains(8)){
                            List<int> temp = new List<int>();
                            temp.Add(2);
                            temp.Add(6);
                            temp.Add(8);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else if(roomDirections.Contains(4)){
                            List<int> temp = new List<int>();
                            temp.Add(2);
                            temp.Add(4);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else if(roomDirections.Contains(6)){
                            List<int> temp = new List<int>();
                            temp.Add(2);
                            temp.Add(6);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else if(roomDirections.Contains(8)){
                            List<int> temp = new List<int>();
                            temp.Add(2);
                            temp.Add(8);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else{
                            List<int> temp = new List<int>();
                            temp.Add(2);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                    }
                    else if(roomDirections.Contains(4)){
                        if(roomDirections.Contains(6) && roomDirections.Contains(8)){
                            List<int> temp = new List<int>();
                            temp.Add(4);
                            temp.Add(6);
                            temp.Add(8);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else if(roomDirections.Contains(6)){
                            List<int> temp = new List<int>();
                            temp.Add(4);
                            temp.Add(6);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else if(roomDirections.Contains(8)){
                            List<int> temp = new List<int>();
                            temp.Add(4);
                            temp.Add(8);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else{
                            List<int> temp = new List<int>();
                            temp.Add(4);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                    }
                    else if(roomDirections.Contains(6)){
                        if(roomDirections.Contains(8)){
                            List<int> temp = new List<int>();
                            temp.Add(6);
                            temp.Add(8);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                        else{
                            List<int> temp = new List<int>();
                            temp.Add(6);
                            foreach(GameObject tempGameObject in allWallTypes){
                                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                    int columnAdjustment = (i - columnHeight / 2) * 4;
                                    int rowAdjustment = (j - rowHeight / 2) * 4;
                                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                    break;
                                }
                            }
                        }
                    }
                    else{
                        List<int> temp = new List<int>();
                        temp.Add(8);
                        foreach(GameObject tempGameObject in allWallTypes){
                            if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                                int columnAdjustment = (i - columnHeight / 2) * 4;
                                int rowAdjustment = (j - rowHeight / 2) * 4;
                                Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                                Instantiate(tempGameObject, newPosition, Quaternion.identity);
                                break;
                            }
                        }
                    }
                }
            }
        }
        if(rooms[1,1] != null){
            List<int> temp = new List<int>();
            temp.Add(2);
            foreach(GameObject tempGameObject in allWallTypes){
                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                    int columnAdjustment = (0 - columnHeight / 2) * 4;
                    int rowAdjustment = (0 - rowHeight / 2) * 4;
                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                    break;
                }
            }
        }
        if(rooms[columnHeight - 2, 1] != null){
            List<int> temp = new List<int>();
            temp.Add(8);
            foreach(GameObject tempGameObject in allWallTypes){
                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                    int columnAdjustment = (columnHeight - 1 - columnHeight / 2) * 4;
                    int rowAdjustment = (0 - rowHeight / 2) * 4;
                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                    break;
                }
            }
        }
        if(rooms[columnHeight - 2, rowHeight - 2] != null){
            List<int> temp = new List<int>();
            temp.Add(6);
            foreach(GameObject tempGameObject in allWallTypes){
                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                    int columnAdjustment = (columnHeight - 1 - columnHeight / 2) * 4;
                    int rowAdjustment = (rowHeight - 1 - rowHeight / 2) * 4;
                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                    break;
                }
            }
        }
        if(rooms[1, rowHeight - 2] != null){
            List<int> temp = new List<int>();
            temp.Add(4);
            foreach(GameObject tempGameObject in allWallTypes){
                if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                    int columnAdjustment = (0 - columnHeight / 2) * 4;
                    int rowAdjustment = (rowHeight - 1 - rowHeight / 2) * 4;
                    Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                    Instantiate(tempGameObject, newPosition, Quaternion.identity);
                    break;
                }
            }
        }
        for(int i = 1; i < columnHeight - 1; i++){
            if(rooms[i,1] != null){
                List<int> temp = new List<int>();
                temp.Add(1);
                foreach(GameObject tempGameObject in allWallTypes){
                    if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                        int columnAdjustment = (i - columnHeight / 2) * 4;
                        int rowAdjustment = (0 - rowHeight / 2) * 4;
                        Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                        Instantiate(tempGameObject, newPosition, Quaternion.identity);
                        break;
                    }
                }
            }
            else{
                if(rooms[i - 1, 1] != null && rooms[i + 1, 1] != null){
                    List<int> temp = new List<int>();
                    temp.Add(2);
                    temp.Add(8);
                    foreach(GameObject tempGameObject in allWallTypes){
                        if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                            int columnAdjustment = (i - columnHeight / 2) * 4;
                            int rowAdjustment = (0 - rowHeight / 2) * 4;
                            Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                            Instantiate(tempGameObject, newPosition, Quaternion.identity);
                            break;
                        }
                    }
                }
                else if(rooms[i - 1, 1] != null){
                    List<int> temp = new List<int>();
                    temp.Add(8);
                    foreach(GameObject tempGameObject in allWallTypes){
                        if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                            int columnAdjustment = (i - columnHeight / 2) * 4;
                            int rowAdjustment = (0 - rowHeight / 2) * 4;
                            Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                            Instantiate(tempGameObject, newPosition, Quaternion.identity);
                            break;
                        }
                    }
                }
                else if(rooms[i + 1, 1] != null){
                    List<int> temp = new List<int>();
                    temp.Add(2);
                    foreach(GameObject tempGameObject in allWallTypes){
                        if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                            int columnAdjustment = (i - columnHeight / 2) * 4;
                            int rowAdjustment = (0 - rowHeight / 2) * 4;
                            Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                            Instantiate(tempGameObject, newPosition, Quaternion.identity);
                            break;
                        }
                    }
                }
            }
            if(rooms[i,rowHeight - 2] != null){
                List<int> temp = new List<int>();
                temp.Add(5);
                foreach(GameObject tempGameObject in allWallTypes){
                    if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                        int columnAdjustment = (i - columnHeight / 2) * 4;
                        int rowAdjustment = (rowHeight - 1 - rowHeight / 2) * 4;
                        Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                        GameObject tempWallPiece = Instantiate(tempGameObject, newPosition, Quaternion.identity);
                        if(newPosition.y > highestY.y){
                            highestY = newPosition;
                            highestWall = tempWallPiece;
                        }
                        int windowRand = Random.Range(1, 101);
                        if(windowRand <= windowChance){
                            tempWallPiece.transform.Find("BlueWalls2_16").gameObject.GetComponent<SpriteRenderer>().sprite = window;
                        }
                        break;
                    }
                }
            }
            else{
                if(rooms[i + 1, rowHeight - 2] != null && rooms[i - 1, rowHeight - 2] != null){
                    List<int> temp = new List<int>();
                    temp.Add(4);
                    temp.Add(6);
                    foreach(GameObject tempGameObject in allWallTypes){
                        if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                            int columnAdjustment = (i - columnHeight / 2) * 4;
                            int rowAdjustment = (rowHeight - 1 - rowHeight / 2) * 4;
                            Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                            Instantiate(tempGameObject, newPosition, Quaternion.identity);
                            break;
                        }
                    }
                }
                else if(rooms[i + 1, rowHeight - 2] != null){
                    List<int> temp = new List<int>();
                    temp.Add(4);
                    foreach(GameObject tempGameObject in allWallTypes){
                        if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                            int columnAdjustment = (i - columnHeight / 2) * 4;
                            int rowAdjustment = (rowHeight - 1 - rowHeight / 2) * 4;
                            Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                            Instantiate(tempGameObject, newPosition, Quaternion.identity);
                            break;
                        }
                    }
                }
                else if(rooms[i - 1, rowHeight - 2] != null){
                    List<int> temp = new List<int>();
                    temp.Add(6);
                    foreach(GameObject tempGameObject in allWallTypes){
                        if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                            int columnAdjustment = (i - columnHeight / 2) * 4;
                            int rowAdjustment = (rowHeight - 1 - rowHeight / 2) * 4;
                            Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                            Instantiate(tempGameObject, newPosition, Quaternion.identity);
                            break;
                        }
                    }
                }
            }
        }
        for(int j = 1; j < rowHeight - 1; j++){
            if(rooms[1,j] != null){
                List<int> temp = new List<int>();
                temp.Add(3);
                foreach(GameObject tempGameObject in allWallTypes){
                    if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                        int columnAdjustment = (0 - columnHeight / 2) * 4;
                        int rowAdjustment = (j - rowHeight / 2) * 4;
                        Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                        Instantiate(tempGameObject, newPosition, Quaternion.identity);
                        break;
                    }
                }
            }
            else{
                if(rooms[1, j - 1] != null && rooms[1, j + 1] != null){
                    List<int> temp = new List<int>();
                    temp.Add(2);
                    temp.Add(4);
                    foreach(GameObject tempGameObject in allWallTypes){
                        if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                            int columnAdjustment = (0 - columnHeight / 2) * 4;
                            int rowAdjustment = (j - rowHeight / 2) * 4;
                            Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                            Instantiate(tempGameObject, newPosition, Quaternion.identity);
                            break;
                        }
                    }
                }
                else if(rooms[1, j - 1] != null){
                    List<int> temp = new List<int>();
                    temp.Add(4);
                    foreach(GameObject tempGameObject in allWallTypes){
                        if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                            int columnAdjustment = (0 - columnHeight / 2) * 4;
                            int rowAdjustment = (j - rowHeight / 2) * 4;
                            Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                            Instantiate(tempGameObject, newPosition, Quaternion.identity);
                            break;
                        }
                    }
                }
                else if(rooms[1, j + 1] != null){
                    List<int> temp = new List<int>();
                    temp.Add(2);
                    foreach(GameObject tempGameObject in allWallTypes){
                        if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                            int columnAdjustment = (0 - columnHeight / 2) * 4;
                            int rowAdjustment = (j - rowHeight / 2) * 4;
                            Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                            Instantiate(tempGameObject, newPosition, Quaternion.identity);
                            break;
                        }
                    }
                }
            }
            if(rooms[columnHeight - 2, j] != null){
                List<int> temp = new List<int>();
                temp.Add(7);
                foreach(GameObject tempGameObject in allWallTypes){
                    if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                        int columnAdjustment = (columnHeight - 1 - columnHeight / 2) * 4;
                        int rowAdjustment = (j - rowHeight / 2) * 4;
                        Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                        Instantiate(tempGameObject, newPosition, Quaternion.identity);
                        break;
                    }
                }
            }
            else{
                if(rooms[columnHeight - 2, j - 1] != null && rooms[columnHeight - 2, j + 1] != null){
                    List<int> temp = new List<int>();
                    temp.Add(6);
                    temp.Add(8);
                    foreach(GameObject tempGameObject in allWallTypes){
                        if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                            int columnAdjustment = (columnHeight - 1 - columnHeight / 2) * 4;
                            int rowAdjustment = (j - rowHeight / 2) * 4;
                            Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                            Instantiate(tempGameObject, newPosition, Quaternion.identity);
                            break;
                        }
                    }
                }
                else if(rooms[columnHeight - 2, j - 1] != null){
                    List<int> temp = new List<int>();
                    temp.Add(6);
                    foreach(GameObject tempGameObject in allWallTypes){
                        if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                            int columnAdjustment = (columnHeight - 1 - columnHeight / 2) * 4;
                            int rowAdjustment = (j - rowHeight / 2) * 4;
                            Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                            Instantiate(tempGameObject, newPosition, Quaternion.identity);
                            break;
                        }
                    }
                }
                else if(rooms[columnHeight - 2, j + 1] != null){
                    List<int> temp = new List<int>();
                    temp.Add(8);
                    foreach(GameObject tempGameObject in allWallTypes){
                        if(AreListsEqual(temp, tempGameObject.GetComponent<WallType>().adjacentRooms)){
                            int columnAdjustment = (columnHeight - 1 - columnHeight / 2) * 4;
                            int rowAdjustment = (j - rowHeight / 2) * 4;
                            Vector3 newPosition = new Vector3(transform.position.x + columnAdjustment, transform.position.y + rowAdjustment, 0);
                            Instantiate(tempGameObject, newPosition, Quaternion.identity);
                            break;
                        }
                    }
                }
            }
        }
    }

    private bool AreListsEqual(List<int> list1, List<int> list2){
        bool areListsEqual = true;

        if (list1.Count != list2.Count)
            return false;

        for (int i = 0; i < list1.Count; i++)
        {
            if (list2[i] != list1[i])
            {
                areListsEqual = false;
            }
        }

        return areListsEqual;
    }
}
