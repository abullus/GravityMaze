using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public LevelSaveData SaveData;
    
    private bool playerActive = true;

    private readonly List<string> LevelOrder = new List<string>
    {
        "Level0", "Level1", "Level2", "Level3", "Level4", "Level5", "Level6", "Level7", "Level8", "Level9",
        "Level10", "Level11", "Level12", "LevelBlueBlocks", "Level13", "Level14", "Level15", "Level16", "Level17", "Level18", "Level19",
        "Level20", "Level21", "Level22", "Level23", "Level24", "Level25", "Level26", "Level27", "Level28", "Level29",
        "Level30", "Level31", "Level32", "Level33", "Level34", "Level35", "Level36", "Level37", "Level38", "Level39", 
        "Level40", "Level41", "Level42", "Level43", "Level44", "Level45", "Level46", "Level47", "Level48", 
    };

    public void Awake()
    {
        LoadGame();
    }
    
    public void StartGame()
    {
        LevelData nextUncompleted = SaveData.LevelDataList.Find(item => item.passed == false);
        LoadLevel(nextUncompleted == null ? LevelOrder[0] : nextUncompleted.name);
    }

    public void MainMenu()
    {
        LoadLevel("MainMenu");
    }

    public void LevelSelector()
    {
        LoadLevel("LevelSelector");
    }

    public void NextLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        int index = LevelOrder.FindIndex(item => item == currentSceneName);
        if (index > -1)
        {
            if (index + 1 == LevelOrder.Count)
            {
                PanelManager.Instance.GameWon();
                return;
            }
            LoadLevel(LevelOrder[index + 1]);
            return;
        }
        Debug.Log("Could not find current scene: "+ currentSceneName+ " in database");
    }

    public void RestartLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        LoadLevel(currentSceneName);
    }
    
    public void LevelLost()
    {
        if (!playerActive) return;
        LevelEnded();
        PanelManager.Instance.LevelLost();
    }

    public void LevelWon()
    {
        if (!playerActive) return;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SaveData.LevelDataList.Find(item => item.name == currentSceneName).passed = true;
        LevelEnded();
        PanelManager.Instance.LevelWon();
    }
    
    public void LoadLevel(string levelName)
    {
        PanelManager.Instance.ChangeSceneTransitionStart();
        StartCoroutine(Wait(0.5f, levelName));
    }
    
    IEnumerator Wait(float duration, string levelName)
    {
        yield return new WaitForSeconds(duration);
        AsyncOperation load = SceneManager.LoadSceneAsync(levelName);
        load.completed += (asyncOperation) =>
        {
            PanelManager.Instance.SceneLoaded(LevelOrder.Any(item => item == levelName), levelName);
            playerActive = true;
        };
    }

    // Potentially testing only
    public void DeleteSaveData()
    {
        try
        {
            File.Delete(Application.persistentDataPath + "/gamesave.save");
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        finally
        {
            SaveData = new LevelSaveData(LevelOrder);
            Debug.Log("DataSaved");
            MainMenu();
        }
    }
    
    // Testing only
    public void UnlockAllLevels()
    {
        DeleteSaveData();
        foreach (var levelData in SaveData.LevelDataList)
        {
            levelData.passed = true;
        }
    }
    
    private void LevelEnded()
    {
        playerActive = false;
        Physics2D.gravity = new Vector2(0,0);
        GravityManager.Instance.gravityStrength = 0;
    }

    private void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, SaveData);
        file.Close();
    }

    private void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            Debug.Log("Save data found");
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
                SaveData = (LevelSaveData) bf.Deserialize(file);
                SaveData = FixMissingData(SaveData);
                file.Close();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                File.Delete(Application.persistentDataPath + "/gamesave.save");
                SaveData = new LevelSaveData(LevelOrder);
                Debug.Log("File has been deleted - new blank object created");
            }
        }
        else
        {
            Debug.Log("No save data found - new blank object created");
            SaveData = new LevelSaveData(LevelOrder);
        }
    }

    private LevelSaveData FixMissingData(LevelSaveData saveData)
    {
        bool outOfOrder = false;
        bool outOfRange = false;
        int levelNumber = LevelOrder.Count;
        
        for (int index = 0; index < levelNumber; index++)
        {
            if (outOfRange || saveData.LevelDataList.Count - 1 < index)
            {
                saveData.LevelDataList.Add(new LevelData(LevelOrder[index]));
                outOfRange = true;
            }
            else if (outOfOrder || LevelOrder[index] != saveData.LevelDataList[index].name)
            {
                saveData.LevelDataList[index] = new LevelData(LevelOrder[index]);
                outOfOrder = true;
            }
        }
        return saveData;
    }

    void OnApplicationPause()
    {
        Debug.Log("Data Saved");
        SaveGame();
    }
    
    void OnApplicationQuit()
    {
        Debug.Log("Data Saved");
        SaveGame();
    }
}

[System.Serializable]
public class LevelSaveData
{
    public List<LevelData> LevelDataList { get; set; }

    public LevelSaveData(List<string> LevelOrder)
    {
        var data = new List<LevelData>();
        foreach (var level in LevelOrder)
        {
            data.Add(new LevelData(level));
        }

        LevelDataList = data;
    }
}

[System.Serializable]
public class LevelData
{
    public string name { get; set; }
    public bool passed { get; set; }

    public LevelData(string levelName, bool levelPassed = false)
    {
        name = levelName;
        passed = levelPassed;
    }
}
