using UnityEngine;
[CreateAssetMenu(fileName = "New Fixed Platform Blocks Template", menuName = "ScriptableObjects/FixedPlatformBlocksTemplate")]
public class FixedPlatformBlocksTemplate : PlatformBlocksTemplate
{
    [SerializeField, Tooltip("Префабы должны содержать компонент PlatformBlock.")]
    private GameObject[] _prefabs;

    public override GameObject[] GetPrefabs()
    {
        return _prefabs;
    }
}