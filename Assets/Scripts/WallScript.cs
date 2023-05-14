using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering.Universal;

public class WallScript : MonoBehaviour
{
    public ParticleSystem collisionEffect;
    public bool movingBlock = false;
    private float particleSpeedMultiplier;
    private float particleNumberMultiplier;
    private float movingBlockParticleNumberMultiplier;

    private float startSpeedThreshold = 1.8f;
    private float burstNumberThreshold = 5;
    private float cameraShakeThreshold = 4f;
    private float speedThreshold;
    private ParticleSystem thisParticleSystem;
    private CameraEffects cameraScript;


    private void Start()
    {
        CheckSettings();

        cameraScript = FindObjectOfType<CameraEffects>();

        thisParticleSystem = Instantiate(collisionEffect, Vector3.zero, quaternion.identity);
    }

    public void CheckSettings()
    {
        SetParticleValues(SettingsManager.Instance.AdvancedEffectsOn);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float relativeSpeed = collision.relativeVelocity.magnitude;
            if (relativeSpeed < 2)
            {
                SoundEffectsManager.Instance.MakeCollisionSound(relativeSpeed / 15);
                return;
            }
            float impulse = ComputeTotalImpulse(collision);
            
            SoundEffectsManager.Instance.MakeCollisionSound(impulse / 7);
            
            if (impulse < speedThreshold) return;

            if (impulse > cameraShakeThreshold)
            {
                cameraScript.Shake(impulse);
            }

            var main = thisParticleSystem.main;
            main.startSpeed = impulse * particleSpeedMultiplier;

            var emitParams = new ParticleSystem.EmitParams
            {
                position = collision.GetContact(0).point,
                applyShapeToPosition = true
            };
            thisParticleSystem.Emit(emitParams, Mathf.RoundToInt(particleNumberMultiplier * impulse * impulse));
        }
    }
    
    private static float ComputeTotalImpulse(Collision2D collision)
    {
        Vector2 impulse = Vector2.zero;

        int contactCount = collision.contactCount;
        for(int i = 0; i < contactCount; i++) {
            ContactPoint2D contact = collision.GetContact(i);
            impulse.x += Mathf.Abs(contact.normalImpulse);
            impulse.y += Mathf.Abs(contact.tangentImpulse);
        }

        return impulse.magnitude;
    }
    
    private void SetParticleValues(bool advancedEffectsOn)
    {
        if (advancedEffectsOn)
        {
            particleSpeedMultiplier = 2.5f; 
            particleNumberMultiplier = 10f; 
            movingBlockParticleNumberMultiplier = 12f;
        }
        else
        { 
            particleSpeedMultiplier = 2.5f; 
            particleNumberMultiplier = 5f; 
            movingBlockParticleNumberMultiplier = 6f;
        }
        
        if (movingBlock)
        {
            particleNumberMultiplier = movingBlockParticleNumberMultiplier;
        }
        
        speedThreshold = Mathf.Max(
            startSpeedThreshold / particleSpeedMultiplier,
            burstNumberThreshold / particleNumberMultiplier);
    }
}
