using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private LevelCreator _levelCreator;

    private void Start()
    {
        _levelCreator.StartLevel(_level);
    }
}