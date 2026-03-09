using UnityEngine;

public class Mover : MonoBehaviour
{
    private float _moveSpeed;
    private float _rotateSpeed;

    private Transform _currentTarget;
    private Transform _transform;

    public void Initialize(Transform transform, float moveSpeed, float rotateSpeed)
    {
        _transform = transform;
        _moveSpeed = moveSpeed;
        _rotateSpeed = rotateSpeed;
    }

    public void MoveToTarget()
    {
        Move();
        Rotate();
    }

    public void ChangeTarget(Transform target)
    {
        _currentTarget = target;
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