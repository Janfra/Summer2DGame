using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    [SerializeField]
    Transform playerPosition;
    Rigidbody2D thisRB;

    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

        thisRB = this.GetComponent<Rigidbody2D>();

        if (playerPosition == null)
            Debug.LogError(gameObject.name + "has not set the player position to face the player!");
        if (thisRB == null)
            Debug.LogError(gameObject.name + "has not set the ridigbody to face the player!");
    }

    private void Update()
    {
        LookAtPlayer();
    }

    public void LookAtPlayer()
    {
        Vector2 faceDirection = (Vector2)playerPosition.position - thisRB.position;
        Debug.Log(faceDirection);
        float angle = (Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Deg2Rad);
        thisRB.rotation = angle;
        Debug.Log(angle);
    }
}
