using UnityEngine;

public class Mover : MonoBehaviour
{
    private float _moveSpeed;
    private float _rotateSpeed;

    private Transform _target;

    public void Initialize(float moveSpeed, float rotateSpeed)
    {
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
        _target = target;
    }

    private void Move()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = _target.position;

        targetPosition.y = transform.position.y;

        transform.position = Vector3.MoveTowards(
            currentPosition,
            targetPosition,
            _moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            _rotateSpeed * Time.deltaTime);
    }
}