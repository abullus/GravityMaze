using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelsScript : MonoBehaviour
{
    public void Pause()
    {
        PanelManager.Instance.Pause();
        Time.timeScale = 0;
    }

    public void Resume()
    {
        PanelManager.Instance.Resume_ShowOverlay();
        Unpause();
        gameObject.GetComponent<Animator>().SetTrigger("Start");
        StartCoroutine(Wait(0.5f));
    }

    public void Unpause()
    {
        Time.timeScale = 1;
    }

    IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
        PanelManager.Instance.Resume_HidePause();
    }
}
