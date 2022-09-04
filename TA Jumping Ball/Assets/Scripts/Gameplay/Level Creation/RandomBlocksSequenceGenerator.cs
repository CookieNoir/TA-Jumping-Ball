using UnityEngine;
using Random = System.Random;

public static class RandomBlocksSequenceGenerator
{
    public static GameObject[] GetSequence(PrefabsAndProbability[] prefabsAndProbabilities, in int blocksCount, in int randomSeed)
    {
        Random random = new Random(randomSeed);
        GameObject[] prefabs = new GameObject[blocksCount];
        
        int maxProbabilityValue = 0;
        int reducedProbabilitiesCount = prefabsAndProbabilities.Length - 1;
        int[] probabilities = new int[reducedProbabilitiesCount];
        for (int i = 0; i < reducedProbabilitiesCount; ++i)
        {
            maxProbabilityValue += prefabsAndProbabilities[i].probability;
            probabilities[i] = maxProbabilityValue;
        }
        maxProbabilityValue += prefabsAndProbabilities[reducedProbabilitiesCount].probability;

        for (int i = 0; i < blocksCount; ++i)
        {
            int value = random.Next(0, maxProbabilityValue);
            int prefabIndex = 0;
            while (prefabIndex < reducedProbabilitiesCount
                && value >= probabilities[prefabIndex]) prefabIndex++;
            prefabs[i] = prefabsAndProbabilities[prefabIndex].prefab;
        }

        return prefabs;
    }
}