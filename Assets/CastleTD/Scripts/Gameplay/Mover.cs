// using UnityEngine;

// public class Mover : MonoBehaviour
// {
//     private float _moveSpeed;
//     private float _rotateSpeed;

//     private Transform _target;

//     public void Initialize(float moveSpeed, float rotateSpeed)
//     {
//         _moveSpeed = moveSpeed;
//         _rotateSpeed = rotateSpeed;
//     }

//     public void Reset()
//     {
//         _target = null;
//     }

//     public void MoveToTarget()
//     {
//         Move();
//         Rotate();
//     }

//     public void ChangeTarget(Transform target)
//     {
//         _target = target;
//     }

//     private void Move()
//     {
//         Vector3 currentPosition = transform.position;
//         Vector3 targetPosition = _target.position;

//         targetPosition.y = transform.position.y;

//         transform.position = Vector3.MoveTowards(
//             currentPosition,
//             targetPosition,
//             _moveSpeed * Time.deltaTime);
//     }

//     private void Rotate()
//     {
//         if (_target == null) return;

//         Vector3 direction = _target.position - transform.position;
//         direction.y = 0f;

//         if (direction.sqrMagnitude < 0.001f)
//             return;

//         Quaternion targetRotation = Quaternion.LookRotation(direction);

//         Vector3 euler = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime).eulerAngles;
//         transform.rotation = Quaternion.Euler(0f, euler.y, 0f);
//     }
// }

using UnityEngine;

public class Mover : MonoBehaviour
{
    private float _moveSpeed;
    private float _rotateSpeed;

    private Transform _target;

    [Header("Avoidance")]
    [SerializeField] private float _avoidRadius = 0.5f; // радиус проверки соседей
    [SerializeField] private LayerMask _unitLayer;      // слой для юнитов
    [SerializeField] private float _avoidStrength = 1f; // сила смещения

    public void Initialize(float moveSpeed, float rotateSpeed)
    {
        _moveSpeed = moveSpeed;
        _rotateSpeed = rotateSpeed;
    }

    public void Reset()
    {
        _target = null;
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
        if (_target == null) return;

        Vector3 currentPosition = transform.position;
        Vector3 desiredPosition = _target.position;
        desiredPosition.y = transform.position.y;

        // --- Обход юнитов ---
        Vector3 offset = Vector3.zero;
        Collider[] neighbors = Physics.OverlapSphere(transform.position, _avoidRadius, _unitLayer);

        foreach (var neighbor in neighbors)
        {
            if (neighbor.transform == transform) continue;

            Vector3 diff = transform.position - neighbor.transform.position;
            float distance = diff.magnitude;
            if (distance > 0 && distance < _avoidRadius)
            {
                // смещаем от юнита пропорционально приближению
                offset += diff.normalized * (_avoidRadius - distance) * _avoidStrength;
            }
        }

        Vector3 finalTarget = desiredPosition + offset;

        // плавное движение
        transform.position = Vector3.MoveTowards(currentPosition, finalTarget, _moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        if (_target == null) return;

        Vector3 direction = _target.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);

        // ограничиваем поворот только по Y
        transform.rotation = Quaternion.Euler(0f, newRotation.eulerAngles.y, 0f);
    }
}