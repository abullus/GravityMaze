using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerScript : MonoBehaviour
{
    public Light2D AdvancedLight;
    public Light2D SimpleLight;
    private Rigidbody2D _rb2D;
    private SettingsManager settings;
    private CameraEffects cameraScript;

    private Light2D thisLight;

    private void Awake()
    {
        CheckSettings();
        
        _rb2D = gameObject.GetComponent<Rigidbody2D>();
        cameraScript = FindObjectOfType<CameraEffects>();
    }

    public void CheckSettings()
    {
        InitialiseLight(SettingsManager.Instance.AdvancedLightingOn);
    }
    
    public void TouchingHole (Vector3 holePosition)
    {
        LevelManager.Instance.LevelLost();
        cameraScript.ZoomIn();
        SlowDown();
    }

    public void TouchingFinish()
    {
        LevelManager.Instance.LevelWon();
        cameraScript.ZoomIn();
        SlowDown();
    }

    private void SlowDown()
    {
        _rb2D.drag = 10;
        _rb2D.angularDrag = 1;
    }

    private void InitialiseLight(bool advancedOn)
    {
        if (thisLight)
        {
            Destroy(thisLight);
        }
        thisLight = Instantiate(advancedOn ? AdvancedLight : SimpleLight, transform, false);
    }
}
