using System;
using UnityEngine;

public class MovingPlatform : Platform
{
    [SerializeField] private Transform _changeableTransform;
    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;
    [SerializeField, Range(0.01f, 0.99f)] private float _startFactorValue = 0.5f;
    [SerializeField] private bool _movesToPoint2 = true;
    [SerializeField, Min(0.1f)] private float _interpolationSpeed = 0.1f;
    [SerializeField, Tooltip("Если true, при интерполяции учитывается расстояние между точками.")]
    private bool _considerDistance = false;
    private Vector3 _left;
    private Vector3 _right;
    private float _factorMultiplier;
    private float _factorValue = 0f;
    private bool _isWorking;

    public override void ResetPlatform()
    {
        _factorValue = _startFactorValue;
        if (_movesToPoint2)
        {
            _left = _point1.position;
            _right = _point2.position;
        }
        else
        {
            _left = _point2.position;
            _right = _point1.position;
        }
        _changeableTransform.position = Vector3.Lerp(_point1.position, _point2.position, _factorValue);
        _factorMultiplier = _considerDistance ? _interpolationSpeed / Vector3.Distance(_left, _right) : _interpolationSpeed;
        _isWorking = true;
    }

    protected override void EnterCollisionPlatformAction()
    {
        _isWorking = false;
    }

    private void FixedUpdate()
    {
        if (_isWorking)
        {
            _factorValue += Time.fixedDeltaTime * _factorMultiplier;
            if (_factorValue >= 1f)
            {
                _factorValue = 0f;
                Vector3 temp = _left;
                _left = _right;
                _right = temp;
            }
            _changeableTransform.position = Vector3.Lerp(_left, _right, _factorValue);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_point1 && _point2)
        {
            Gizmos.color = Color.green;
            Vector3 position1 = _point1.position;
            Vector3 position2 = _point2.position;
            Gizmos.DrawSphere(position1, 0.1f);
            Gizmos.DrawSphere(position2, 0.1f);
            Gizmos.DrawLine(position1, position2);
            if (_movesToPoint2)
            {
                Gizmos.color = Color.blue;
                _changeableTransform.position = Vector3.Lerp(position1, position2, _startFactorValue);
                Gizmos.DrawSphere(_changeableTransform.position, 0.1f);
            }
            else
            {
                Gizmos.color = Color.red;
                _changeableTransform.position = Vector3.Lerp(position2, position1, _startFactorValue);
                Gizmos.DrawSphere(_changeableTransform.position, 0.1f);
            }
        }
    }
}