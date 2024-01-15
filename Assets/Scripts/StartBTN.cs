using UnityEngine;

public class StartBTN : MonoBehaviour
{
    public GameObject startScreenUI;
    public GameObject gameUI;

    public void Press() {
        startScreenUI.SetActive(false);
        gameUI.SetActive(true);
    }



}
