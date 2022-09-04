using UnityEngine;

public class BreakablePlatform : Platform
{
    [SerializeField, Min(1)] private int _startLivesCount = 1;
    private int _currentLivesCount;

    public override void ResetPlatform()
    {
        _currentLivesCount = _startLivesCount;
        gameObject.SetActive(true);
    }

    protected override void ExitCollisionPlatformAction()
    {
        _currentLivesCount--;
        if (_currentLivesCount <= 0) gameObject.SetActive(false);
    }
}