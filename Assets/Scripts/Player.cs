using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour {

    /* script for the player.
     * 1.count to 0 before we start
     * 2.make the player change color.
     * 3.make him jump.
     * 4.check collision:
     *   - in the color changer - if true we:
     *       change color,
     *       instaniate new color changer & circle wheel,
     *       destroy the color changer & circle.
     *   - in the circle wheel - if true we lose.
     */

    [Header("Player")]
    private float jumpForce = 5f;
    private string currentColor;
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    [Header("others")]
    private GameObject currentColorChanger;
    private GameObject currentSuccessEffect;
    private bool canJump = false;

    [Header("Timer")]
    public float countdownTime = 5f;
    public TextMeshProUGUI countdownText;

    [Header("prefabs")]
    public GameObject colorChanger;
    public GameObject Circle;
    public GameObject successEffect;

    [Header("scripts")]
    public GameManager manager;

    [Header("lists")]
    public List<string> colorTags = new List<string>() { "Cyan", "Yellow", "Pink", "Magenta" };
    public List<Color> colorList = new List<Color>
    {
        new Color(53, 226, 242), // Cyan
        new Color(246, 223, 14), // Yellow
        new Color(255, 0, 128),  // Pink
        new Color(140, 19, 251)  // Magenta
    };

    private void Start() {
        manager = FindObjectOfType<GameManager>();
        currentColorChanger = GameObject.FindGameObjectWithTag("ColorChanger");
        SetRandomColor();
        StartCoroutine(StartGameCountdown());
    }

    // Update is called once per frame
    void Update() {
        if (canJump) {
            if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)) {
                // we add velocity insted of adding force because adding force could effect the player when it goes up & down, and we want only up.
                rb.velocity = Vector2.up * jumpForce;
            }
        }

        // if the player faal down it's GameOver.
        if(currentColorChanger.transform.position.y - transform.position.y > 20) {
            manager.GameOver();
        }

    }

    IEnumerator StartGameCountdown() {
        rb.gravityScale = 0f; // Disable gravity during the countdown
        float timer = countdownTime;

        while (timer > 0f) {
            countdownText.text = timer.ToString("F0");

            // Animate the scale of the countdown text from 1 to 1.3 over 1 second
            LeanTween.scale(countdownText.gameObject, new Vector3(1.5f, 1.5f, 1f), 1f).setEase(LeanTweenType.easeOutQuad);

            yield return new WaitForSeconds(1f); // Wait for 1 second
            countdownText.transform.localScale = Vector3.one; // Reset the scale back to 1
            timer -= 1f; // Decrease by 1 second
        }

        rb.gravityScale = 1f; // Enable gravity after the countdown
        countdownText.text = "GO!";
        yield return new WaitForSeconds(0.3f);
        countdownText.text = ""; // Clear the countdown text
        canJump = true;
    }


    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "ColorChanger") {
            InstantiateGameObejects();
            SetRandomColor();
            Destroy(collision.gameObject);
            manager.UpdateScore();
            return;
        }

        // if we hit a diffrent color it's GAME OVER!
        if (collision.tag != currentColor) {
            Destroy(currentColorChanger);
            manager.GameOver();
        }
    }

    void SetRandomColor() {
        // get the color list
        List<Color> newColorList = new List<Color>(colorList);
        List<string> newColorTags = new List<string>(colorTags);

        // check if the corrent color isnt null or empty
        if (newColorList.Contains(sr.color)) {
            //get the color of the player from the list
            newColorList.Remove(sr.color);
            newColorTags.Remove(currentColor);
        }
        // pick a random color for the player.
        int index = Random.Range(0, newColorList.Count);
        sr.color = newColorList[index];
        currentColor = newColorTags[index];
    }

    void InstantiateGameObejects() {
        // Effect Prefab.
        currentSuccessEffect = Instantiate(successEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(currentSuccessEffect, 2f);

        // instantiate a new circle & add to list.
        GameObject newCircle = Instantiate(Circle, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), Quaternion.identity);
        manager.AddToCircleList(newCircle);

        // instantiate a new colorChanger.
        currentColorChanger = Instantiate(colorChanger, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.identity);
    }



}
