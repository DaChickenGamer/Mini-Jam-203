using UnityEngine;

public class FishingMiniGame : MonoBehaviour
{

    public GameObject fishingRod;
    public Sprite fishSprite;  
    public GameObject meterBar;
    public Animator animator;

    public GameObject maxCapacityRod;
    private  float maxCapacity = 8f;
 
    public float rodStrength;
    private float currentLevel;
    private float min, max;
    private  const float movingRate = 4f;
    private float meter = 0f;
    private const int middleScreen  = 5;

    
    Fish fish1;
    void Start()
    {

        currentLevel = maxCapacity / 2;
        CalculateRange();
        CreateBars();
        fish1 = new Fish(3, "cod", fishSprite);
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
        currentLevel = Mathf.Clamp(currentLevel, middleScreen/2 - rodStrength/2, middleScreen*1.5f + rodStrength);
        CalculateRange();

        if (IsFishCaught(fish1.GetCurrentLevel()))
        {
            meter += Time.deltaTime;
        }
        else
        {
            meter -= Time.deltaTime;
            fish1.NewSpawn();
        }

        meter = Mathf.Clamp(meter,0, maxCapacity);
        meterBar.transform.localScale = new Vector3(0.1f, meter, 1f);

        if (meter >= maxCapacity)
        {
            Debug.Log("Fish Caught!");
            animator.SetBool("isFishing", false);
            animator.SetTrigger("fishCaught");
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
 maxCapacityRod.transform.localScale = new Vector3(1f, maxCapacity, 1f);
        maxCapacityRod.transform.localPosition = new Vector3(0, middleScreen, 0);
       


fishingRod.transform.localScale = new Vector3(1f, rodStrength, 1f);
        fishingRod.transform.localPosition = new Vector3(0, middleScreen, -0.1f);
        


        meterBar.transform.localPosition = new Vector3(maxCapacityRod.transform.localPosition.x + 1f, middleScreen, 0);
        meterBar.transform.localScale = new Vector3(0.1f, maxCapacity, 1f);
    }

}
