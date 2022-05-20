using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerMovementStats")]
public class PlayerMovement_ScriptableObject : ScriptableObject
{
    [field: SerializeField]
    public float Speed { get; private set; } = 10.0f;
    [field: SerializeField]
    public float DashSpeed { get; private set; } = 40.0f;
    [field: SerializeField]
    public float DashCD { get; private set; } = 0.5f;
    [field: SerializeField]
    public float DashDuration { get; private set; } = 0.1f;
    
    [field: SerializeField]
    public Rigidbody2D PlayerRB;

    private void OnDisable()
    {
        PlayerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
}

