using UnityEngine;
using FMODUnity;
public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject fishingMiniGame;
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    

        public string footstepEvent;  
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // boxCollider = GetComponent<BoxCollider2D>();    
    }


    void Update()
    {
        bool isWalking = false;
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Invoke(nameof(OpenFishing), 1f);
            animator.SetBool("isFishing", true);


        }
        else
        {
            //animator.SetBool("isFishing", false);

        }


        if (Input.GetKey(KeyCode.D) && !animator.GetBool("isFishing"))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            spriteRenderer.flipX = false;
            isWalking = true;
        }
        else if (Input.GetKey(KeyCode.A) && !animator.GetBool("isFishing"))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            isWalking = true;

            spriteRenderer.flipX = true;

        }

        
        animator.SetBool("isWalking", isWalking);

    }



    void OpenFishing()
    {
        fishingMiniGame.SetActive(true);
    }

    void PlayFootstep()
    {
        RuntimeManager.PlayOneShot(footstepEvent, transform.position);
    }
}
