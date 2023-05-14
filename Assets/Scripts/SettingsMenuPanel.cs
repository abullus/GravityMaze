using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuPanel : MonoBehaviour
{
    public Toggle AdvancedEffectsToggle;
    public Toggle AdvancedLightingToggle;
    public Toggle SoundOnToggle;
    public Dropdown SensitivityDropdown;
    public Dropdown InputMethodDropdown;

    public void Awake()
    {
        SetTogglesToManagerValues();
    }
 
    void Start() {
        SensitivityDropdown.onValueChanged.AddListener(delegate {
            SetSensitivity(SensitivityDropdown);
        });
        InputMethodDropdown.onValueChanged.AddListener(delegate {
            SetInputMethod(InputMethodDropdown);
        });
    }
    
    void OnDestroy() {
        SensitivityDropdown.onValueChanged.RemoveAllListeners();
        InputMethodDropdown.onValueChanged.RemoveAllListeners();
    }

    public void SetAdvancedEffects(bool value)
    {
        SettingsManager.Instance.AdvancedEffectsOn = value;
    }
    
    public void SetAdvancedLighting(bool value)
    {
        SettingsManager.Instance.AdvancedLightingOn = value;
    }

    public void SetSoundOn(bool value)
    {
        SettingsManager.Instance.SoundOn = value;
    }

    private void SetSensitivity(Dropdown dropdown)
    {
        SettingsManager.Instance.InputSensitivity = (SettingsManager.Sensitivity) dropdown.value;
    }
    
    private void SetInputMethod(Dropdown dropdown)
    {
        SettingsManager.Instance.InputMethod = (SettingsManager.Method) dropdown.value;
    }

    public void DeleteLevelSaveData()
    {
        PanelManager.Instance.ConfirmationPanel();
    }

    public void UnlockAllLevels()
    {
        LevelManager.Instance.UnlockAllLevels();
    }

    public void ReturnToDefaults()
    {
        SettingsManager.Instance.ReturnToDefaults();
        SetTogglesToManagerValues();
    }

    private void SetTogglesToManagerValues()
    {
        AdvancedLightingToggle.isOn = SettingsManager.Instance.GetCurrentSave("AdvancedLightingOn") == 1;
        AdvancedEffectsToggle.isOn = SettingsManager.Instance.GetCurrentSave("AdvancedEffectsOn") == 1 ;
        SoundOnToggle.isOn = SettingsManager.Instance.GetCurrentSave("SoundOn") == 1;
        SensitivityDropdown.value = SettingsManager.Instance.GetCurrentSave("Sensitivity");
        InputMethodDropdown.value = SettingsManager.Instance.GetCurrentSave("InputMethod");
    }
}
