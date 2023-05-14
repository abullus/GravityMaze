using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainMenuScript : MonoBehaviour
{
    private int rotateCount;
    private bool clockwise;

    private void Awake()
    {
        PanelManager.Instance.SceneLoaded(false, "MainMenu");
    }

    private void Start()
    {
        GravityManager.Instance.sensitivityFactor = 0.3f;
    }

    private void Update()
    {
        GravityManager.Instance.ChangeGravityAngle(true);
        
    }
}
