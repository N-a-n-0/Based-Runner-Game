using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject[] eventTilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 30;

    public int numberOfTiles = 5;

    public bool gameStarted = false;

    public bool endOfLevelReached = false;

    public int totalTilesSpawned = 0;

    [Range(0, 1000)]
    public int tileCount;

    private List<GameObject> activeTiles = new List<GameObject>();



    public Transform playerTransform;

    void Start()
    {

        for (int i = 0; i < numberOfTiles; i++)
        {
            if (gameStarted == false)
            {
                //  print(gameStarted);


                gameStarted = true;
                CustomSpawnTile(0); //Spawning the starter tile right here :) //sus



            }
            else
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));



            }

        }

    }


    void Update()
    {
        if (totalTilesSpawned == tileCount && SceneManager.GetActiveScene().name != "Main")
        {
            endOfLevelReached = true;
        }

        if (endOfLevelReached == false)
        {

            if (playerTransform.position.z - 35 > zSpawn - (numberOfTiles * tileLength))
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));

                DeleteTile();
            }

        }
        else if (totalTilesSpawned == tileCount && endOfLevelReached == true)
        {
            print("BRUH");
            // endOfLevelReached = false;
            CustomSpawnTile(1);
        }


    }

    public void SpawnTile(int tileIndex)
    {

        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        totalTilesSpawned++;
        activeTiles.Add(go);
        zSpawn += tileLength;
    }
    public void CustomSpawnTile(int tileIndex)
    {

        GameObject go = Instantiate(eventTilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        totalTilesSpawned++;
        activeTiles.Add(go);
        zSpawn += tileLength;
    }
    private void DeleteTile()
    {

        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);


    }



}
