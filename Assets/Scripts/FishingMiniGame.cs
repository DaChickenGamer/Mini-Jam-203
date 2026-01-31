using UnityEngine;

public class FishingMiniGame : MonoBehaviour
{
    public static float maxCapacity;
    public float rodStrength;
    private float currentLevel;
    private float min,max;
    private const float movingRate = 1f; 
    Fish fish1;  
    void Start()
    {
        maxCapacity = 100f;
        rodStrength = 10f;
        currentLevel= maxCapacity / 2;
        CalculateRange();

        fish1 = new Fish(5f, "cod");
    
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            currentLevel+= movingRate * Time.deltaTime;
          
            
        }else
        {
            currentLevel-= movingRate * Time.deltaTime;
           
        }
          CalculateRange();
          if(IsFishCaught(fish1.GetSpawnLevel()))
          {
            Debug.Log("Fish Caught: " + fish1.name);
          }
    }


    void CalculateRange()
    {
        min = currentLevel - rodStrength;
        max = currentLevel + rodStrength;
        Debug.Log("Min: " + min + " Max: " + max);
    }

    bool IsFishCaught(float fishLevel)
    {
        return fishLevel >= min && fishLevel <= max;
    }

}
