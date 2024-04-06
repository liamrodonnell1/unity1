using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject completeLevelUI;
    public void CompleteLevel ()
    {
        completeLevelUI.SetActive(true);
    }
}
