using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject tilePrefab;
    [SerializeField]
    GameObject currentTile;
    [SerializeField]
    GameObject pickupPrefab;

    GameObject player;

    Stack<GameObject> reusedTiles = new Stack<GameObject>();

    // Singleton Pattern
    #region Singleton
    private static TileSpawner instance;
    public static TileSpawner Instance { 
        get {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<TileSpawner>();
            }
            return instance;
        } 
    }
    #endregion

    public Stack<GameObject> ReusedTiles { get => reusedTiles; set => reusedTiles = value; }


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player") as GameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 40; i++)
        {
            SpawnTiles();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function for Create and Spawn Tiles and add them to the ReusedTiles Stack in order to reuse them in game
    public void SpawnTiles()
    {
        if (ReusedTiles.Count < 50 ) { 
        Transform pointTransform = currentTile.transform.GetChild(Random.Range(0, currentTile.transform.childCount));
        currentTile = Instantiate(tilePrefab, pointTransform.position, Quaternion.identity) as GameObject;
        ReusedTiles.Push(currentTile);
        }
        else
        {
            Transform pointTransform = currentTile.transform.GetChild(Random.Range(0, currentTile.transform.childCount));
            GameObject tmp = ReusedTiles.Pop();
            tmp.transform.position = pointTransform.position;
            currentTile = tmp;
        }

        if (Random.Range(0, 9) == 0)
        {
            Vector3 pos = new Vector3(currentTile.transform.position.x, player.transform.position.y, currentTile.transform.position.z);
            Instantiate(pickupPrefab, pos, pickupPrefab.transform.rotation);
        }

    }
}
