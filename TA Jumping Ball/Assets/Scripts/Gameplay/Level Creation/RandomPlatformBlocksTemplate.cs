using UnityEngine;
[CreateAssetMenu(fileName = "New Random Platform Blocks Template", menuName = "ScriptableObjects/RandomPlatformBlocksTemplate")]
public class RandomPlatformBlocksTemplate : PlatformBlocksTemplate
{
    [SerializeField] private PrefabsAndProbability[] _prefabsAndProbabilities;
    [SerializeField, Min(1)] private int _blocksCount;
    [SerializeField] private int _randomSeed;

    public override GameObject[] GetPrefabs()
    {
        return RandomBlocksSequenceGenerator.GetSequence(_prefabsAndProbabilities, _blocksCount, _randomSeed);
    }
}