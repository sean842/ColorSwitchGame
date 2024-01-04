using UnityEngine;

public class Rotator : MonoBehaviour
{

    public float speed = 100;
    public static int success = 0;// var for making game harder. if we pass 5 it will rotate faster.


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,speed*Time.deltaTime);

        // every 3 circles we make it rotate faster!.
        if(success % 3 == 0 && success > 0)
        {
            speed += 50;
            success = 0;
            Debug.Log("faster");
        }

    }
}
