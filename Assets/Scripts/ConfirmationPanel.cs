using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationPanel : MonoBehaviour
{
    public void ConfirmButton()
    {
        Time.timeScale = 1;
        LevelManager.Instance.DeleteSaveData();
    }
    
    public void CloseButton()
    {
        gameObject.GetComponentInChildren<Animator>().SetTrigger("Start");
        Destroy(gameObject, 1);
    }
}
