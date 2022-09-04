using System;
using UnityEngine;
using UnityEngine.Events;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private Transform _levelOrigin;
    [SerializeField] private Ball _ball;
    [SerializeField] private Water _water;
    [SerializeField] private FollowingCamera _followingCamera;
    public UnityEvent<int> OnLevelCreated;
    public UnityEvent<int> OnPlatformEnterValue;
    public UnityEvent<PlatformTypes> OnPlatformEnterType;
    public UnityEvent OnPlatformExit;

    private Level _currentLevel;
    private int _levelTargetScore;

    public void StartLevel(Level level)
    {
        if (level != _currentLevel)
        {
            _DropLevel();

            GameObject newObject = Instantiate(level.startBlockPrefab, _levelOrigin);
            newObject.transform.position = Vector3.zero;
            PlatformBlock platformBlock = newObject.GetComponent<PlatformBlock>();
            float currentHeight = platformBlock.BlockHeight;
            UnityAction<int, PlatformTypes> PlatformEnterActions = (value, type) =>
            {
                OnPlatformEnterValue.Invoke(value);
                OnPlatformEnterType.Invoke(type);
            };
            UnityAction PlatformExitActions = () => OnPlatformExit.Invoke();
            int valueStep = level.liftValue;
            int platformValue = -valueStep;
            platformBlock.SetValues(ref platformValue, valueStep, PlatformEnterActions, PlatformExitActions);

            foreach (PlatformBlocksTemplate template in level.platformBlocksTemplates)
            {
                GameObject[] prefabs = template.GetPrefabs();
                foreach (GameObject prefab in prefabs)
                {
                    newObject = Instantiate(prefab, _levelOrigin);
                    newObject.transform.position = currentHeight * Vector3.up;
                    platformBlock = newObject.GetComponent<PlatformBlock>();
                    currentHeight += platformBlock.BlockHeight;
                    platformBlock.SetValues(ref platformValue, valueStep, PlatformEnterActions, PlatformExitActions);
                }
            }

            ShaderGlobalsSetter.SetWorldColors(level.levelColors);

            _levelTargetScore = platformValue;
            _currentLevel = level;
        }

        foreach (Transform child in _levelOrigin)
        {
            child.GetComponent<PlatformBlock>().ResetPlatform();
        }

        Vector3 startPoint = level.ballStartHeight * Vector3.up;
        _ball.ResetValues(startPoint);
        _water.ResetValues(startPoint);
        _followingCamera.SetTarget(_ball.transform, Vector3.zero);

        OnLevelCreated.Invoke(_levelTargetScore);
    }

    private void _DropLevel()
    {
        foreach (Transform child in _levelOrigin)
        {
            Destroy(child.gameObject);
        }
    }

    public void Restart()
    {
        StartLevel(_currentLevel);
    }
}