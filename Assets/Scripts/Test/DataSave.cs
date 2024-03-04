using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSave : MonoBehaviour
{
    public string path = Application.dataPath;
    public Player player;

    [ContextMenu("save")]
    public void Save()
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string filePath = Path.Combine(path, "Test.txt");
        string json = JsonUtility.ToJson(player, true);
        File.WriteAllText(filePath, json);
    }

    [ContextMenu("load")]
    public void Load()
    {
        string filePath = Path.Combine(path, "Test.txt");
        string read = File.ReadAllText(filePath);
        player = JsonUtility.FromJson<Player>(read);
        Debug.Log(read);
    }
}

[Serializable]
public class Player
{
    public int level;
    public int gold;
    public int exp;
}
