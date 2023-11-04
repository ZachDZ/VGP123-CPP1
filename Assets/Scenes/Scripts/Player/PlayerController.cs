using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    // Component References
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    // Movement Variables
    public float speed = 5.0f;
    public float jumpForce = 300.0f;

    // GroundCheck Stuff
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        // Getting Our Component References
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // Checking Variables For Dirty Data
        if (rb == null)
            Debug.Log("No Rigidbody Reference");
        if (sr == null)
            Debug.Log("No Sprite Renderer Reference");
        if (anim == null)
            Debug.Log("No Animator Reference");

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f;
            Debug.Log("GroundCheck set to default value");
        }

        if (speed <= 0)
        {
            speed = 5.0f;
            Debug.Log("Speed set to default value");
        }

        if (jumpForce <= 0)
        {
            jumpForce = 3.00f;
            Debug.Log("jumpForce set to default value");
        }

        if (groundCheck == null)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(gameObject.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "GroundCheck";
            groundCheck = obj.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        float hInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (isGrounded)
        {
            rb.gravityScale = 1;
        }

        if (curPlayingClips.Length > 0)
        {
            if (curPlayingClips[0].clip.name == "Attack")
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
            }
        }

        // Jump Event
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        if (!isGrounded && Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("jumpAttack");
        }

        // Attack Events
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("fire");
        }

        // Flip Event
        if (hInput != 0) sr.flipX = (hInput < 0);

        

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);

    }

    public void IncreaseGravity()
    {
        rb.gravityScale = 5;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
