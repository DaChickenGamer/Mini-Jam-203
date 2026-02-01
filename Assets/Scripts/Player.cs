using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
       // body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
       // boxCollider = GetComponent<BoxCollider2D>();    
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
           transform.position += Vector3.right * moveSpeed * Time.deltaTime;
           animator.SetBool("isWalking", true);
           spriteRenderer.flipX = false; 
        }
        else if(Input.GetKey(KeyCode.A))
        {
           transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            animator.SetBool("isWalking", true);
            spriteRenderer.flipX = true;

        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
