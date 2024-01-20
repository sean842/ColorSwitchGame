using System.Collections;
using UnityEngine;

public class StartBTN : MonoBehaviour
{
    public GameObject startScreenUI;
    public GameObject gameUI;

    public Fader Fader;
    
    public void Press() {
        Fader.Fading(startScreenUI, gameUI);
        //startScreenUI.SetActive(false);
        //gameUI.SetActive(true);
    }


}
