using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraEffects : MonoBehaviour
{
    private Camera thisCamera;
    public void Start()
    {
        thisCamera = gameObject.GetComponent<Camera>();
    }
    
    public void Shake(float impulse)
    {
        StartCoroutine(shake(impulse/7, impulse/ 1.5f,
            impulse/15, impulse * 2.5f, 1, 20/impulse));
    }

    public void ZoomIn()
    {
        StartCoroutine(zoom(thisCamera.orthographicSize, 2f, 4));
    }

    IEnumerator shake(float duration, float frequency, float maxTranslate, float maxRotate, float trauma, float recoverySpeed)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            
            float shake = Mathf.Pow(trauma, 2);
            transform.localPosition = new Vector3(
                Mathf.PerlinNoise(0, Time.time * frequency) * 2 - 1,
                Mathf.PerlinNoise(1, Time.time * frequency) * 2 - 1,
                Mathf.PerlinNoise(2, Time.time * frequency) * 2 - 1
            ) * (maxTranslate * shake);
            
            transform.localRotation = Quaternion.Euler(new Vector3(
                Mathf.PerlinNoise(0, Time.time * frequency) * 2 - 1,
                Mathf.PerlinNoise(1, Time.time * frequency) * 2 - 1,
                Mathf.PerlinNoise(2, Time.time * frequency) * 2 - 1
            ) * (maxRotate * shake));
            
            trauma = Mathf.Clamp01(trauma - recoverySpeed * Time.deltaTime);
            yield return 0;
        }
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    IEnumerator zoom (float startSize, float endSize, float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            thisCamera.orthographicSize = Mathf.SmoothStep(startSize, endSize, elapsed / duration);
            yield return 0;
        }
    }
    
}

