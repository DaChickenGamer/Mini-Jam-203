using UnityEngine;

public class Fish
{
    private int fishToughness;
        private float[] toughnessArray = { 0.1f, 0.5f, 10f, 20f };

    public string fishName;
    private GameObject fishObject;
    private float currentLevel;

    public Fish(int toughness, string fishName, Sprite fishSprite)
    {
        fishObject = new GameObject("Fish_" + fishName);
        fishObject.transform.localScale = new Vector3(2.5f, 2.5f, 1f);
        var sr = fishObject.AddComponent<SpriteRenderer>();
        sr.sprite = fishSprite;
        sr.sortingOrder = 10;
        Spawn();
    }
        
    public void Spawn()
    {
        float level = Random.Range(1.5f, 8.5f);
        Debug.Log("Fish " + fishName + " spawned at level: " + level);
        currentLevel = level;
        fishObject.transform.position = new Vector3(0, currentLevel, 0);
    }

    public void NewSpawn()
    {
        if( Random.Range(0f, 1f) < toughnessArray[fishToughness] / 100f )
        {
            Spawn();
            Debug.Log("Fish " + fishName + " respawned at level: " + currentLevel);
        }
       
    }

    public float GetCurrentLevel()
    {
        Debug.Log("Current Level called, level: " + currentLevel);
        return currentLevel ;
    }
}
