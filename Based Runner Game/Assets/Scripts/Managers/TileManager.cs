using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 30;

    public int numberOfTiles = 5;

    public bool gameStarted = false;

    private List<GameObject> activeTiles = new List<GameObject>();



    public Transform playerTransform;
    
    void Start()
    {

       for(int i = 0; i < numberOfTiles; i++)
        {
            if(gameStarted == false)
            {
                print(gameStarted);

              
                gameStarted = true;
                SpawnTile(0);
                 
            }
            else
            {
                SpawnTile(Random.Range(1, tilePrefabs.Length));

            }
            
        }

    }

   
    void Update()
    {
        if(playerTransform.position.z -35 > zSpawn-(numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(1, tilePrefabs.Length));
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {

        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }
    private void DeleteTile()
    {
       
            Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);

       
    }
}