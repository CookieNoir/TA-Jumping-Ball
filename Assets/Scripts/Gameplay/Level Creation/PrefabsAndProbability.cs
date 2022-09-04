using System;
using UnityEngine;
[Serializable]
public struct PrefabsAndProbability
{
    [Tooltip("������ ������ ��������� ��������� PlatformBlock.")]
    public GameObject prefab;
    [Tooltip("���� ������������������ �����. ��� ������ ��������, ��� ������ ����������� ��������� ����� �����."), Min(1)]
    public int probability;
}