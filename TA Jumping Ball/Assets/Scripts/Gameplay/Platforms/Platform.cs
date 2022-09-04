using System;
using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    [SerializeField] private PlatformTypes _platformType;
    public event UnityAction<int, PlatformTypes> OnPlatformEnter;
    public event UnityAction OnPlatformExit;
    private int _platformValue;

    public void SetPlatformValue(in int value)
    {
        _platformValue = value;
    }

    public virtual void ResetPlatform()
    {
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ILandable landable = collision.gameObject.GetComponent<ILandable>();
        if (landable != null)
        {
            Vector2 contactPoint = collision.GetContact(0).point;
            if (landable.TryToLand(contactPoint))
            {
                EnterCollisionPlatformAction();
                if (collision.gameObject.CompareTag("Player"))
                {
                    OnPlatformEnter.Invoke(_platformValue, _platformType);
                }
            }
        }
    }

    protected virtual void EnterCollisionPlatformAction()
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ILandable landable = collision.gameObject.GetComponent<ILandable>();
        if (landable != null)
        {
            ExitCollisionPlatformAction();
            if (collision.gameObject.CompareTag("Player"))
            {
                OnPlatformExit.Invoke();
            }
        }
    }

    protected virtual void ExitCollisionPlatformAction()
    {

    }
}