using UnityEngine;
[CreateAssetMenu(fileName = "PlayerHealth_OB", menuName = "ScriptableObjects/PlayerHealth")]

public class PlayerHealth_SO : ScriptableObject
{
    [field: SerializeField]
    public int Health { get; private set; } = 3;
}