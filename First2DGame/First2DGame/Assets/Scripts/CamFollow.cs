using UnityEngine;

public class CamFollow : MonoBehaviour
{
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerFollow();
    }

    void PlayerFollow()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
