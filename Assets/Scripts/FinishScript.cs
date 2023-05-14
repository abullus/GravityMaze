using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FinishScript : MonoBehaviour
{
    public GameObject SpinningBackground;
    public ParticleSystem FinishSparkleEffect;
    public ParticleSystem levelWonEffect;
    public ParticleSystem DestroyedEffect;

    private Light2D finishLight;

    private ParticleSystem thisFinishSparkleEffect;
    private GameObject thisSpinningBackground;

    private bool advancedEffectsOn;

    private void Start()
    {
        CheckSettings();
    }

    public void CheckSettings()
    {
        advancedEffectsOn = SettingsManager.Instance.AdvancedEffectsOn;

        if (thisSpinningBackground)
        {
            Destroy(thisSpinningBackground);
        }

        if (thisFinishSparkleEffect)
        {
            Destroy(thisFinishSparkleEffect);
        }
        
        if (advancedEffectsOn)
        {
            thisFinishSparkleEffect = Instantiate(FinishSparkleEffect, transform, false);
            thisSpinningBackground = Instantiate(SpinningBackground, transform, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            SoundEffectsManager.Instance.FinishSound();
            if (advancedEffectsOn)
            {
                Instantiate(levelWonEffect, transform, false);
            }

            otherCollider.gameObject.GetComponent<PlayerScript>().TouchingFinish();
        }
        else
        {
            Destroy(otherCollider.gameObject);
            var destroyEffect = Instantiate(DestroyedEffect, transform, false);
            Destroy(destroyEffect, 3f);
        }
    }
}
