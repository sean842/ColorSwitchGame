using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    /* script for the player.
     * 1.make the player change color.
     * 2.make him jump.
     * 3.check collision:
     *   in the color changer - if true we:
     *     change color,
     *     instaniate new color changer & circle wheel,
     *     destroy the color changer & circle.
     *   in the circle wheel - if true we lose.
     */

    [Header("Player")]
    private float jumpForce = 5f;
    private string currentColor;
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    [SerializeField] private GameObject currentColorChanger;


    [Header("prefabs")]
    public GameObject colorChanger;
    public GameObject Circle;

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
        //manager = gameObject.AddComponent<GameManager>();
        manager = FindObjectOfType<GameManager>();
        SetRandomColor();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)) {
            // we add velocity insted of adding force because adding force could effect the player when it goes up & down, and we want only up.
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // if we hit the color changer we change color and dwstroy it. later we instantiate it again.
        if (collision.tag == "ColorChanger") {
            // we set rendom color and destroy the color changer.
            SetRandomColor();
            Destroy(collision.gameObject);
            manager.UpdateScore();

            // instantiate a new circle & add to list.
            GameObject newCircle = Instantiate(Circle, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), Quaternion.identity);
            manager.AddToCircleList(newCircle);

            // instantiate a new colorChanger.
            currentColorChanger = Instantiate(colorChanger, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.identity);
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


}
