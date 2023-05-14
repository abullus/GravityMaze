using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetArrowInput : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GravityManager.Instance.ChangeGravityAngle(true);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            GravityManager.Instance.ChangeGravityAngle(false);
        }
    }
}
