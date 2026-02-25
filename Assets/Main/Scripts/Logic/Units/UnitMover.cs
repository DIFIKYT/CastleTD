using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _stoppingDistance;

    private Transform _currentTarget;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void GetTarget(Transform target)
    {
        _currentTarget = target;
    }

    private void Update()
    {
        if (_currentTarget == null)
            return;

        float distance = Vector3.Distance(_transform.position, _currentTarget.position);

        if (distance > _stoppingDistance)
        {
            Move();
            Rotate();
        }
    }

    private void Move()
    {
        Vector3 currentPosition = _transform.position;
        Vector3 targetPosition = _currentTarget.position;

        targetPosition.y = _transform.position.y;

        _transform.position = Vector3.MoveTowards(
            currentPosition,
            targetPosition,
            _moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        Vector3 direction = (_currentTarget.position - _transform.position).normalized;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        _transform.rotation = Quaternion.RotateTowards(
            _transform.rotation,
            targetRotation,
            _rotateSpeed * Time.deltaTime);
    }
}