using System;
using UnityEngine;
[Serializable]
public struct PrefabsAndProbability
{
    [Tooltip("Префаб должен содержать компонент PlatformBlock.")]
    public GameObject prefab;
    [Tooltip("Мера распространенности блока. Чем больше значение, тем больше вероятность появления этого блока."), Min(1)]
    public int probability;
}