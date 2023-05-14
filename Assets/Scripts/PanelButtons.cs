using UnityEngine;

public class PanelButtons : MonoBehaviour
{
    public void StartGame()
    {
        LevelManager.Instance.StartGame();
    }

    public void MainMenu()
    {
        LevelManager.Instance.MainMenu();
    }

    public void LevelSelector()
    {
        LevelManager.Instance.LevelSelector();
    }

    public void NextLevel()
    {
        LevelManager.Instance.NextLevel();
    }

    public void RestartLevel()
    {
        LevelManager.Instance.RestartLevel();
    }
}
