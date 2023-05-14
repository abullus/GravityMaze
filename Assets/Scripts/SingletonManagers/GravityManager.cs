using System;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public float gravityStrength = 9.81f;
    public float sensitivityFactor = 3f;
    public float GravityAngle;

    public static GravityManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of GravityManager!");
        }
        Instance = this;
    }
    
    void Start()
    {
        CheckSettings();
        
        GravityAngle = Mathf.PI;
        SetGravity(VectorToAngle());
    }

    public void CheckSettings()
    {
        sensitivityFactor = GetChangeAmount(SettingsManager.Instance.InputSensitivity);
    }

    public void ChangeGravityAngle(bool clockwise)
    {
        if (clockwise)
        {
            GravityAngle += sensitivityFactor * Time.deltaTime;
        }
        else
        {
            GravityAngle -= sensitivityFactor * Time.deltaTime;
        }
        SetGravity(VectorToAngle());
    }

    public void ChangeGravityVector(Vector2 vector)
    {
        vector *= sensitivityFactor;
        SetGravity(vector);
    }

    private void SetGravity(Vector2 vector)
    {
        vector *= gravityStrength;
        Physics2D.gravity = vector;
    }

    private Vector2 VectorToAngle()
    {
        var x = Mathf.Sin(GravityAngle);
        var y = Mathf.Cos(GravityAngle);
        return new Vector2(x, y);
    }

    private float GetChangeAmount(SettingsManager.Sensitivity sensitivity)
    {
        switch (sensitivity)
        {
            case SettingsManager.Sensitivity.VeryLow:
                return 1f;
            case SettingsManager.Sensitivity.Low:
                return 2f;
            case SettingsManager.Sensitivity.Medium:
                return 3f;
            case SettingsManager.Sensitivity.High:
                return 4f;
            case SettingsManager.Sensitivity.VeryHigh:
                return 5f;
            default:
                return 3f;
        }
    }
}
