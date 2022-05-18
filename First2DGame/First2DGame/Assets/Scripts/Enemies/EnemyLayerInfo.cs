using UnityEngine;

[CreateAssetMenu(fileName = "EnemyLayerInfo", menuName = "ScriptableObjects/EnemyLayer")]
public class EnemyLayerInfo : ScriptableObject
{
    [field: SerializeField]
    public int enemyLayer { get; private set; } = 7;
}
