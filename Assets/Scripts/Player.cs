using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float jumpForce = 5f;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public string currentColor;

    [Header("Colors")]
    public Color colorCyan;
    public Color colorYellow;
    public Color colorPink;
    public Color colorMagenta;

    [Header("prefabs")]
    public GameObject colorChanger;
    public GameObject Circle;




    private void Start()
    {
        SetRandomColor();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            // we add velocity insted of adding force because adding force could effect the player when it goes up & down, and we want only up.
            rb.velocity = Vector2.up*jumpForce;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if we hit the color changer we change color and dwstroy it. later we instantiate it again.
        if(collision.tag == "ColorChanger")
        {
            SetRandomColor();
            Destroy(collision.gameObject);

            // instantiate a new circle.
            Instantiate(Circle, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), Quaternion.identity);

            Rotator.success++;

            // instantiate a new colorChanger..
            Instantiate(colorChanger, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.identity);

            return;
            // we need to destroy the last circle!!!
        }

        // if we hit a diffrent color it's GAME OVER!
        if (collision.tag != currentColor)
        {
            Debug.Log("game over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }

    void SetRandomColor()
    {
        int index = Random.Range(0, 4);

        // maybe check if it's thw same color.
        switch (index)
        {
            case 0:
                currentColor = "Cyan";
                sr.color = colorCyan;
                break;
            case 1:
                currentColor = "Yellow";
                sr.color = colorYellow;
                break;
            case 2:
                currentColor = "Pink";
                sr.color = colorPink;
                break;
            case 3:
                currentColor = "Magenta";
                sr.color= colorMagenta;
                break;
        }
    }

}
