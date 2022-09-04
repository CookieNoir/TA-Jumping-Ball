using UnityEngine;

public class TargetFramerateSetter : MonoBehaviour
{
    [SerializeField, Min(-1)] private int _targetFramerate = 60;

    private void Start()
    {
        Application.targetFrameRate = _targetFramerate;
    }
}