using UnityEngine;

public class Fish
{
    private int fishToughness;
        private float[] toughnessArray = { 0.1f, 0.5f, 10f, 20f };

    public string name;
    private GameObject fishObject;
    private float currentLevel;

    public Fish(int toughness, string fishName, Sprite fishSprite)
    {
        fishObject = new GameObject("Fish_" + name);
        var sr = fishObject.AddComponent<SpriteRenderer>();
        sr.sprite = fishSprite;
        sr.sortingOrder = 10;
        Spawn(FishingMiniGame.maxCapacity);
    }
        
    public void Spawn(float maxCapacity)
    {
        float level = Random.Range(0f, maxCapacity);
        Debug.Log("Fish " + name + " spawned at level: " + level);
        currentLevel = level;
        fishObject.transform.position = new Vector3(0, currentLevel, 0);
    }

    public void NewSpawn()
    {
        if( Random.Range(0f, 1f) < toughnessArray[fishToughness] / 100f )
        {
            Spawn(FishingMiniGame.maxCapacity);
            Debug.Log("Fish " + name + " respawned at level: " + currentLevel);
        }
       
    }

    public float GetCurrentLevel()
    {
        Debug.Log("Current Level called, level: " + currentLevel);
        return currentLevel ;
    }
}
