using System;
using UnityEngine;

public class MovingBlockScript : MonoBehaviour
{
    public bool ReverseGravity = false;

    private void Start()
    {
        if (ReverseGravity)
        {
            var thisRigidBody = gameObject.GetComponent<Rigidbody2D>();
            thisRigidBody.gravityScale = -thisRigidBody.gravityScale;
        }
    }
}
