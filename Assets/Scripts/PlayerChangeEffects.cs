using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerChangeEffects : MonoBehaviour
{
    private ParticleSystem playerChangeParticles;
    private Light2D playerLight;
    private bool firstTouch;
    private ParticleSystem.EmitParams emitParams;
    private Color originalColor;
    public Color changeColor = new Color(0.1981f, 0.4862f, 0.3176f);

    private bool advancedEffectsOn;
    private bool advancedLightingOn;

    public void CheckSettings()
    {
        advancedEffectsOn = SettingsManager.Instance.AdvancedEffectsOn;
        advancedLightingOn = SettingsManager.Instance.AdvancedLightingOn;
    }

    private void Start()
    {
        firstTouch = true;
        
        emitParams = new ParticleSystem.EmitParams 
        {
            applyShapeToPosition = true,
            startColor = changeColor,
        };
        
        var player = GameObject.FindGameObjectWithTag("Player");
        var systems = player.GetComponentsInChildren<ParticleSystem>();
        playerChangeParticles = Array.Find(systems, x => x.CompareTag("AcceleratorEffect"));
        playerLight = player.GetComponentInChildren<Light2D>();
        originalColor = playerLight.color;
        
        CheckSettings();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!firstTouch) return;
        if (!other.gameObject.CompareTag("Player")) return;

        if (advancedEffectsOn)
        {
            emitParams.position = other.transform.position;
            playerChangeParticles.Emit(emitParams, 70);
        }

        if (advancedLightingOn)
        {
            playerLight.color = changeColor;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (!firstTouch) return;
        if (!other.gameObject.CompareTag("Player")) return;

        if (advancedLightingOn)
        {
            playerLight.color = originalColor;
        }
        
        firstTouch = false;
    }
}
