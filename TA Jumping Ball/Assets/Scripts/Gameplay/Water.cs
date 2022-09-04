using UnityEngine;
using UnityEngine.Events;

public class Water : MonoBehaviour
{
    [SerializeField, Min(0.01f)] private float _liftingSpeed = 1f;
    [SerializeField, Tooltip("Наибольшее расстояние между целевым объектом и текущим."), Min(0.01f)]
    private float _maxDistance = 10f;
    [SerializeField] private float _smoothTime = 0.3f;
    public UnityEvent OnWaterReachedPlayer;
    private Vector3 _targetPosition;
    private Vector3 _velocity = Vector3.zero;
    private bool _isLifting;

    public void ResetValues(Vector3 startPoint)
    {
        _targetPosition = startPoint - _maxDistance * Vector3.up;
        transform.position = _targetPosition;
        _isLifting = true;
    }

    public void CheckDistance(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > _maxDistance)
        {
            _targetPosition = target.position - _maxDistance * Vector3.up;
        }
    }

    public void Stop()
    {
        _isLifting = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isLifting)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Stop();
                OnWaterReachedPlayer.Invoke();
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isLifting)
        {
            _targetPosition += _liftingSpeed * Time.fixedDeltaTime * Vector3.up;
            transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _velocity, _smoothTime);
        }
    }
}