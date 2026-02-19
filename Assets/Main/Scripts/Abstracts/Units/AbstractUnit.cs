using UnityEngine;

public abstract class AbstractUnit : MonoBehaviour
{
    [SerializeField] private LanePushMovement _lanePushMovement;
    [SerializeField] private TargetDetector _targetDetector;
    [SerializeField] private AttackBehaviour _attackBehaviour;
    [SerializeField] private Health _health;
}