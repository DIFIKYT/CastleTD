using UnityEngine;

[CreateAssetMenu(fileName = "New unit config", menuName = "Unit/Configs/Create new unit Config")]
public class UnitConfig : ScriptableObject
{
    [SerializeField] private Unit _prefab;
    [SerializeField] private UnitStats _stats;

    public Unit Prefab => _prefab;
    public UnitStats Stats => _stats;
}