using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int score;
    public int level;
}

public class JSONManager : MonoBehaviour
{
    private string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/Save/UserData.json";
      
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveData();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }
    }

    public void SaveData()
    {
        PlayerData player = new PlayerData();
        player.playerName = "Luca";
        player.score = 1000;
        player.level = 2;

        string json = JsonUtility.ToJson(player, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Data Saved" +json);

    }
    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerData loadedPlayer = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Loaded Data" + loadedPlayer.playerName + ", Score " + loadedPlayer.score + ", level" + loadedPlayer.level);

        }
        else
        {
            Debug.Log("No data found");
        }
    }
}
