using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {


    /* 
     * Incharge of circles, Add, Destry, Speed.
     */


    private List<GameObject> circleList = new List<GameObject>(); // List to store the circles
    public GameObject circlePrefab; // prefab to instantiate

    public int passCounter = 0; // Counter to track how many times the player has passed the circle wheel
    public int speedToAdd = 0;
    public bool difficult = false;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI startCount;

    public GameOver GameOverScript;
    public GameObject GameOverUI;
    public GameObject GameStage;

    // Start is called before the first frame update
    void Start() {
        // Find the initial circle in the scene and add it to the list
        GameObject initialCircle = GameObject.FindGameObjectWithTag("Circle");
        AddToCircleList(initialCircle);
        
    }

    // Method to add a circle to the list.
    public void AddToCircleList(GameObject circle) {
        circleList.Add(circle);
        RemoveFirstCircleIfNecessary();
        IncreaseSpeedIfNecessary();
    }

    // Method to remove the first circle in the list & Destroy it if there are more than 2 circles.
    private void RemoveFirstCircleIfNecessary() {
        if (circleList.Count > 2) {
            GameObject firstCircle = circleList[0]; // Find the first circle.
            circleList.RemoveAt(0); // Remove the circle from the list.
            Destroy(firstCircle); // Destroy it from the game.
        }
    }

    // Method to increase the rotation speed if the circle count is a multiple of 3
    private void IncreaseSpeedIfNecessary() {
        // Increase the pass counter when a circle is added
        passCounter++;
        if (passCounter % 3 == 0) {
            speedToAdd += 50;
        }
        if (passCounter > 4) {
            difficult = true;
        }
    }

    public void UpdateScore() {
        score++;
        scoreText.text = score.ToString();
    }

    public void GameOver() {
        foreach(GameObject circle in circleList) {
            Destroy(circle);
        }
        GameStage.SetActive(false);
        GameOverUI.SetActive(true);
        GameOverScript.GameOverFunc(score);
    }

}
