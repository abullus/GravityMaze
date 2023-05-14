using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public GameObject levelItem;

    public void Start()
    {
        LevelManager levelManager = LevelManager.Instance;
        List<LevelData> levels = levelManager.SaveData.LevelDataList;
        int levelCount = 1;
        bool firstPassed = false;
        foreach (LevelData level in levels)
        {
            GameObject newLevelItem  = Instantiate(levelItem, transform);
            LevelSelectItem item = newLevelItem.GetComponent<LevelSelectItem>();
            item.SetData("Level " + levelCount,level.name, level.passed);
            if (firstPassed == false && level.passed == false)
            {
                item.SetData("Level " + levelCount, level.name, true);
                item.Blue();
                firstPassed = true;
            }
            levelCount++;
        }
    }
}
