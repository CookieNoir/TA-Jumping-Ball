using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    public UnityEvent<int> OnWin;
    public UnityEvent<int> OnLoss;
    public UnityEvent<int> OnScoreChanged;
    public bool Controllable { get; private set; }
    private int _targetScore;
    private int _currentScore;

    public void SetTargetScore(int value)
    {
        _targetScore = value;
        Controllable = true;
        _SetScore(0);
    }

    public void TryToChangeScore(int value)
    {
        if (value > _currentScore) _SetScore(value);
    }

    private void _SetScore(in int value)
    {
        _currentScore = value;
        OnScoreChanged.Invoke(_currentScore);
        if (_currentScore >= _targetScore) _Win();
    }

    private void _Win()
    {
        Controllable = false;
        OnWin.Invoke(_currentScore);
    }

    public void Loss()
    {
        Controllable = false;
        OnLoss.Invoke(_currentScore);
    }
}