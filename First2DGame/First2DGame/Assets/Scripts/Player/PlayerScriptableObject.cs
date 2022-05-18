using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerScriptableObject : ScriptableObject
{
    [field: SerializeField]
    public int Health { get; private set; } = 3;
    [field: SerializeField]
    public float Speed { get; private set; } = 10.0f;
    [field: SerializeField]
    public float DashSpeed { get; private set; } = 50.0f;
    [field: SerializeField]
    public float DashCD { get; private set; } = 0.5f;
    [field: SerializeField]
    public float DashDuration { get; private set; } = 1f;
    [field: SerializeField]
    public Rigidbody2D PlayerRB { get; set; }
    [field: SerializeField]
    public int playerLayer { get; private set; } = 6;
}
