using UnityEngine;

[CreateAssetMenu(fileName = "EnemyHealth_SO", menuName = "ScriptableObjects/EnemyHealth")]
public class EnemyHealth_SO : ScriptableObject
{
    [field: SerializeField]
    public int Health { get; private set; } = 3;
}
