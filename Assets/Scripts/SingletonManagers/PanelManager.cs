using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of PanelManager!");
        }
        Instance = this;
    }
    
    public Canvas canvas;
    public GameObject levelLostPanel;
    public GameObject levelWonPanel;
    public GameObject gameWonPanel;
    public GameObject overlayPanel;
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject transitionPanel;
    public GameObject confirmationPanel;

    private static GameObject thisPausePanel;
    private static GameObject thisOverlayPanel;
    private static Canvas thisCanvas;
    private static GameObject thisSettingsPanel;
    private static GameObject thisLevelWonPanel;
    private static GameObject thisTransitionPanel;

    public void SceneLoaded(bool level, string levelName)
    {
        Transition();
        if (level)
        {
            Overlay();
        }
    }

    private void Transition()
    {
        thisTransitionPanel = InstantiatePanel(transitionPanel);
    }

    public void ChangeSceneTransitionStart()
    {
        if (thisTransitionPanel == null) return;
        thisTransitionPanel.transform.SetAsLastSibling();
        thisTransitionPanel.GetComponentInChildren<Animator>().SetTrigger("Start");
    }

    public void LevelLost()
    {
        InstantiatePanel(levelLostPanel);
    }

    public void LevelWon()
    {
        thisLevelWonPanel = InstantiatePanel(levelWonPanel);
    }

    public void GameWon()
    {
        Destroy(thisLevelWonPanel);
        InstantiatePanel(gameWonPanel);
    }

    private void Overlay()
    {
        thisOverlayPanel = InstantiatePanel(overlayPanel);
        thisPausePanel = InstantiatePanel(pausePanel);
        thisPausePanel.SetActive(false);
    }

    public void Pause()
    {
        thisOverlayPanel.SetActive(false);
        thisPausePanel.SetActive(true);
        thisPausePanel.transform.SetAsLastSibling();
    }
    
    public void Resume_ShowOverlay()
    {
        thisOverlayPanel.SetActive(true);
    }

    public void Resume_HidePause()
    {
        thisPausePanel.SetActive(false);
    }

    public void Settings()
    {
        if (thisSettingsPanel == null)
        {
            thisSettingsPanel = InstantiatePanel(settingsPanel);
        }
    }

    public void CloseSettings()
    {
        if (thisSettingsPanel == null)
        {
            return;
        }
        thisSettingsPanel.GetComponentInChildren<Animator>().SetTrigger("Start");
        Destroy(thisSettingsPanel, 1);
        thisSettingsPanel = null;
    }

    public void ConfirmationPanel()
    {
        InstantiatePanel(confirmationPanel);
    }

    private GameObject InstantiatePanel(GameObject panel)
    {
        thisCanvas = FindObjectOfType<Canvas>();
        if (thisCanvas == null)
        {
            thisCanvas = Instantiate(canvas);
        }

        var canvasInstanceTransform = thisCanvas.transform;
        var panelInstance = Instantiate(
            panel, 
            canvasInstanceTransform.position, 
            Quaternion.identity, 
            canvasInstanceTransform);
        panelInstance.SetActive(true);
        return (panelInstance);
    }
}
