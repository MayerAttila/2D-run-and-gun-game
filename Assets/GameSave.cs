using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class GameSave 
{
    public static void SaveGame(SavePlayerDatas.player player) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Game.save";
        Debug.Log(path + "    save");
        FileStream fs = new FileStream(path, FileMode.Create);

        formatter.Serialize(fs, player);
        fs.Close();
    }

    public static SavePlayerDatas.player LoadSave() 
    {
        string path = Application.persistentDataPath + "/Game.save";
        Debug.Log(path + "    load");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            SavePlayerDatas.player readedData = formatter.Deserialize(fs) as SavePlayerDatas.player;
            
            fs.Close();
            
            return readedData;
        }
        else 
        {
            Debug.Log("no save file found");
            return null;
        }
    }

    public static void DeleteGameSave()
    {
        string path = Application.persistentDataPath + "/Game.save";
        if (File.Exists(path)) 
        {
            Debug.Log("exist");
            File.Delete(path);
        } 
    }
}
