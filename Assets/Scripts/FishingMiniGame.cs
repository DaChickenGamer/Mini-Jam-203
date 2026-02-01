using UnityEngine;

public class FishingMiniGame : MonoBehaviour
{

    public GameObject fishingRod;
    public Sprite fishSprite;  

    private GameObject maxCapacityRod;
    public static float maxCapacity = 10f;
    public float rodStrength;
    private float currentLevel;
    private float min, max;
    private const float movingRate = 1f;
    Fish fish1;
    void Start()
    {

        currentLevel = maxCapacity / 2;
        CalculateRange();
        CreateBars();
        fish1 = new Fish(1, "cod", fishSprite);
        Debug.Log("FishingRod: " + fishingRod);
    }



    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            currentLevel += movingRate * Time.deltaTime;


        }
        else
        {
            currentLevel -= movingRate * Time.deltaTime;

        }
        currentLevel = Mathf.Clamp(currentLevel, 0+rodStrength/2, maxCapacity-rodStrength/2);
        CalculateRange();
        if (IsFishCaught(fish1.GetCurrentLevel()))
        {
            Debug.Log("Fish Caught: " + fish1.name);
        }
        else
        {
            fish1.NewSpawn();
        }
    }


    void CalculateRange()
    {
        min = currentLevel - rodStrength;
        max = currentLevel + rodStrength;
        fishingRod.transform.position = new Vector3(0, currentLevel, -0.1f);

        // Debug.Log("Min: " + min + " Max: " + max);
    }

    bool IsFishCaught(float fishLevel)
    {
        return fishLevel >= min && fishLevel <= max;
    }


    void CreateBars()
    {

        maxCapacityRod = GameObject.CreatePrimitive(PrimitiveType.Quad);
        maxCapacityRod.transform.position = new Vector3(0, maxCapacity / 2, 0);
        maxCapacityRod.transform.localScale = new Vector3(1f, maxCapacity, 1f);



        fishingRod.transform.position = new Vector3(0, 0, -0.1f);
        fishingRod.transform.localScale = new Vector3(1f, rodStrength, 1f);
    }

}
