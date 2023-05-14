using System;
using UnityEngine;

public class RotateWithGravity : MonoBehaviour
{
    public bool IsOrientedWithGravity = false;

    private int upwardsFactor = 1;

    private void Start()
    {
        if (!IsOrientedWithGravity)
        {
            upwardsFactor = -1;
        }
    }

    private void Update()
    {
        Vector2 currentGravity = Physics2D.gravity;
        Quaternion quaternion = Quaternion.LookRotation(Vector3.forward, upwardsFactor * currentGravity);
        transform.localRotation = quaternion;
    }
    
}
