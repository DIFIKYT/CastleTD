using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Unit Config", fileName = "UnitConfig")]
public class UnitConfig : ScriptableObject
{
    [SerializeField] private Unit _prefab;
    [SerializeField] private UnitStats _stats;

    public Unit Prefab => _prefab;
    public UnitStats Stats => _stats;
}