using UnityEngine;

[CreateAssetMenu(fileName = "New unit", menuName = "Unit/Create new unit", order = 51)]
public class UnitStats : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private int _maxHitPoints;
    [SerializeField] private int _damageValue;
    [SerializeField] private float _stoppingDistance;

    public string Name => _name;
    public float MoveSpeed => _moveSpeed;
    public float RotateSpeed => _rotateSpeed;
    public int MaxHitPoints => _maxHitPoints;
    public int DamageValue => _damageValue;
    public float StoppingDistance => _stoppingDistance;
}