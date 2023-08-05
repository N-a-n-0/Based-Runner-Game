using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class FileDataHandler
{

    private string dataDirPath = "";

    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        Debug.Log("DATA SHOULD BE LOADING");
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        Debug.Log(fullPath);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //deserialize the data from Json back into the C# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
               Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        Debug.Log(loadedData);
        return loadedData;
    }

    public void Save(GameData data)
    {
        Debug.Log("DATA SHOULD BE SAVING");
        string fullPath = Path.Combine(dataDirPath, dataFileName);
      
        Debug.Log(fullPath);
        try
        {
            //create the directory the file will be written to if it doesn't alreayd have it
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //Serialize the C# game data object into json
            string dataToStore = JsonUtility.ToJson(data, true);

            //write the serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
           Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
            
        }
    }

}
