using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Rigidbody2DExtender : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private RigidbodyType2D _defaultBallRigidbodyType;
    //private float _defaultBallGravityScale;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _defaultBallRigidbodyType = _rigidbody.bodyType;
        //_defaultBallGravityScale = _rigidbody.gravityScale;
    }

    public bool HasContacts()
    {
        return _collider.IsTouchingLayers();
    }

    public void TurnOff()
    {
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        //_rigidbody.gravityScale = 0f;
        _ToggleColliders(false);
    }

    public void TurnOn()
    {
        _rigidbody.bodyType = _defaultBallRigidbodyType;
        //_rigidbody.gravityScale = _defaultBallGravityScale;
        _ToggleColliders(true);
    }

    private void _ToggleColliders(in bool value)
    {
        _collider.enabled = value;
    }
}