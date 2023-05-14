using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    public void CloseButton()
    {
        gameObject.GetComponentInChildren<Animator>().SetTrigger("Start");
        Destroy(gameObject, 1);
    }
}
