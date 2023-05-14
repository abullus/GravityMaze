using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ExpanderScript : MonoBehaviour
{
    public bool contractor;
    private bool firstTouch;
    private Transform playerTransform;
    private float scaleAmount = 0.1f;
    void Start()
    {
        firstTouch = true;

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!firstTouch) return;
        if (!other.gameObject.CompareTag("Player")) return;
        if (contractor)
        {
            if (Math.Abs(playerTransform.localScale.x - 0.1f) < 0.03) return;
            playerTransform.localScale -= scaleAmount * Vector3.one;
        }
        else
        {
            if (Math.Abs(playerTransform.localScale.x - 0.3f) < 0.03) return;
            playerTransform.localScale += scaleAmount * Vector3.one;

        }
        firstTouch = false;
    }
}
