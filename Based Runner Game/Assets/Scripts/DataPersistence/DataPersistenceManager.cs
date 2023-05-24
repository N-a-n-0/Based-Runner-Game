using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.IO;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]

    [SerializeField] private string fileName;


    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene");

        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }


    public void LoadGame()
    {

        this.gameData = dataHandler.Load();



        if (this.gameData == null)
        {
            Debug.Log("No data was found. INitializing data to deafults.");
            NewGame();
        }


        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        Debug.Log("Loaded death count = " + gameData.deathCount);
        Debug.Log("Loaded coin count = " + gameData.coins);
    }

    public void SaveGame()
    {
       foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData( gameData);
        }
        Debug.Log("Loaded death count = " + gameData.deathCount);
        Debug.Log("Loaded coin count = " + gameData.coins);


        dataHandler.Save(gameData);
    }


    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);

    }
}
