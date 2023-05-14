using System.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform _playerTransform;
    private const float UpdateSpeed = 0.15f;
    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        var playerPosition = _playerTransform.position;
        var thisTransform = transform;
        offset.z = thisTransform.position.z - playerPosition.z;
        thisTransform.position = playerPosition + offset;
    }

    void LateUpdate()
    {
        var desiredPosition = _playerTransform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, UpdateSpeed);
    }
}
