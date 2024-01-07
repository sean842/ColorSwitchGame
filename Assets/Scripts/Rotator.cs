using UnityEngine;

public class Rotator : MonoBehaviour {


    // --- script to rotate the circle wheel. ---
    [Header("Speed")]
    public float baseSpeed = 100;
    public float speed;

    public float rotationTimer = 5;
    public float normalDuration = 2f;
    public float halfSpeedDuration = 5f;

    public bool checkDuration = false;// if true - normal timet; else - half timer.

    //public bool difficult = false;
    public GameManager gameManager;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
        speed = baseSpeed + gameManager.speedToAdd;
    }

    private void Update() {

        if (!gameManager.difficult) {
            // spin normal and speed up when needed. 
            Rotation();
        }
        else {
           
            Rotation();
            UpdateTimer();

            // Check if it's time to switch rotation state
            if (rotationTimer <= 0) {
                SetTimeAndSpeed();
            }

        }

    }

    /// <summary>
    /// rotate the circle normaly.
    /// </summary>
    void Rotation() {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }

    void UpdateTimer() {
        // Update the timer
        rotationTimer -= Time.deltaTime;
    }

    void SetTimeAndSpeed() {
        // check for timer state & change timer time and rotation speed.
        if (checkDuration == true) { 
            rotationTimer = normalDuration;
            speed = baseSpeed + gameManager.speedToAdd;
            checkDuration = false;

        }
        else {
            rotationTimer = halfSpeedDuration;
            speed = speed / 2;
            checkDuration = true;
        }
    }


}
