using UnityEngine;
[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/EnemyStats")]
public class EnemyScriptableObject : ScriptableObject
{
    [field: SerializeField]
    public float Speed { get; private set; } = 10.0f;
    [field: SerializeField]
    public float RangeToWait { get; private set; } = 10.0f;
    [field: SerializeField]
    public float chaseTimer { get; private set; } = 2.0f;
    [field: SerializeField]
    public int Damage { get; private set; } = 1;
    [field: SerializeField]
    public float AttackRange { get; private set; } = 10.0f;
    [field: SerializeField]
    public float attackTimer { get; private set; } = 3.0f;
    public EnemyLayerInfo Layer;
}
