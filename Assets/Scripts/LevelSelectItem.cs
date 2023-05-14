using UnityEngine;
using UnityEngine.UI;

public class LevelSelectItem : MonoBehaviour
{
    public Text displayNameText;
    public Button button;
    
    public void SetData(string displayName, string levelName, bool passed)
    {
        button.interactable = passed;
        displayNameText.text = displayName;
        button.onClick.AddListener(delegate {LoadScene(levelName); });
    }

    public void Blue()
    {
        ColorBlock buttonColors = button.colors;
        
        buttonColors.normalColor = new Color(0, 0.8705f, 1, 1);
        buttonColors.selectedColor = new Color(0, 0.6488f, 0.7452f, 1);
        buttonColors.pressedColor = new Color(0, 0.6488f, 0.7452f, 1);
        buttonColors.highlightedColor = new Color(0.4528f, 0.9291f, 1, 1);
        
        button.colors = buttonColors;

    }

    private static void LoadScene(string thisLevelName)
    {
        LevelManager.Instance.LoadLevel(thisLevelName);
    }
}
