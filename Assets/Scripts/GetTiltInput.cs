using UnityEngine;

public class GetTiltInput : MonoBehaviour
{
    private Vector3 playerOrientation;
    void Start()
    {
        playerOrientation = Input.acceleration;
    }

    void Update()
    {
        Vector3 currentAdjustedAcceleration = Input.acceleration - playerOrientation;
        
        GravityManager.Instance.ChangeGravityVector(currentAdjustedAcceleration);
    }
    
    /*protected void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        GUILayout.Label("playerOrientation: " + playerOrientation);
        GUILayout.Label("input.acceleration: " + Input.acceleration);
        GUILayout.Label("currentAdjustedAcceleration: " + (Input.acceleration - playerOrientation));
        GUILayout.Label("gravity: " + (Input.acceleration - playerOrientation) *20f);
        GUILayout.Label("Physics2D.gravity: " + (Physics2D.gravity));
    }*/
}
