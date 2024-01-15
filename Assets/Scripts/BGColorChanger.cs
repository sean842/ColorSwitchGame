using UnityEngine;

public class BGColorChanger : MonoBehaviour {

    [SerializeField] private Camera cam;
    [SerializeField] private Color[] colors;
    [SerializeField] private float colorChangeSpeed;
    [SerializeField] private float time;
    private float currentTime;
    private int colorIndex;

    private void Awake() {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ColorChanger();
        ColorChangeTime();
    }


    void ColorChanger() {
        cam.backgroundColor = Color.Lerp(cam.backgroundColor, colors[colorIndex], time * Time.deltaTime);
    }

    void ColorChangeTime() {
        if (currentTime <= 0) {
            colorIndex++;
            CheckColorIndex();
            currentTime = time;
        }
        else {
            currentTime -= Time.deltaTime;
        }
    }

    void CheckColorIndex() {
        if (colorIndex >= colors.Length) {
            colorIndex = 0;
        }
    }


}
