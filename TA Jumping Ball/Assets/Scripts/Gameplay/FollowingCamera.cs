using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private bool _constantFollowing = true;
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothTime = 0.3f;
    private Vector3 _targetPosition;
    private Vector3 _velocity = Vector3.zero;

    private void Start()
    {
        enabled = _target;
    }

    public void SetTarget(Transform target, Vector3 startPosition)
    {
        _target = target;
        transform.position = startPosition;
        _targetPosition = startPosition;
        enabled = true;
    }

    public void RefreshTargetPosition()
    {
        _targetPosition = _target.position;
    }

    private void LateUpdate()
    {
        if (_constantFollowing) RefreshTargetPosition();
        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _velocity, _smoothTime);
    }
}