using System;
using UnityEngine;
using UnityEngine.Events;

public class PlatformBlock : MonoBehaviour
{
    [SerializeField, Min(0.125f)] private float _blockHeight = 0.125f;
    public float BlockHeight { get => _blockHeight; private set => _blockHeight = value; }
    [SerializeField] private Platform[] _platforms; // Должны быть упорядочены по высоте от нижайшей платформы до высочайшей

    public void SetValues(ref int value, in int valueStep, UnityAction<int, PlatformTypes> enterAction, UnityAction exitAction)
    {
        foreach (Platform platform in _platforms)
        {
            value += valueStep;
            platform.SetPlatformValue(value);
            if (enterAction != null) platform.OnPlatformEnter += enterAction;
            if (exitAction != null) platform.OnPlatformExit += exitAction;
        }
    }

    public void ResetPlatform()
    {
        foreach (Platform platform in _platforms) platform.ResetPlatform();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + _blockHeight * Vector3.up);
    }
}