using UnityEngine;

public class Rotator : MonoBehaviour
{
    // --- script to rotate the circle wheel. ---

    //private float baseSpeed = 100;
    //private float speed;



    //public GameManager gameManager;

    //private void Start()
    //{
    //    gameManager = FindObjectOfType<GameManager>();
    //    speed = baseSpeed + gameManager.speedToAdd;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    transform.Rotate(0,0,speed*Time.deltaTime);

    //}



    private float baseSpeed = 100;
    private float speed;

    private float rotationTimer;
    private float normalDuration = 3f;
    private float halfSpeedDuration = 2f;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        UpdateSpeed(); // Call this to set the initial speed
    }

    private void Update()
    {
        RotateCircle();

        // Check if it's time to switch rotation state
        if (rotationTimer >= GetDurationForCurrentState())
        {
            SwitchRotationState();
        }
    }

    private void RotateCircle()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);

        // Update the timer
        rotationTimer += Time.deltaTime;
    }

    private void SwitchRotationState()
    {
        rotationTimer = 0f;

        // Switch between normal and half-speed rotation
        if (speed == baseSpeed)
        {
            gameManager.speedToAdd += 50; // Add speed every 3 passes directly
            UpdateSpeed(); // Update the speed immediately
        }
    }

    private float GetDurationForCurrentState()
    {
        return (speed == baseSpeed) ? normalDuration : halfSpeedDuration;
    }

    // Method to update the speed based on GameManager's speedToAdd
    private void UpdateSpeed()
    {
        speed = baseSpeed + gameManager.speedToAdd;
    }
}
