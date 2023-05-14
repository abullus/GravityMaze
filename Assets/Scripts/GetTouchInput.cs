using UnityEngine;

public class GetTouchInput : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                GravityManager.Instance.ChangeGravityAngle(true);
            }

            if (touch.position.x > Screen.width / 2)
            {
                GravityManager.Instance.ChangeGravityAngle(false);
            }
        }
    }
}
