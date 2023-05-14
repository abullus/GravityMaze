using System;
using UnityEngine;
using Object = System.Object;

public class SettingsManager : Singleton<SettingsManager>
{
    private bool _advancedEffectsOn;

    public bool AdvancedEffectsOn
    {
        get => _advancedEffectsOn;
        set
        {
            _advancedEffectsOn = value;
            SaveChanges(_advancedEffectsOn ? 1 : 0, "AdvancedEffectsOn");
            
            FindObjectOfType<FinishScript>()?.CheckSettings();
            
            foreach (var holeScript in FindObjectsOfType<DeathHoleScript>())
            {
                holeScript.CheckSettings();
            }

            foreach (var wallScript in FindObjectsOfType<WallScript>())
            {
                wallScript.CheckSettings();
            }

            foreach (var playerChangeScript in FindObjectsOfType<PlayerChangeEffects>())
            {
                playerChangeScript.CheckSettings();
            }
        }
    }

    private bool _advancedLightingOn;

    public bool AdvancedLightingOn
    {
        get => _advancedLightingOn;
        set
        {
            _advancedLightingOn = value;
            SaveChanges(_advancedLightingOn ? 1 : 0, "AdvancedLightingOn");
            
            FindObjectOfType<PlayerScript>()?.CheckSettings();
            
            foreach (var playerChangeScript in FindObjectsOfType<PlayerChangeEffects>())
            {
                playerChangeScript.CheckSettings();
            }
        }
    }

    private bool _soundOn;

    public bool SoundOn
    {
        get => _soundOn;
        set
        {
            _soundOn = value;
            SaveChanges(_soundOn ? 1 : 0, "SoundOn");
            
            FindObjectOfType<SoundEffectsManager>().CheckSettings();
        }
    }

    private Sensitivity _sensitivity;

    public Sensitivity InputSensitivity
    {
        get => _sensitivity;
        set
        {
            _sensitivity = value;
            SaveChanges((int) _sensitivity, "Sensitivity");
            
            FindObjectOfType<GravityManager>().CheckSettings();
        }
    }
    
    private Method _inputMethod;

    public Method InputMethod
    {
        get => _inputMethod;
        set
        {
            _inputMethod = value;
            SaveChanges((int) _inputMethod, "InputMethod");
            
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");

            if (value == Method.Tilt)
            {
                camera.GetComponentInChildren<RotateWithGravity>(true).enabled = false;
                player.GetComponentInChildren<GetArrowInput>(true).enabled = false;
                player.GetComponentInChildren<GetTiltInput>(true).enabled = true;
                player.GetComponentInChildren<GetTouchInput>(true).enabled = false;
            }
            else
            {
                camera.GetComponentInChildren<RotateWithGravity>(true).enabled = true;
                player.GetComponentInChildren<GetArrowInput>(true).enabled = true;
                player.GetComponentInChildren<GetTiltInput>(true).enabled = false;
                player.GetComponentInChildren<GetTouchInput>(true).enabled = true;
            }
            
            
        }
    }

    private void Awake()
    {
        Debug.Log("Awake");
        SoundOn = GetCurrentSave("SoundOn") == 1;
        AdvancedEffectsOn = GetCurrentSave("AdvancedEffectsOn") == 1;
        AdvancedLightingOn = GetCurrentSave("AdvancedLightingOn") == 1;
        InputSensitivity = (Sensitivity) GetCurrentSave("Sensitivity");
        InputMethod = (Method) GetCurrentSave("InputMethod");
    }

    private void SaveChanges(int value, string name)
    {
        PlayerPrefs.SetInt(name, value);
        PlayerPrefs.Save();
    }

    public int GetCurrentSave(string name)
    {
        if (PlayerPrefs.HasKey(name))
        {
            return PlayerPrefs.GetInt(name);
        }

        int defaultValue = GetDefaultIntegerValue(name);
        PlayerPrefs.SetInt(name, defaultValue);
        PlayerPrefs.Save();
        return defaultValue;
    }

    private int GetDefaultIntegerValue(string name)
    {
        switch (name)
        {
            case "Sensitivity":
                return 2;
            
            case "InputMethod":
                return 0;
            
            default:
                return 1;
        }
    }

    public void ReturnToDefaults()
    {
        AdvancedEffectsOn = GetDefaultIntegerValue("AdvancedEffectsOn") == 1;
        AdvancedLightingOn = GetDefaultIntegerValue("AdvancedLightingOn") == 1;
        SoundOn = GetDefaultIntegerValue("SoundOn") == 1;
        InputSensitivity = (Sensitivity) GetDefaultIntegerValue("Sensitivity");
        InputMethod = (Method) GetDefaultIntegerValue("InputMethod");
    }

    public enum Sensitivity
    {
        VeryLow,
        Low,
        Medium,
        High,
        VeryHigh,
    }

    public enum Method
    {
        Touch,
        Tilt,
    }
}
