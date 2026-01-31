using UnityEngine;

public class Fish
{
    public float fishToughness;
    public string name;
    private float spawnLevel;


    public Fish(float toughness, string fishName)
    {
        fishToughness = toughness;
        name = fishName;
        Spawn(FishingMiniGame.maxCapacity);
    }

    public void Spawn(float maxCapacity)
    {
        float level = Random.Range(0f, maxCapacity);
        Debug.Log("Fish " + name + " spawned at level: " + level);
        spawnLevel = level;
    }

    public float GetSpawnLevel()
    {
        return spawnLevel;
    }
}
