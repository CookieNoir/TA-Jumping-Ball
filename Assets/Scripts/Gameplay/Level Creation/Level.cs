using UnityEngine;
[CreateAssetMenu(fileName = "New Level", menuName = "ScriptableObjects/Level")]
public class Level : ScriptableObject
{
    public GameObject startBlockPrefab;
    public PlatformBlocksTemplate[] platformBlocksTemplates;
    [Min(0)] public int liftValue; // Количество очков, которое дается за поднятие на одну платформу
    public float ballStartHeight;
    public LevelColors levelColors;
}