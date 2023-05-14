using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;

public class DeathHoleScript : MonoBehaviour
{
    public ParticleSystem Particles;
    public ParticleSystem PlayerDestroyedEffect;

    private ParticleSystem thisParticles;

    private bool advancedEffectsOn;
    void Start()
    {
        CheckSettings();
    }

    public void CheckSettings()
    {
        advancedEffectsOn = SettingsManager.Instance.AdvancedEffectsOn;
        
        if (thisParticles)
        {
            Destroy(thisParticles);
        }
        
        if (advancedEffectsOn)
        {
            thisParticles = Instantiate(Particles, transform, false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (advancedEffectsOn)
            {
                Instantiate(PlayerDestroyedEffect, transform, false);
            }
            
            other.gameObject.GetComponent<PlayerScript>().TouchingHole(transform.position);
        }
    }
}