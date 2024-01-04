using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;

    private void Update()
    {
        // we check if the player is moving up, if he is we move the camera with him.
        if(player.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        }
    }



}
