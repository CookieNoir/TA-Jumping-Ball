using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour, ILandable
{
    [SerializeField] private Rigidbody2DExtender _ballRigidbody;
    [SerializeField, Min(0.01f)] private float _jumpHeight = 5f;
    [SerializeField, Tooltip("Время (в секундах), через которое расстояние от шара до точки начала прыжка будет максимальным."),
        Range(0.2f, 5f)]
    private float _jumpLength;
    [SerializeField, Tooltip("Кривая, используемая при линейной интерполяции шара от начальной точки прыжка до конечной. Должна быть определена на отрезке [0, 1].")]
    private AnimationCurve _jumpCurve;
    private IEnumerator _jumpCoroutineReference;
    private bool _onGround = false;

    private void Awake()
    {
        _jumpCoroutineReference = _JumpCoroutine();
    }

    public void ResetValues(Vector3 position)
    {
        StopCoroutine(_jumpCoroutineReference);
        _onGround = false;
        transform.position = position;
        _ballRigidbody.TurnOn();
        if (_ballRigidbody.HasContacts()) Land();
    }

    public void TryToJump()
    {
        if (_onGround)
        {
            StopCoroutine(_jumpCoroutineReference);
            _jumpCoroutineReference = _JumpCoroutine();
            _onGround = false;
            StartCoroutine(_jumpCoroutineReference);
        }
    }

    public bool TryToLand(in Vector2 landPoint)
    {
        Vector2 landDirection = landPoint - (Vector2)transform.position;
        float dot = Vector2.Dot(landDirection, Vector2.up);
        if (Mathf.Sign(dot) < 0f)
        {
            Land();
            return true;
        }
        else return false;
    }

    private void Land()
    {
        _onGround = true;
    }

    private IEnumerator _JumpCoroutine()
    {
        Vector2 startPoint = transform.position;
        Vector2 endPoint = startPoint + _jumpHeight * Vector2.up;
        float factorValue = 0f;
        float stepMultiplier = 1f / _jumpLength;

        _ballRigidbody.TurnOff();

        while (factorValue < 1f)
        {
            Vector2 newPosition = Vector2.Lerp(startPoint, endPoint, _jumpCurve.Evaluate(factorValue));
            transform.position = newPosition;
            yield return new WaitForFixedUpdate();
            factorValue += stepMultiplier * Time.fixedDeltaTime;
        }
        transform.position = endPoint;

        _ballRigidbody.TurnOn();
    }
}